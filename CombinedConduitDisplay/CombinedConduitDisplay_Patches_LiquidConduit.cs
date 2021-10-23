using HarmonyLib;
using UnityEngine;

namespace CombinedConduitDisplay
{
    class CombinedConduitDisplay_Patches_LiquidConduit
    {
        [HarmonyPatch(typeof(LiquidVentConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LiquidVentConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LiquidMiniPumpConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LiquidMiniPumpConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LiquidValveConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LiquidValveConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LiquidLogicValveConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LiquidLogicValveConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LiquidLimitValveConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LiquidLimitValveConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LiquidConduitPreferentialFlowConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LiquidConduitPreferentialFlowConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LiquidFilterConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LiquidFilterConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }
        [HarmonyPatch(typeof(LiquidConduitTemperatureSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LiquidConduitTemperatureSensorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LiquidConduitElementSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LiquidConduitElementSensorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LiquidConduitDiseaseSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LiquidConduitDiseaseSensorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }
    }
}
