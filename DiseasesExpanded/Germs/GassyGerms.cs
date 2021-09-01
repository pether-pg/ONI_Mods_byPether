using Klei.AI;
using Klei.AI.DiseaseGrowthRules;
using UnityEngine;

namespace DiseasesExpanded
{
    class GassyGerms : Disease
    {
        public const string ID = nameof(GassyGerms);
        public static string staticId = nameof(GassyGerms);
        public static Color32 colorValue = ColorPalette.GassyOrange;

        public GassyGerms(bool statsOnly)
            : base(id: GassyGerms.staticId,
                  strength: (byte)50,
                  temperature_range: new Disease.RangeInfo(168.15f, 258.15f, 513.15f, 563.15f),
                  temperature_half_lives: new Disease.RangeInfo(10f, 1200f, 1200f, 10f),
                  pressure_range: new Disease.RangeInfo(0.0f, 0.0f, 1000f, 1000f),
                  pressure_half_lives: Disease.RangeInfo.Idempotent(),
                  statsOnly)
        {
        }

        protected override void PopulateElemGrowthInfo()
        {
            float infinity = float.PositiveInfinity;
            this.InitializeElemGrowthArray(ref this.elemGrowthInfo, Disease.DEFAULT_GROWTH_INFO);
            this.AddGrowthRule(GermGrowthRules.GrowthRule_Default());

            // Solid

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_diffScale_minDiffCount(Element.State.Solid, 0.4f, 3000f, 1200f, 1E-06f, 1000000));

            foreach (SimHashes element in new SimHashes[2] { SimHashes.Carbon, SimHashes.Diamond })
            {
                this.AddGrowthRule((GrowthRule)GermGrowthRules.ElementGrowthRule(element, 0.0f, infinity, 3000f, 0.005f, null, 1000f, null, null));
            }

            this.AddGrowthRule((GrowthRule)GermGrowthRules.ElementGrowthRule(SimHashes.BleachStone, null, 10f, 10f, 0.001f, null, null, 100000, null));

            // Gas

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_diffScale_minDiffCount(Element.State.Gas, 250f, 12000f, 1200f, 10000f, 0.005f, 5100));

            foreach (SimHashes element in new SimHashes[3] { SimHashes.CarbonDioxide, SimHashes.Methane, SimHashes.SourGas })
            {
                this.AddGrowthRule((GrowthRule)GermGrowthRules.ElementGrowthRule(element, 0.0f, infinity, 6000f, null, null, null, null, null));
            }

            this.AddGrowthRule((GrowthRule)GermGrowthRules.ElementGrowthRule(SimHashes.ChlorineGas, null, 10f, 10f, 0.001f, null, null, 100000, null));

            // Liquid

            this.AddGrowthRule((GrowthRule)GermGrowthRules.StateGrowthRule_maxPerKg_DiffScale(Element.State.Liquid, 0.4f, 1200f, 300f, 100f, 0.01f));

            foreach (SimHashes element in new SimHashes[4] { SimHashes.CrudeOil, SimHashes.Petroleum, SimHashes.Naphtha, SimHashes.LiquidMethane })
            {
                this.AddGrowthRule((GrowthRule)GermGrowthRules.ElementGrowthRule(element, null, infinity, 6000f, 0.005f, null, 1000f, null, null));
            }

            this.AddGrowthRule((GrowthRule)GermGrowthRules.ElementGrowthRule(SimHashes.Chlorine, null, 10f, 10f, 0.001f, null, null, 100000, null));

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
