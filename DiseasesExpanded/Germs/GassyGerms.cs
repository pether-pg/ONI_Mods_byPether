using Klei.AI;
using Klei.AI.DiseaseGrowthRules;
using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class GassyGerms : Disease
    {
        public static ExposureType GetExposureType()
        {
            return new ExposureType()
            {
                germ_id = GassyGerms.ID,
                sickness_id = GasSickness.ID,
                exposure_threshold = (Settings.Instance.RebalanceForDiseasesRestored ? 10 : 100),
                excluded_traits = new List<string>() { "Flatulence" },
                base_resistance = 1,
                infect_immediately = true,
                excluded_effects = new List<string>()
                    {
                      GasSickness.RECOVERY_ID,
                      GassyVaccineConfig.EffectID
                    }
            };
        }

        public const string ID = nameof(GassyGerms);
        public static Color32 colorValue = ColorPalette.GassyOrange;

        private const float degC = 273.15f; // used to quickly convert temperature from *C to K
        private const float chlorineFreezingK = degC - 101;
        private const float grassTempHighWarningK = 348.15f; // see GasGrassConfig.cs
        private const float grassTempHighLethalK = 373.15f; // see GasGrassConfig.cs

        public float UVHalfLife { get; private set; } // for Romen's UV Lamp mod

        public GassyGerms(bool statsOnly)
            : base(id: GassyGerms.ID,
                  strength: (byte)50,
                  temperature_range: new Disease.RangeInfo(0, chlorineFreezingK, grassTempHighWarningK, grassTempHighLethalK),
                  temperature_half_lives: new Disease.RangeInfo(10f, 1200f, 1200f, 10f),
                  pressure_range: new Disease.RangeInfo(0.0f, 0.0f, 1000f, 1000f),
                  pressure_half_lives: Disease.RangeInfo.Idempotent(),
                  1.0f,
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

            // Gas

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_diffScale_minDiffCount(Element.State.Gas, 250f, 12000f, 1200f, 10000f, 0.001f, 1000));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveInElement(SimHashes.Oxygen));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveInElement(SimHashes.ChlorineGas));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.Methane));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.ContaminatedOxygen));


            // Liquid

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_DiffScale(Element.State.Liquid, 0.4f, 1200f, 300f, 100f, 0.01f));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveInElement(SimHashes.Chlorine));

            // Exposure

            this.InitializeElemExposureArray(ref this.elemExposureInfo, Disease.DEFAULT_EXPOSURE_INFO);
            this.AddExposureRule(new ExposureRule()
            {
                populationHalfLife = new float?(float.PositiveInfinity)
            });
            this.AddExposureRule((ExposureRule)GermGrowthRules.ExposureLike_Slimelung_PollutedOxygen(SimHashes.Methane));
        }
    }
}
