using Klei.AI;
using Klei.AI.DiseaseGrowthRules;
using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class AbandonedGerms : Disease
    {
        public const string ID = nameof(AbandonedGerms);
        public static Color32 ColorValue = ColorPalette.PureBlack;
        public float UVHalfLife { get; private set; } // for Romen's UV Lamp mod

        // This is fake germ, should never appear on the map
        // The purpose of it is to use it, when there there are germs needed, but should die quickly.
        // Just in case something spawned those germs, make them die in anything as soon as possible

        public AbandonedGerms(bool statsOnly)
            : base(id: ID,
                  strength: (byte)50,
                  temperature_range: new Disease.RangeInfo(0, 0, 0, 0),
                  temperature_half_lives: new Disease.RangeInfo(0, 0, 0, 0),
                  pressure_range: new Disease.RangeInfo(0.0f, 0.0f, 0.0f, 0.0f),
                  pressure_half_lives: Disease.RangeInfo.Idempotent(),
                  100.0f,
                  statsOnly)
        {
            UVHalfLife = UVLampSupport.UVHalfLife_GetFromRadKillRate(radiationKillRate);
        }

        protected override void PopulateElemGrowthInfo()
        {
            this.InitializeElemGrowthArray(ref this.elemGrowthInfo, Disease.DEFAULT_GROWTH_INFO);
            this.AddGrowthRule(GermGrowthRules.GrowthRule_Default());

            this.AddGrowthRule(GermGrowthRules.StateGrowthRule_maxPerKg_DiffScale(Element.State.Solid, 0, 1.0f, 1.0f, 0, 0));
            this.AddGrowthRule(GermGrowthRules.StateGrowthRule_maxPerKg_DiffScale(Element.State.Liquid, 0, 1.0f, 1.0f, 0, 0));
            this.AddGrowthRule(GermGrowthRules.StateGrowthRule_maxPerKg_DiffScale(Element.State.Gas, 0, 1.0f, 1.0f, 0, 0));

            this.InitializeElemExposureArray(ref this.elemExposureInfo, Disease.DEFAULT_EXPOSURE_INFO);
        }
    }
}
