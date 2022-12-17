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
        [HarmonyPatch(new Type[] { typeof(GameObject), typeof(Vector3), typeof(Orientation), typeof(IList<Tag>), typeof(int)})]
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

        [HarmonyPatch(typeof(BuildingDef))]
        [HarmonyPatch("IsValidPlaceLocation")]
        [HarmonyPatch(new Type[] { typeof(GameObject), typeof(int), typeof(Orientation), typeof(bool), typeof(string) },
            new ArgumentType[] { ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Out })]
        public static class CanBuildPatch
        {
            public static void Postfix(BuildingDef __instance, ref bool __result, ref string fail_reason)
            {
                if (__instance.PrefabID == SharedStorageConfig.ID && SharedStorageData.Instance.IsAlreadyBuilt)
                {
                    __result = false;
                    fail_reason = STRINGS.UI.TOOLTIPS.HELP_BUILDLOCATION_ONLY_ONE_STORAGE;
                }
            }
        }
    }
}
