using Klei.AI;
using Klei.AI.DiseaseGrowthRules;
using UnityEngine;
using System.Collections.Generic;
using Dupes_Aromatics.Misc;

namespace Dupes_Aromatics
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
                germ_id = ID,
                infection_effect = EFFECT_ID,
                exposure_threshold = 100,
                infect_immediately = true,
                excluded_traits = new List<string>() { "Allergies" }
            };
        }

        public static Effect GetSmellEffect()
        {
            Effect effect = new Effect(EFFECT_ID, STRINGS.EFFECTS.SMELLEDLAVENDER.NAME, STRINGS.EFFECTS.SMELLEDLAVENDER.DESC, EFFECT_TIME, true, true, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier("Botanist", EFFECT_STR, STRINGS.EFFECTS.SMELLEDLAVENDER.NAME));
            effect.SelfModifiers.Add(new AttributeModifier("Ranching", EFFECT_STR, STRINGS.EFFECTS.SMELLEDLAVENDER.NAME));
            effect.SelfModifiers.Add(new AttributeModifier("Cooking", EFFECT_STR, STRINGS.EFFECTS.SMELLEDLAVENDER.NAME));
            return effect;
        }

        public static Effect GetCritterEffect()
        {
            Effect effect = new Effect(EFFECT_ID_CRITTER, STRINGS.EFFECTS.SMELLEDLAVENDER.NAME, STRINGS.EFFECTS.SMELLEDLAVENDER.DESC, EFFECT_TIME, true, true, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, 1, STRINGS.EFFECTS.SMELLEDLAVENDER.NAME));
            effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Amounts.Wildness.deltaAttribute.Id, -5 / 600.0f, STRINGS.EFFECTS.SMELLEDLAVENDER.NAME));
            effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Amounts.Fertility.deltaAttribute.Id, 5 / 600.0f, STRINGS.EFFECTS.SMELLEDLAVENDER.NAME));
            return effect;
        }

        public const string ID = nameof(LavenderScent);
        public const string EFFECT_ID = "SmelledLavender";
        public const string EFFECT_ID_CRITTER = "CritterSmelledLavender";
        public const float EFFECT_TIME = 300;
        public const float EFFECT_STR = 3;
        public static Color32 colorValue = new Color32(105, 79, 179, 255);

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