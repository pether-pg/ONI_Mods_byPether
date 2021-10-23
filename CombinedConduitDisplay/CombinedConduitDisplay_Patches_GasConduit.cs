using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;

namespace CombinedConduitDisplay
{
    class CombinedConduitDisplay_Patches_GasConduit
    {        
        [HarmonyPatch(typeof(GasVentConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class GasVentConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(GasVentHighPressureConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class GasVentHighPressureConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(GasValveConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class GasValveConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(GasLogicValveConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class GasLogicValveConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(GasFilterConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class GasFilterConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(GasLimitValveConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class GasLimitValveConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(GasConduitTemperatureSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class GasConduitTemperatureSensorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(GasConduitElementSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class GasConduitElementSensorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(GasConduitDiseaseSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class GasConduitDiseaseSensorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }
    }
}
