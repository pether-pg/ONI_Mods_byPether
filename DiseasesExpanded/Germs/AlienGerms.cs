using Klei.AI;
using Klei.AI.DiseaseGrowthRules;
using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class AlienGerms : Disease
    {
        public static ExposureType GetExposureType()
        {
            return new ExposureType()
            {
                germ_id = AlienGerms.ID,
                sickness_id = AlienSickness.ID,
                exposure_threshold = 1,
                infect_immediately = true,
                excluded_traits = new List<string>() { },
                base_resistance = -1,
                excluded_effects = new List<string>()
                    {
                      AlienSickness.RECOVERY_ID
                    }
            };
        }
        public const string ID = nameof(AlienGerms);
        public static Color32 colorValue = ColorPalette.NavyBlue;

        private const float degC = 273.15f; // used to quickly convert temperature from *C to K

        public float UVKillRate { get; set; } // for Romen's UV Lamp mod

        public AlienGerms(bool statsOnly)
            : base(id: ID,
                  strength: (byte)50,
                  temperature_range: new Disease.RangeInfo(0 + degC, 75 + degC, 300 + degC, 1000 + degC),
                  temperature_half_lives: new Disease.RangeInfo(10f, 1200f, 1200f, 10f),
                  pressure_range: new Disease.RangeInfo(0.0f, 0.0f, 2000f, 5000f),
                  pressure_half_lives: Disease.RangeInfo.Idempotent(),
                  -1.0f,
                  statsOnly)
        {
            UVKillRate = radiationKillRate / 2;
        }

        protected override void PopulateElemGrowthInfo()
        {
            this.InitializeElemGrowthArray(ref this.elemGrowthInfo, Disease.DEFAULT_GROWTH_INFO);
            this.AddGrowthRule(GermGrowthRules.GrowthRule_Default());

            // Solid

            GrowthRule inSolid = GermGrowthRules.StateGrowthRule_maxPerKg_diffScale_minDiffCount(Element.State.Solid, 1f, 30000f, 10f, 100, 1E-02f, 5000);
            inSolid.underPopulationDeathRate = 1000;
            this.AddGrowthRule(inSolid);

            GrowthRule inRegolith = GermGrowthRules.ThriveAndSpreadInElement(SimHashes.Regolith);
            inRegolith.minCountPerKG = 1;
            inRegolith.underPopulationDeathRate = 1000;
            this.AddGrowthRule(inRegolith);

            this.AddGrowthRule(GermGrowthRules.DieInElement(SimHashes.BleachStone));
            this.AddGrowthRule(GermGrowthRules.DieInElement(SimHashes.Steel));

            // Gas

            this.AddGrowthRule(GermGrowthRules.StateLike_Slimelung_Gas(Element.State.Gas));

            this.AddGrowthRule(GermGrowthRules.GrowthLike_Slimelung_PollutedOxygen(SimHashes.CarbonDioxide));
            this.AddGrowthRule(GermGrowthRules.GrowthLike_Slimelung_PollutedOxygen(SimHashes.Steam));
            this.AddGrowthRule(GermGrowthRules.SurviveInElement(SimHashes.Oxygen));

            this.AddGrowthRule(GermGrowthRules.DieInElement(SimHashes.ChlorineGas));

            // Liquid

            this.AddGrowthRule(GermGrowthRules.StateLike_Slimelung_Gas(Element.State.Liquid));

            this.AddGrowthRule(GermGrowthRules.SurviveAndSpreadInElement(SimHashes.Water));

            this.AddGrowthRule(GermGrowthRules.DieInElement(SimHashes.Chlorine));

            // Other

            this.AddGrowthRule(GermGrowthRules.GrowthLike_FoodPoison_Edible(GameTags.Creature));

            // Exposure

            this.InitializeElemExposureArray(ref this.elemExposureInfo, Disease.DEFAULT_EXPOSURE_INFO);
            this.AddExposureRule(new ExposureRule()
            {
                populationHalfLife = new float?(float.PositiveInfinity)
            });
            this.AddExposureRule(GermGrowthRules.ExposureLike_Anything_ChlorineGas(SimHashes.Chlorine));
            this.AddExposureRule(GermGrowthRules.ExposureLike_Anything_ChlorineGas(SimHashes.ChlorineGas));
            this.AddExposureRule(GermGrowthRules.ExposureLike_Slimelung_PollutedOxygen(SimHashes.CarbonDioxide));
            this.AddExposureRule(GermGrowthRules.ExposureLike_Slimelung_PollutedOxygen(SimHashes.Steam));
        }
    }
}
