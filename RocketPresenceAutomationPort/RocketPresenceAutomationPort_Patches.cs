using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;

namespace RocketPresenceAutomationPort
{
    public class RocketPresenceAutomationPort_Patches
    {
        private static void AddLogicPort(ref BuildingDef result, bool smallEngine = false)
        {
            result.LogicOutputPorts = new List<LogicPorts.Port>();
            result.LogicOutputPorts.Add(LogicPorts.Port.OutputPort((HashedString)PresencePortStrings.Id,
                                                                    smallEngine? new CellOffset(1, 1) : new CellOffset(2, 2),
                                                                    PresencePortStrings.Description,
                                                                    PresencePortStrings.Active,
                                                                    PresencePortStrings.Inactive));

        }

        private static void AddPortScipt(GameObject go)
        {
            PresencePort port = go.AddOrGet<PresencePort>();
            port.PortName = (HashedString)PresencePortStrings.Id;
        }

        [HarmonyPatch(typeof(SteamEngineConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public static class SteamEngineConfig_CreateBuildingDef_Patch
        {
            public static void Postfix(ref BuildingDef __result)
            {
                AddLogicPort(ref __result);
            }
        }

        [HarmonyPatch(typeof(KeroseneEngineConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public static class KeroseneEngineConfig_CreateBuildingDef_Patch
        {
            public static void Postfix(ref BuildingDef __result)
            {
                AddLogicPort(ref __result);
            }
        }

        [HarmonyPatch(typeof(HydrogenEngineConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public static class HydrogenEngineConfig_CreateBuildingDef_Patch
        {
            public static void Postfix(ref BuildingDef __result)
            {
                AddLogicPort(ref __result);
            }
        }

        [HarmonyPatch(typeof(CO2EngineConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public static class CO2EngineConfig_CreateBuildingDef_Patch
        {
            public static void Postfix(ref BuildingDef __result)
            {
                AddLogicPort(ref __result, true);
            }
        }

        [HarmonyPatch(typeof(SugarEngineConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public static class SugarEngineConfig_CreateBuildingDef_Patch
        {
            public static void Postfix(ref BuildingDef __result)
            {
                AddLogicPort(ref __result, true);
            }
        }

        [HarmonyPatch(typeof(KeroseneEngineClusterSmallConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public static class KeroseneEngineClusterSmallConfig_CreateBuildingDef_Patch
        {
            public static void Postfix(ref BuildingDef __result)
            {
                AddLogicPort(ref __result, true);
            }
        }

        [HarmonyPatch(typeof(KeroseneEngineClusterConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public static class KeroseneEngineClusterConfig_CreateBuildingDef_Patch
        {
            public static void Postfix(ref BuildingDef __result)
            {
                AddLogicPort(ref __result);
            }
        }

        [HarmonyPatch(typeof(SteamEngineClusterConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public static class SteamEngineClusterConfig_CreateBuildingDef_Patch
        {
            public static void Postfix(ref BuildingDef __result)
            {
                AddLogicPort(ref __result);
            }
        }

        [HarmonyPatch(typeof(HydrogenEngineClusterConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public static class HydrogenEngineClusterConfig_CreateBuildingDef_Patch
        {
            public static void Postfix(ref BuildingDef __result)
            {
                AddLogicPort(ref __result);
            }
        }

        [HarmonyPatch(typeof(HEPEngineConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public static class HEPEngineConfig_CreateBuildingDef_Patch
        {
            public static void Postfix(ref BuildingDef __result)
            {
                AddLogicPort(ref __result);
            }
        }

        [HarmonyPatch(typeof(SteamEngineConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class SteamEngineConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                AddPortScipt(go);
            }
        }

        [HarmonyPatch(typeof(KeroseneEngineConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class KeroseneEngineConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                AddPortScipt(go);
            }
        }

        [HarmonyPatch(typeof(HydrogenEngineConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class HydrogenEngineConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                AddPortScipt(go);
            }
        }

        [HarmonyPatch(typeof(CO2EngineConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class CO2EngineConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                AddPortScipt(go);
            }
        }

        [HarmonyPatch(typeof(SugarEngineConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class SugarEngineConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                AddPortScipt(go);
            }
        }

        [HarmonyPatch(typeof(KeroseneEngineClusterSmallConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class KeroseneEngineClusterSmallConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                AddPortScipt(go);
            }
        }

        [HarmonyPatch(typeof(KeroseneEngineClusterConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class KeroseneEngineClusterConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                AddPortScipt(go);
            }
        }

        [HarmonyPatch(typeof(SteamEngineClusterConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class SteamEngineClusterConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                AddPortScipt(go);
            }
        }

        [HarmonyPatch(typeof(HydrogenEngineClusterConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class HydrogenEngineClusterConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                AddPortScipt(go);
            }
        }

        [HarmonyPatch(typeof(HEPEngineConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class HEPEngineConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                AddPortScipt(go);
            }
        }
    }
}
