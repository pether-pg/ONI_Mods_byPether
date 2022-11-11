using Klei.AI;
using Klei.AI.DiseaseGrowthRules;
using UnityEngine;
using System.Collections.Generic;
using Dupes_Aromatics.Misc;

namespace Dupes_Aromatics
{
    class MallowScent : Disease
    {
        public static ExposureType GetAllergiesExposureType()
        {
            return new ExposureType()
            {
                germ_id = MallowScent.ID,
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
                germ_id = MallowScent.ID,
                infection_effect = "SmelledFlowers",
                exposure_threshold = 2,
                infect_immediately = true,
                excluded_traits = new List<string>() { "Allergies" }
            };
        }

        public static Effect GetSmellEffect()
        {
            Effect effect = new Effect(EFFECT_ID, STRINGS.EFFECTS.SMELLEDMALLOW.NAME, STRINGS.EFFECTS.SMELLEDMALLOW.DESC, EFFECT_TIME, true, true, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier("Athletics", EFFECT_STR, STRINGS.EFFECTS.SMELLEDMALLOW.NAME));
            effect.SelfModifiers.Add(new AttributeModifier("Strength", EFFECT_STR, STRINGS.EFFECTS.SMELLEDMALLOW.NAME));
            effect.SelfModifiers.Add(new AttributeModifier("Machinery", EFFECT_STR, STRINGS.EFFECTS.SMELLEDMALLOW.NAME));
            return effect;
        }

        public const string ID = nameof(MallowScent);
        public const string EFFECT_ID = "SmelledMallow";
        public const float EFFECT_TIME = 300;
        public const float EFFECT_STR = 1;
        public static Color32 colorValue = new Color32(255, 255, 255, 255);

        private const float plantTempLethalLow = 218.15f; // from EntityTemplates.ExtendEntityToBasicPlant()
        private const float plantTempWarnLow = 283.15f; // from EntityTemplates.ExtendEntityToBasicPlant()
        private const float plantTempWarnHigh = 303.15f; // from EntityTemplates.ExtendEntityToBasicPlant()
        private const float plantTempLethalHigh = 398.15f; // from EntityTemplates.ExtendEntityToBasicPlant()

        public float UVHalfLife { get; private set; } // for Romen's UV Lamp mod, do not rename from UVHalfLife

        public MallowScent(bool statsOnly)
        : base(id: MallowScent.ID,
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
            this.AddGrowthRule(GermGrowthRules.DefaultLike_PollengGerms());

            // Solid

            this.AddGrowthRule(GermGrowthRules.StateLike_PollengGerms_Solid(Element.State.Solid));

            // Gas

            this.AddGrowthRule(GermGrowthRules.StateLike_PollengGerms_Gas(Element.State.Gas));
            this.AddGrowthRule(GermGrowthRules.GrowthLike_PollenGerms_Oxygen(SimHashes.Oxygen));

            // Liquid

            this.AddGrowthRule(GermGrowthRules.StateLike_PollengGerms_Liquid(Element.State.Liquid));

            // Exposure

            this.InitializeElemExposureArray(ref this.elemExposureInfo, Disease.DEFAULT_EXPOSURE_INFO);
            this.AddExposureRule(GermGrowthRules.ExposureLike_PollenGerms_Default());
            this.AddExposureRule(GermGrowthRules.ExposureLike_PollenGerms_Oxygen(SimHashes.Oxygen));
        }
    }
}