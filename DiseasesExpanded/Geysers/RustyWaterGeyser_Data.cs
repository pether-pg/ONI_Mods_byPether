using System;
using System.Collections.Generic;
using Klei;

namespace DiseasesExpanded
{
    public static class RustyWaterGeyser_Data
    {
        public const string TYPE = "rusty_water";
        public const string TYPE_2 = "rusty_water_2";
        public const int WIDTH = 4;
        public const int HEIGHT = 2;

        public static GeyserConfigurator.GeyserType GetGeyserType(int copy = 1)
        {
            return new GeyserConfigurator.GeyserType(
                copy == 1 ? TYPE : TYPE_2,
                SimHashes.Water,
                GeyserConfigurator.GeyserShape.Liquid,
                298.15f,                        // float temperature = 25 C
                2000f,                          // float minRatePerCycle
                4000f,                          // float maxRatePerCycle
                500f,                           // float maxPressure
                (string[])null                  // string[] requiredDlcIds
                ).AddDisease(new SimUtil.DiseaseInfo()
                {
                    idx = GermIdx.RustGermIdx,
                    count = 20000
                });
        }

        public static GeyserGenericConfig.GeyserPrefabParams GetGenericGeyserPrefabParams(int copy = 1)
        {
            return new GeyserGenericConfig.GeyserPrefabParams(
                Kanims.RustyWaterGeyser,
                WIDTH,
                HEIGHT,
                GetGeyserType(copy),
                true
                );
        }
    }
}
