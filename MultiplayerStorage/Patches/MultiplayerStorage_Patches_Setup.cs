using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using STRINGS;

namespace MultiplayerStorage
{
    class MultiplayerStorage_Patches_Setup
    {

        [HarmonyPatch(typeof(GeneratedBuildings))]
        [HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
        public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
        {
            public static void Prefix()
            {
                BasicModUtils.MakeStrings(SharedStorageConfig.ID,
                                        STRINGS.BUILDINGS.PREFABS.SHAREDSTORAGE.NAME,
                                        STRINGS.BUILDINGS.PREFABS.SHAREDSTORAGE.DESC,
                                        STRINGS.BUILDINGS.PREFABS.SHAREDSTORAGE.EFFECT);

                BasicModUtils.MakeStatusItemStrings(SharedStorageConfig.statusItemId, STRINGS.STATUSITEMS.REBOOTREQUIRED.NAME, STRINGS.STATUSITEMS.REBOOTREQUIRED.TOOLTIP);

                ModUtil.AddBuildingToPlanScreen("Base", SharedStorageConfig.ID);
            }
        }

        [HarmonyPatch(typeof(Database.Techs))]
        [HarmonyPatch("Init")]
        public static class Techs_Init_Patch
        {
            public static void Postfix(Database.Techs __instance)
            {
                Tech tech = __instance.TryGet("SolidManagement");
                if (tech != null)
                    tech.unlockedItemIDs.Add(SharedStorageConfig.ID);
            }
        }
    }
}
