using UnityEngine; 
using HarmonyLib;
using System;
using System.Collections.Generic;

namespace SidequestMod
{
    class QuestScreen_Patches
    {
        static ManagementMenu.ManagementMenuToggleInfo info = new ManagementMenu.ManagementMenuToggleInfo("Quests", "OverviewUI_research_nav_icon", tooltip: "Quests - tooltip");

        [HarmonyPatch(typeof(ManagementMenu))]
        [HarmonyPatch("OnPrefabInit")]
        public class ManagementMenu_OnPrefabInit_Patch
        {
            public static void Postfix()
            {
                Traverse.Create(ManagementMenu.Instance).Method("AddToggleTooltip", info, null).GetValue();

                Dictionary<ManagementMenu.ManagementMenuToggleInfo, ManagementMenu.ScreenData> ScreenInfoMatch;
                ScreenInfoMatch = Traverse.Create(ManagementMenu.Instance).Field("ScreenInfoMatch").GetValue<Dictionary<ManagementMenu.ManagementMenuToggleInfo, ManagementMenu.ScreenData>>();
                
                ScreenInfoMatch.Add(info, new ManagementMenu.ScreenData()
                {
                    screen = CustomSettingsController.Instance,
                    tabIdx = 0,
                    toggleInfo = info,
                    cancelHandler = (Func<bool>)null
                });
            }
        }


        [HarmonyPatch(typeof(KIconToggleMenu))]
        [HarmonyPatch("Setup")]
        [HarmonyPatch(new Type[] { typeof(IList<KIconToggleMenu.ToggleInfo>) })]
        public class KIconToggleMenu_Setup_Patch
        {
            public static void Prefix(IList<KIconToggleMenu.ToggleInfo> toggleInfo)
            {
                if (ContainsVitals(toggleInfo))
                    toggleInfo.Add(info);
            }

            private static bool ContainsVitals(IList<KIconToggleMenu.ToggleInfo> toggleInfo)
            {
                foreach (var inf in toggleInfo)
                    if (inf.icon == "OverviewUI_vitals_icon")
                        return true;
                return false;
            }
        }
    }
}
