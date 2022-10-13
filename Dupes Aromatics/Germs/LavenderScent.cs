using Klei.AI;
using Klei.AI.DiseaseGrowthRules;
using UnityEngine;
using System.Collections.Generic;
using Dupes_Aromatics.Misc;

namespace Dupes_Aromatics.Germs
{
    class LavenderScent : Disease
    {
        public static ExposureType GetAllergiesExposureType()
        {
            return new ExposureType()
            {
                germ_id = LavenderScent.ID,
                sickness_id = "Allergies",
                exposure_threshold = 2,
                infect_immediately = true,
                required_traits = new List<string>() { "Allergies" },
                excluded_effects = new List<string>()
                {
                  "HistamineSuppression"
                }
            };
        }

        public static ExposureType GetSmelledExposureType()
        {
            return new ExposureType()
            {
                germ_id = LavenderScent.ID,
                infection_effect = "SmelledFlowers",
                exposure_threshold = 2,
                infect_immediately = true,
                excluded_traits = new List<string>() { "Allergies" }
            };
        }

        public const string ID = nameof(LavenderScent);
        public const string EFFECT_ID = "SmelledLavender";
        public static Color32 colorValue = new Color32(255, 0, 255, 255);

        private const float plantTempLethalLow = 218.15f; // from EntityTemplates.ExtendEntityToBasicPlant()
        private const float plantTempWarnLow = 283.15f; // from EntityTemplates.ExtendEntityToBasicPlant()
        private const float plantTempWarnHigh = 303.15f; // from EntityTemplates.ExtendEntityToBasicPlant()
        private const float plantTempLethalHigh = 398.15f; // from EntityTemplates.ExtendEntityToBasicPlant()

        public float UVHalfLife { get; private set; } // for Romen's UV Lamp mod, do not rename from UVHalfLife

        public LavenderScent(bool statsOnly)
        : base(id: LavenderScent.ID,
              strength: (byte)50,
              temperature_range: new Disease.RangeInfo(plantTempLethalLow, plantTempWarnLow, plantTempWarnHigh, plantTempLethalHigh),
              temperature_half_lives: new Disease.RangeInfo(10f, 1200f, 1200f, 10f),
              pressure_range: new Disease.RangeInfo(0.0f, 0.0f, 1000f, 1000f),
              pressure_half_lives: Disease.RangeInfo.Idempotent(),
              1.0f,
              statsOnly)
        {
            UVHalfLife = 10;
        }

        protected override void PopulateElemGrowthInfo()
        {
            this.InitializeElemGrowthArray(ref this.elemGrowthInfo, Disease.DEFAULT_GROWTH_INFO);
            this.AddGrowthRule(GermGrowthRules.GrowthRule_Default());

            // Solid

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_diffScale_minDiffCount(Element.State.Solid, 0.4f, 3000f, 1200f, 1E-06f, 1000000));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.DieInElement(SimHashes.BleachStone));

            // Gas

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_diffScale_minDiffCount(Element.State.Gas, 250f, 12000f, 1200f, 10000f, 0.005f, 5100));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveAndSpreadInElement(SimHashes.Oxygen));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveAndSpreadInElement(SimHashes.CarbonDioxide));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveAndSpreadInElement(SimHashes.ContaminatedOxygen));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.DieInElement(SimHashes.ChlorineGas));

            // Liquid

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_DiffScale(Element.State.Liquid, 0.4f, 1200f, 300f, 100f, 0.01f));

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