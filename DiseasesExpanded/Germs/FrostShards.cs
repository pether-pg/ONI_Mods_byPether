using Klei.AI;
using Klei.AI.DiseaseGrowthRules;
using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class FrostShards : Disease
    {
        public static ExposureType GetExposureType()
        {
            return new ExposureType()
            {
                germ_id = FrostShards.ID,
                sickness_id = FrostSickness.ID,
                exposure_threshold = 500,
                infect_immediately = true,
                excluded_traits = new List<string>() { },
                base_resistance = 2,
                excluded_effects = new List<string>()
                    {
                      FrostSickness.RECOVERY_ID,
                      "RecentlySauna", // See SaunaConfig.cs
                      "RecentlyHotTub" // See HotTubConfig.cs
                    }
            };
        }
        public const string ID = nameof(FrostShards);
        public static Color32 colorValue = ColorPalette.IcyBlue;

        private static float degC = 273.15f; // used to quickly convert temperature from *C to K

        public float UVHalfLife { get; private set; } // for Romen's UV Lamp mod

        public FrostShards(bool statsOnly)
            : base(id: FrostShards.ID,
                  strength: (byte)50,
                  temperature_range: new Disease.RangeInfo(0f, 0.15f, degC + 0, degC + 25),
                  temperature_half_lives: new Disease.RangeInfo(10f, 1200f, 1200f, 10f),
                  pressure_range: new Disease.RangeInfo(0.0f, 0.0f, 1000f, 1000f),
                  pressure_half_lives: Disease.RangeInfo.Idempotent(),
                  0.0f,
                  statsOnly)
        {
            UVHalfLife = UVLampSupport.UVHalfLife_GetFromRadKillRate(radiationKillRate);
        }

        protected override void PopulateElemGrowthInfo()
        {
            this.InitializeElemGrowthArray(ref this.elemGrowthInfo, Disease.DEFAULT_GROWTH_INFO);
            this.AddGrowthRule(GermGrowthRules.GrowthRule_Default());

            // Solid

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_diffScale_minDiffCount(Element.State.Solid, 0.4f, 3000f, 1200f, 1E-06f, 1000000));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.Ice));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.DirtyIce));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.DieInElement(SimHashes.BleachStone));

            // Gas

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_diffScale_minDiffCount(Element.State.Gas, 250f, 12000f, 1200f, 10000f, 0.005f, 5100));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveAndSpreadInElement(SimHashes.Oxygen));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveAndSpreadInElement(SimHashes.Hydrogen));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveAndSpreadInElement(SimHashes.CarbonDioxide));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.DieInElement(SimHashes.ChlorineGas));

            // Liquid

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_DiffScale(Element.State.Liquid, 0.4f, 1200f, 300f, 100f, 0.01f));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.LiquidOxygen));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.LiquidHydrogen));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.LiquidCarbonDioxide));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.DieInElement(SimHashes.Chlorine));

            // Exposure

            this.InitializeElemExposureArray(ref this.elemExposureInfo, Disease.DEFAULT_EXPOSURE_INFO);
            this.AddExposureRule(new ExposureRule()
            {
                populationHalfLife = new float?(float.PositiveInfinity)
            });
            ElementExposureRule elementExposureRule1 = new ElementExposureRule(SimHashes.Chlorine);
            elementExposureRule1.populationHalfLife = new float?(10f);
            this.AddExposureRule((ExposureRule)elementExposureRule1);
            ElementExposureRule elementExposureRule2 = new ElementExposureRule(SimHashes.ChlorineGas);
            elementExposureRule2.populationHalfLife = new float?(10f);
            this.AddExposureRule((ExposureRule)elementExposureRule2);
        }
    }
}
