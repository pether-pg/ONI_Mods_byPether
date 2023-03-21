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
        public static Color32 ColorValue = Settings.Instance.MutatingVirus.MutationVirusStageColors[0];

        private const float degC = 273.15f; // used to quickly convert temperature from *C to K

        private const float radResPerLvl = -0.15f;
        private const float minGrowthTemp = 15 + degC;
        private const float maxGrowthTemp = 20 + degC;
        private const float growthTempPerLvl = 5;

        public float UVHalfLife { get; private set; } // for Romen's UV Lamp mod

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
            UVHalfLife = UVLampSupport.UVHalfLife_GetFromRadKillRate(radiationKillRate);
        }

        public void UpdateGermData()
        {
            if (!MutationData.IsReadyToUse())
                return;

            this.overlayLegendHovertext = MutationData.Instance.GetLegendString();
            this.radiationKillRate = 1.5f + radResPerLvl * MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Res_RadiationResistance);
            this.UVHalfLife = UVLampSupport.UVHalfLife_GetFromRadKillRate(radiationKillRate);
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

            this.InitializeElemGrowthArray(ref this.elemGrowthInfo, Disease.DEFAULT_GROWTH_INFO);
            this.AddGrowthRule(GermGrowthRules.GrowthRule_Default());

            // Solid

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_diffScale_minDiffCount(Element.State.Solid, 0.4f, 3000f, 10f, 1000f, 1E-06f, 5000));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.DieInElement(SimHashes.BleachStone));

            // Gas

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_diffScale_minDiffCount(Element.State.Gas, 250f, 1200f, 10f, 1000000f, 0.005f, 5000));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveAndSpreadInElement(SimHashes.Oxygen));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveAndSpreadInElement(SimHashes.CarbonDioxide));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.ContaminatedOxygen));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.DieInElement(SimHashes.ChlorineGas));

            // Liquid

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_diffScale_minDiffCount(Element.State.Liquid, 0.4f, 1200f, 1000f, 1000f, 100f, 5000));
            
            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.DirtyWater));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.DieInElement(SimHashes.Chlorine));

            // Exposure

            this.InitializeElemExposureArray(ref this.elemExposureInfo, Disease.DEFAULT_EXPOSURE_INFO);
            this.AddExposureRule(new ExposureRule()
            {
                populationHalfLife = new float?(float.PositiveInfinity)
            });

            this.AddExposureRule((ExposureRule)GermGrowthRules.ExposureLike_Anything_ChlorineGas(SimHashes.Chlorine));
            this.AddExposureRule((ExposureRule)GermGrowthRules.ExposureLike_Anything_ChlorineGas(SimHashes.ChlorineGas));

            this.AddExposureRule((ExposureRule)GermGrowthRules.ExposureLike_Slimelung_PollutedOxygen(SimHashes.DirtyWater));
        }

        protected override void PopulateElemGrowthInfo()
        {
            UpdateGrowthRules();
        }
    }
}
