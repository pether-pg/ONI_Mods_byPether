using HarmonyLib;
using UnityEngine;

namespace CheckpointAutomation
{
    public class CheckpointAutomation_Patches
    {
        [HarmonyPatch(typeof(JetSuitMarkerConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public class JetSuitMarkerConfig_CreateBuildingDef_Patch
        {
            public static void Postfix(BuildingDef __result)
            {
                __result.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
            }
        }

        [HarmonyPatch(typeof(JetSuitMarkerConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public class JetSuitMarkerConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<LogicOperationalController>();
            }
        }

        [HarmonyPatch(typeof(LeadSuitMarkerConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public class LeadSuitMarkerConfig_CreateBuildingDef_Patch
        {
            public static void Postfix(BuildingDef __result)
            {
                __result.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
            }
        }

        [HarmonyPatch(typeof(LeadSuitMarkerConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public class LeadSuitMarkerConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<LogicOperationalController>();
            }
        }

        [HarmonyPatch(typeof(OxygenMaskMarkerConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public class OxygenMaskMarkerConfig_CreateBuildingDef_Patch
        {
            public static void Postfix(BuildingDef __result)
            {
                __result.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
            }
        }

        [HarmonyPatch(typeof(OxygenMaskMarkerConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public class OxygenMaskMarkerConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<LogicOperationalController>();
            }
        }

        [HarmonyPatch(typeof(SuitMarkerConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public class SuitMarkerConfig_CreateBuildingDef_Patch
        {
            public static void Postfix(BuildingDef __result)
            {
                __result.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
            }
        }

        [HarmonyPatch(typeof(SuitMarkerConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public class SuitMarkerConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<LogicOperationalController>();
            }
        }
    }
}
