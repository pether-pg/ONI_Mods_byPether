using HarmonyLib;
using UnityEngine;

namespace InterplanarAutomation
{
    public class InterplanarAutomation_Patches : KMod.UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);

            Debug.Log($"{GetType().Namespace}: Loaded from: {this.mod.ContentPath}");
            Debug.Log($"{GetType().Namespace}: Mod version: {this.mod.packagedModInfo.version} " +
                        $"supporting game build {this.mod.packagedModInfo.lastWorkingBuild} ({this.mod.packagedModInfo.supportedContent})");
        }

        [HarmonyPatch(typeof(GeneratedBuildings))]
        [HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
        public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
        {
            public static void Prefix()
            {
                BasicModUtils.MakeStrings(RadioTowerConfig.ID,
                                        RadioTowerConfig.Name,
                                        RadioTowerConfig.Description,
                                        RadioTowerConfig.Effect);

                ModUtil.AddBuildingToPlanScreen("Automation", RadioTowerConfig.ID);
            }
        }

        [HarmonyPatch(typeof(Database.Techs))]
        [HarmonyPatch("Init")]
        public static class Techs_Init_Patch
        {
            public static void Postfix(Database.Techs __instance)
            {
                Tech tech = __instance.TryGet("Multiplexing");
                tech.unlockedItemIDs.Add(RadioTowerConfig.ID);
            }
        }

        [HarmonyPatch(typeof(WarpConduitReceiverConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public class WarpConduitReceiverConfig_CreateBuildingDef_Patch
        {
            public static void Postfix(ref BuildingDef __result)
            {
                WarpBridgeData.AddOutputPort(ref __result);
            }
        }

        [HarmonyPatch(typeof(WarpConduitReceiverConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class WarpConduitReceiverConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(ref GameObject go)
            {
                LogicRibbonWarpBridge bridge = go.AddOrGet<LogicRibbonWarpBridge>();
                bridge.receiver = go;
            }
        }

        [HarmonyPatch(typeof(WarpConduitSenderConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public class WarpConduitSenderConfig_CreateBuildingDef_Patch
        {
            public static void Postfix(ref BuildingDef __result)
            {
                WarpBridgeData.AddInputPort(ref __result);
            }
        }

        [HarmonyPatch(typeof(WarpConduitSender))]
        [HarmonyPatch("FindPartner")]
        public class WarpConduitSender_FindPartner_Patch
        {
            public static void Postfix(ref WarpConduitSender __instance)
            {
                if (__instance.receiver == null) return;
                LogicRibbonWarpBridge bridge = __instance.receiver.gameObject.AddOrGet<LogicRibbonWarpBridge>();
                bridge.sender = __instance.gameObject;
            }
        }
    }
}
