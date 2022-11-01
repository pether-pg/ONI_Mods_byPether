using HarmonyLib;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace SignsTagsAndRibbons
{
    class SignsTagsAndRibbons_Patches_DetailScreen
    {
        [HarmonyPatch(typeof(DetailsScreen), "OnPrefabInit")]
        public static class DetailsScreen_OnPrefabInit_Patch
        {
            public static void Postfix()
            {
                FUI_SideScreen.AddClonedSideScreen<SignSideScreen>(
                    "Sign Side Screen",
                    "MonumentSideScreen",
                    typeof(MonumentSideScreen));
            }
        }
    }
}
