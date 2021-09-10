using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using STRINGS;
using System.Collections.Generic;

namespace MultiplayerStorage
{
    public class MultiplayerStorage_Patches
    {
        [HarmonyPatch(typeof(BuildingDef))]
        [HarmonyPatch("TryPlace")]
        public class BuildingDef_TryPlace_Patch
        {
            public static void Postfix(GameObject __result)
            {
                if (__result != null && __result.name.Contains(SharedStorageConfig.ID))
                    SharedStorageData.Instance.UnderConstruction = __result;
            }
        }

        [HarmonyPatch(typeof(Storage))]
        [HarmonyPatch("OnQueueDestroyObject")]
        public class Storage_OnQueueDestroyObject_Patch
        {
            public static void Prefix(Storage __instance)
            {
                if (__instance != null && SharedStorageData.Instance != null && __instance.gameObject == SharedStorageData.Instance.GO)
                {
                    SharedStorageData.ClearItems();
                    Debug.Log("MultiplayerStorage: items cleared from destroyed Storage");
                }
            }
        }

        [HarmonyPatch]
        public static class BuildingDef_IsValidBuildLocation_Patch
        {
            internal static MethodBase TargetMethod()
            {
                return typeof(BuildingDef).GetMethod(
                    nameof(BuildingDef.IsValidPlaceLocation),
                    new Type[] { typeof(GameObject), typeof(int), typeof(Orientation), typeof(bool), typeof(string).MakeByRefType() });
            }

            private static string originalMessage = string.Empty;
            private static bool messageModified = false;

            public static void Prefix(GameObject source_go, ref int cell)
            {
                if (source_go != null && source_go.name.Contains(SharedStorageConfig.ID) 
                    && (SharedStorageData.Instance.GO != null || SharedStorageData.Instance.UnderConstruction != null))
                {
                    // This is a workaround - I coulnt't access out string argument in a patch
                    // To make this work, I change one of the working messages and fake error to trigger the text.
                    // This code is bad and I feel bad, it should be improved, but hey - it works!

                    cell = Grid.InvalidCell;
                    if (string.IsNullOrEmpty(originalMessage))
                        originalMessage = UI.TOOLTIPS.HELP_BUILDLOCATION_INVALID_CELL;
                    UI.TOOLTIPS.HELP_BUILDLOCATION_INVALID_CELL = STRINGS.UI.TOOLTIPS.HELP_BUILDLOCATION_ONLY_ONE_STORAGE;
                    messageModified = true;
                }
            }

            public static void Postfix()
            {
                if(messageModified)
                {
                    UI.TOOLTIPS.HELP_BUILDLOCATION_INVALID_CELL = originalMessage;
                    messageModified = false;
                }
            }
        }
    }
}
