using System;
using System.Collections.Generic;
using Klei.AI;
using Klei.AI.DiseaseGrowthRules;
using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class RustGerms : Disease
    {
        public static ExposureType GetExposureType()
        {
            return new ExposureType()
            {
                germ_id = RustGerms.ID,
                sickness_id = RustSickness_0.ID,
                exposure_threshold = 100,
                infect_immediately = true,
                excluded_traits = new List<string>() { },
                base_resistance = 0,
                excluded_effects = new List<string>()
                {
                    RustSickness_0.RECOVERY_ID // Higher stages use the same EffectId
                }
            };
        }

        public const string ID = nameof(RustGerms);
        public static Color32 ColorValue = Settings.Instance.RustDust.GermColor;

        private const float degC = 273.15f; // used to quickly convert temperature from *C to K

        public float UVHalfLife { get; private set; } // for Romen's UV Lamp mod

        public RustGerms(bool statsOnly)
            : base(id: RustGerms.ID,
                  strength: (byte)50,
                  temperature_range: new Disease.RangeInfo(-100 + degC, -35 + degC, 60 + degC, 100 + degC),
                  temperature_half_lives: new Disease.RangeInfo(10f, 1200f, 1200f, 10f),
                  pressure_range: new Disease.RangeInfo(0.0f, 0.0f, 1000f, 1000f),
                  pressure_half_lives: Disease.RangeInfo.Idempotent(),
                  5.0f,
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

            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveAndSpreadInElement(SimHashes.BleachStone));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.OxyRock));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.Rust));

            // Gas

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_diffScale_minDiffCount(Element.State.Gas, 250f, 6000f, 1200f, 100000f, 0.005f, 5100));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.Steam));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.GrowthLike_Slimelung_Oxygen(SimHashes.Oxygen));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.GrowthLike_Slimelung_Oxygen(SimHashes.Hydrogen));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveInElement(SimHashes.ChlorineGas));

            // Liquid

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_DiffScale(Element.State.Liquid, 0.4f, 1200f, 300f, 100f, 0.01f));

            this.AddGrowthRule((GrowthRule)GermGrowthRules.SurviveInElement(SimHashes.Chlorine));
            this.AddGrowthRule((GrowthRule)GermGrowthRules.ThriveAndSpreadInElement(SimHashes.Water));

            // Exposure

            this.InitializeElemExposureArray(ref this.elemExposureInfo, Disease.DEFAULT_EXPOSURE_INFO);
            this.AddExposureRule(new ExposureRule()
            {
                populationHalfLife = new float?(float.PositiveInfinity)
            });
        }
    }
}
