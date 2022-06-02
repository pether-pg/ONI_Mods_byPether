using Klei.AI;
using Klei.AI.DiseaseGrowthRules;
using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class MutatingGerms : Disease
    {
        public static ExposureType GetExposureType(int exposureThresholdLevel = 0, int resistanceLevel = 0)
        {
            return new ExposureType()
            {
                germ_id = MutatingGerms.ID,
                sickness_id = MutatingSickness.ID,
                exposure_threshold = 151 - (exposureThresholdLevel * 10),
                excluded_traits = new List<string>() { },
                base_resistance = 5 - (resistanceLevel / 2),
                excluded_effects = new List<string>()
                    {
                      MutatingSickness.RECOVERY_ID,
                      MutatingAntiviralConfig.EffectID
                    }
            };
        }

        public const string ID = nameof(MutatingGerms);
        public static Color32 colorValue = ColorPalette.DeepPurple;

        private const float degC = 273.15f; // used to quickly convert temperature from *C to K

        private const float radResPerLvl = -0.2f;
        private const float minGrowthTemp = 10 + degC;
        private const float maxGrowthTemp = 30 + degC;
        private const float growthTempPerLvl = 10;

        public MutatingGerms(bool statsOnly)
            : base(id: MutatingGerms.ID,
                  strength: (byte)50,
                  temperature_range: new Disease.RangeInfo(minGrowthTemp - growthTempPerLvl, minGrowthTemp, maxGrowthTemp, maxGrowthTemp + growthTempPerLvl),
                  temperature_half_lives: new Disease.RangeInfo(10f, 1200f, 1200f, 10f),
                  pressure_range: new Disease.RangeInfo(0.0f, 0.0f, 1000f, 1000f),
                  pressure_half_lives: Disease.RangeInfo.Idempotent(),
                  1.0f,
                  statsOnly)
        {
        }

        public void UpdateGermData()
        {
            if (!MutationData.IsReadyToUse())
                return;

            this.radiationKillRate = 1.5f + radResPerLvl * MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Res_RadiationResistance);
            this.temperatureRange = new Disease.RangeInfo(
                minGrowthTemp - growthTempPerLvl * (1 + 2 * MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Res_TemperatureResistance)),
                minGrowthTemp - growthTempPerLvl * MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Res_TemperatureResistance),
                maxGrowthTemp + growthTempPerLvl * MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Res_TemperatureResistance),
                maxGrowthTemp + growthTempPerLvl * (1 + 2 * MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Res_TemperatureResistance))
                );
        }

        public void UpdateGrowthRules(bool ensureNull = false, int o2Lvl = 0, int co2Lvl = 0, int polLvl = 0, int h2oLvl = 0, int chlLvl = 0, int gasLvl = 0, int liqLvl = 0)
        {
            if(ensureNull)
            {
                this.exposureRules = null;
                this.growthRules = null;
            }

            int chlLiveLvl = 10;
            int o2ThriveLvl = 5;
            float chlorineDeathScale = 10.0f / (1 + chlLvl * chlLvl * chlLvl * chlLvl);
            float chlorineThriveScale = chlLvl - chlLiveLvl;
            float oxygenThriveScale = 0.3f * (1 + o2Lvl - o2ThriveLvl);
            float co2ThriveScale = 0.3f * (1 + co2Lvl);
            float waterThriveScale = 0.3f * (1 + h2oLvl);
            float pollutionThriveScale = 0.5f * (1 + polLvl);

            this.InitializeElemGrowthArray(ref this.elemGrowthInfo, Disease.DEFAULT_GROWTH_INFO);
            this.AddGrowthRule(GermGrowthRules.GrowthRule_Default());

            // Solid

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_diffScale_minDiffCount(Element.State.Solid, 0.4f, 3000f, 1200f, 1E-06f, 1000000));

            if(chlLvl <= chlLiveLvl)
                this.AddGrowthRule((GrowthRule)GermGrowthRules.DieInElement(SimHashes.BleachStone, chlorineDeathScale));
            else
                this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.BleachStone, chlorineThriveScale));

            // Gas

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_diffScale_minDiffCount(Element.State.Gas, 250f, 120f * (1 + gasLvl * gasLvl), 1200f, 1000f * (1 + gasLvl), 0.005f, 5100));

            if(o2Lvl <= o2ThriveLvl)
                this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveAndSpreadInElement(SimHashes.Oxygen));
            else
                this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.Oxygen, oxygenThriveScale));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.CarbonDioxide, co2ThriveScale));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.ContaminatedOxygen, pollutionThriveScale));

            if (chlLvl <= chlLiveLvl)
                this.AddGrowthRule((GrowthRule)GermGrowthRules.DieInElement(SimHashes.ChlorineGas, chlorineDeathScale));
            else
                this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.ChlorineGas, chlorineThriveScale));

            // Liquid

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_DiffScale(Element.State.Liquid, 0.4f, 120f * (1 + liqLvl), 300f, 100f * (1+liqLvl), 0.01f));
            
            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.Water, waterThriveScale));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.DirtyWater, pollutionThriveScale));

            if (chlLvl <= chlLiveLvl)
                this.AddGrowthRule((GrowthRule)GermGrowthRules.DieInElement(SimHashes.Chlorine, chlorineDeathScale));
            else
                this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.Chlorine, chlorineThriveScale));

            // Exposure

            this.InitializeElemExposureArray(ref this.elemExposureInfo, Disease.DEFAULT_EXPOSURE_INFO);
            this.AddExposureRule(new ExposureRule()
            {
                populationHalfLife = new float?(float.PositiveInfinity)
            });

            if (chlLvl <= chlLiveLvl)
            {
                this.AddExposureRule((ExposureRule)GermGrowthRules.KillingExposure(SimHashes.Chlorine, chlorineDeathScale));
                this.AddExposureRule((ExposureRule)GermGrowthRules.KillingExposure(SimHashes.ChlorineGas, chlorineDeathScale));
            }

            this.AddExposureRule((ExposureRule)GermGrowthRules.ExposureLike_Slimelung_PollutedOxygen(SimHashes.Water));
            this.AddExposureRule((ExposureRule)GermGrowthRules.ExposureLike_Slimelung_PollutedOxygen(SimHashes.DirtyWater));
        }

        protected override void PopulateElemGrowthInfo()
        {
            UpdateGrowthRules();
        }
    }
}
