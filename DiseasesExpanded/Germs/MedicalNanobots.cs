using Klei.AI;
using Klei.AI.DiseaseGrowthRules;
using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class MedicalNanobots : Disease
    {
        public static ExposureType GetExposureType(int exposureThresholdLevel = 0)
        {
            return new ExposureType()
            {
                germ_id = MedicalNanobots.ID,
                infection_effect = EFFECT_ID,
                exposure_threshold = 2000 - (exposureThresholdLevel * 100),
                infect_immediately = true,
                excluded_traits = new List<string>() { }
            };
        }

        public static bool MaliciousOverride = false; // When set to true, will make nanobots attack and damage duplicants. Don't do it!

        public static Effect GetEffect()
        {
            float sgn = MaliciousOverride ? -1 : 1;
            int timeLvl = 0;
            if(MedicalNanobotsData.IsReadyToUse())
                timeLvl = MedicalNanobotsData.Instance.GetDevelopmentLevel(MutationVectors.Vectors.Res_EffectDuration);
            
            float time = (1 + timeLvl) * durationTimePerLvl;
            Effect effect = new Effect(EFFECT_ID, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME, STRINGS.EFFECTS.NANOBOTENHANCEMENT.DESC, time, true, true, false);

            if (!MedicalNanobotsData.IsReadyToUse())
                return effect;

            int strLvl = MedicalNanobotsData.Instance.GetDevelopmentLevel(MutationVectors.Vectors.Att_Stress);
            int calLvl = MedicalNanobotsData.Instance.GetDevelopmentLevel(MutationVectors.Vectors.Att_Calories);
            int brtLvl = MedicalNanobotsData.Instance.GetDevelopmentLevel(MutationVectors.Vectors.Att_Breathing);
            int exhLvl = MedicalNanobotsData.Instance.GetDevelopmentLevel(MutationVectors.Vectors.Att_Stamina);
            int attLvl = MedicalNanobotsData.Instance.GetDevelopmentLevel(MutationVectors.Vectors.Att_Attributes);
            int hpsLvl = MedicalNanobotsData.Instance.GetDevelopmentLevel(MutationVectors.Vectors.Att_Damage);
            int resLvl = MedicalNanobotsData.Instance.GetDevelopmentLevel(MutationVectors.Vectors.Res_BaseInfectionResistance);

            if (hpsLvl > 0)
                effect.SelfModifiers.Add(new AttributeModifier("HitPointsDelta", sgn * healPerLvl * hpsLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));
            if (strLvl > 0)
                effect.SelfModifiers.Add(new AttributeModifier("StressDelta", sgn * stressPerLvl * strLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));
            if (brtLvl > 0)
                effect.SelfModifiers.Add(new AttributeModifier("BreathDelta", sgn * breathPerLvl * brtLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));
            if (exhLvl > 0)
                effect.SelfModifiers.Add(new AttributeModifier("StaminaDelta", sgn * staminaPerLvl * exhLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));
            if (calLvl > 0)
                effect.SelfModifiers.Add(new AttributeModifier("CaloriesDelta", sgn * calPerLvl * calLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));
            if (resLvl > 0)
                effect.SelfModifiers.Add(new AttributeModifier("GermResistance", sgn * resPerLvl * resLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));

            if (attLvl > 0)
            {
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Athletics.Id, sgn * attrPerLvl * attLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Strength.Id, sgn * attrPerLvl * attLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Digging.Id, sgn * attrPerLvl * attLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Construction.Id, sgn * attrPerLvl * attLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Art.Id, sgn * attrPerLvl * attLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Caring.Id, sgn * attrPerLvl * attLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Learning.Id, sgn * attrPerLvl * attLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Machinery.Id, sgn * attrPerLvl * attLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Cooking.Id, sgn * attrPerLvl * attLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Botanist.Id, sgn * attrPerLvl * attLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Ranching.Id, sgn * attrPerLvl * attLvl, STRINGS.EFFECTS.NANOBOTENHANCEMENT.NAME));
            }

            return effect;
        }

        public const string ID = nameof(MedicalNanobots);
        public const string EFFECT_ID = "MedicalNanobotsEnhancement";
        public static Color32 ColorValue = Settings.Instance.MedicalNanobots.GermColor;

        private const float degC = 273.15f; // used to quickly convert temperature from *C to K

        private const float radResPerLvl = -0.1f;
        private const float minGrowthTemp = 10 + degC;
        private const float maxGrowthTemp = 30 + degC;
        private const float growthTempPerLvl = 5;
        private const float durationTimePerLvl = 150;
        private const float baseHalfLife = 12000f;

        private const float stressPerDay = -3;
        private const float stressPerLvl = stressPerDay / 600;
        private const float calPerDay = 1666.682f;
        private const float calPerLvl = calPerDay / 30;
        private const float breathPerLvl = 0.1f;
        private const float staminaPerLvl = 0.005f;
        private const float attrPerLvl = 1f;
        private const float resPerLvl = 0.3f;
        private const float healPerLvl = 0.1f;

        public float UVHalfLife { get; private set; } // for Romen's UV Lamp mod

        public MedicalNanobots(bool statsOnly)
            : base(id: MedicalNanobots.ID,
                  strength: (byte)50,
                  temperature_range: new Disease.RangeInfo(0f, 0.15f, degC + 0, degC + 25),
                  temperature_half_lives: new Disease.RangeInfo(10f, baseHalfLife, baseHalfLife, 10f),
                  pressure_range: new Disease.RangeInfo(0.0f, 0.0f, 1000f, 1000f),
                  pressure_half_lives: Disease.RangeInfo.Idempotent(),
                  1.5f,
                  statsOnly)
        {
            UVHalfLife = UVLampSupport.UVHalfLife_GetFromRadKillRate(radiationKillRate);
        }

        public void UpdateGermData()
        {
            if (!MedicalNanobotsData.IsReadyToUse())
                return;

            this.radiationKillRate = 1.5f + radResPerLvl * MedicalNanobotsData.Instance.GetDevelopmentLevel(MutationVectors.Vectors.Res_RadiationResistance);
            this.UVHalfLife = UVLampSupport.UVHalfLife_GetFromRadKillRate(radiationKillRate);
            this.temperatureRange = new Disease.RangeInfo(
                minGrowthTemp - growthTempPerLvl * (1 + 2 * MedicalNanobotsData.Instance.GetDevelopmentLevel(MutationVectors.Vectors.Res_TemperatureResistance)),
                minGrowthTemp - growthTempPerLvl * MedicalNanobotsData.Instance.GetDevelopmentLevel(MutationVectors.Vectors.Res_TemperatureResistance),
                maxGrowthTemp + growthTempPerLvl * MedicalNanobotsData.Instance.GetDevelopmentLevel(MutationVectors.Vectors.Res_TemperatureResistance),
                maxGrowthTemp + growthTempPerLvl * (1 + 2 * MedicalNanobotsData.Instance.GetDevelopmentLevel(MutationVectors.Vectors.Res_TemperatureResistance))
                );
            this.overlayLegendHovertext = MedicalNanobotsData.Instance.GetLegendString();
        }

        public void UpdateGrowthRules(bool ensureNull = false)
        {
            if (ensureNull)
            {
                this.exposureRules = null;
                this.growthRules = null;
            }

            this.InitializeElemGrowthArray(ref this.elemGrowthInfo, Disease.DEFAULT_GROWTH_INFO);
            this.AddGrowthRule(GermGrowthRules.GrowthRule_Default());

            // Solid

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_diffScale_minDiffCount(Element.State.Solid, 0.4f, 3000f, 1200f, 1E-06f, 1000000));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.Steel));

            // Gas

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_diffScale_minDiffCount(Element.State.Gas, 250f, baseHalfLife, baseHalfLife / 10, NanobotPackConfig.SPAWNED_BOTS_COUNT, 0.005f, 5100));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveAndSpreadInElement(SimHashes.Oxygen));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveAndSpreadInElement(SimHashes.Hydrogen));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveAndSpreadInElement(SimHashes.CarbonDioxide));

            // Liquid

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_DiffScale(Element.State.Liquid, 0.4f, 1200f, 300f, 100f, 0.01f));

            // Exposure

            this.InitializeElemExposureArray(ref this.elemExposureInfo, Disease.DEFAULT_EXPOSURE_INFO);
            this.AddExposureRule(new ExposureRule()
            {
                populationHalfLife = new float?(float.PositiveInfinity)
            });
        }

        protected override void PopulateElemGrowthInfo()
        {
            UpdateGrowthRules();
        }
    }
}
