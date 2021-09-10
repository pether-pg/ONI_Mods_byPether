using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using STRINGS;
using System.Collections.Generic;

namespace MultiplayerStorage
{
    class MultiplayerStorage_Patches_Menu
    {
        [HarmonyPatch(typeof(SaveLoader))]
        [HarmonyPatch(new Type[] { typeof(string), typeof(bool), typeof(bool) })]
        [HarmonyPatch("Save")]
        public class SaveLoader_Save_Patch
        {
            public static void Prefix()
            {
                if (SharedStorageData.Instance.IsActive)
                {
                    string path = StorageBinarySerializer.GetFullPath(Settings.Instance.StorageFilePath);
                    StorageBinarySerializer.Serialize(SharedStorageData.GetStorage(), path);
                }
            }
        }

        [HarmonyPatch(typeof(PauseScreen))]
        [HarmonyPatch("OnLoadConfirm")]
        public class PauseScreen_OnLoadConfirm_Patch
        {
            public static void Prefix()
            {
                if (SharedStorageData.Instance.IsActive)
                    StorageOwnershipInfo.ReleaseControl();
            }
        }

        [HarmonyPatch(typeof(PauseScreen))]
        [HarmonyPatch("OnRetireConfirm")]
        public class PauseScreen_OnRetireConfirm_Patch
        {
            public static void Prefix()
            {
                if (SharedStorageData.Instance.IsActive)
                    StorageOwnershipInfo.ReleaseControl();
            }
        }

        [HarmonyPatch(typeof(PauseScreen))]
        [HarmonyPatch("OnQuitConfirm")]
        public class PauseScreen_OnQuitConfirm_Patch
        {
            public static void Prefix()
            {
                if (SharedStorageData.Instance.IsActive)
                    StorageOwnershipInfo.ReleaseControl();
            }
        }

        [HarmonyPatch(typeof(PauseScreen))]
        [HarmonyPatch("OnDesktopQuitConfirm")]
        public class PauseScreen_OnDesktopQuitConfirm_Patch
        {
            public static void Prefix()
            {
                if (SharedStorageData.Instance.IsActive)
                    StorageOwnershipInfo.ReleaseControl();
            }
        }
    }
}
