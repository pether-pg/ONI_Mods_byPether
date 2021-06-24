using HarmonyLib;
using System.Collections.Generic;
using STRINGS;
using System.Reflection;

namespace ResearchRequirements
{
    public class ResearchRequirements_Patches
    {
        [HarmonyPatch(typeof(Research))]
        [HarmonyPatch("SetActiveResearch")]
        public class Research_SetActiveResearch_Patch
        {
            public static void Prefix(ref Tech tech)
            {
                if (tech == null)
                    return;

                // Can use last check status - req was checked while opening research screen to generate tooltips
                if (TechRequirements.Instance.GetTechReq(tech.Id).GetLastUnlockCheck()) 
                    return;

                tech = null;
            }
        }

        [HarmonyPatch(typeof(ResearchCenter))]
        [HarmonyPatch("OnWorkTick")]
        public class ResearchCenter_OnWorkTick_Patch
        {
            public static void Postfix(ResearchCenter __instance)
            {
                GVD.VersionAlert(true);
                /*
                 * Works vor vanilla
                 * 
                TechInstance activeResearch = Research.Instance.GetActiveResearch();
                Tech tech = activeResearch.tech;
                TechRequirements.TechReq req = TechRequirements.Instance.GetTechReq(tech.Id);

                if (req.ContinuousCheck)
                    if(!req.ReqUnlocked())
                    {
                        __instance.StopWork(__instance.worker, true);
                        ResearchScreen researchScreen = (ResearchScreen)ManagementMenu.Instance.researchScreen;
                        researchScreen.CancelResearch();
                        Research.Instance.SetActiveResearch(null, true);
                    }
                */

                /*
                 * Works vor DLC
                 * */
                TechInstance activeResearch = Research.Instance.GetActiveResearch();
                Tech tech = activeResearch.tech;
                TechRequirements.TechReq req = TechRequirements.Instance.GetTechReq(tech.Id);

                if (req.ContinuousCheck)
                    if (!req.ReqUnlocked())
                    {
                        __instance.StopWork(__instance.worker, true);
                        ResearchScreen researchScreen = Traverse.Create(ManagementMenu.Instance).Field("researchScreen").GetValue<ResearchScreen>();
                        if (researchScreen == null)
                            return;
                        researchScreen.CancelResearch();
                        Research.Instance.SetActiveResearch(null, true);
                        Debug.Log("ResearchRequirements: research canceled");
                    }
                
            }
        }

        [HarmonyPatch(typeof(ManagementMenu))]
        [HarmonyPatch("ToggleScreen")]
        public class ManagementMenu_ToggleScreen_Patch
        {
            public static void Postfix(ManagementMenu.ScreenData screenData, ManagementMenu __instance)
            {
                KIconToggleMenu.ToggleInfo researchInfo = Traverse.Create(__instance).Field("researchInfo").GetValue<KIconToggleMenu.ToggleInfo>();
                ManagementMenu.ScreenData activeScreen = Traverse.Create(__instance).Field("activeScreen").GetValue<ManagementMenu.ScreenData>();

                if (screenData.toggleInfo == researchInfo && activeScreen == screenData)
                {
                    RequirementFunctions.CountResourcesInReservoirs();
                    ResearchScreen researchScreen = Traverse.Create(ManagementMenu.Instance).Field("researchScreen").GetValue<ResearchScreen>();
                    if (researchScreen == null)
                        return;
                    Dictionary<Tech, ResearchEntry> entryMap = Traverse.Create(researchScreen).Field("entryMap").GetValue<Dictionary<Tech, ResearchEntry>>();
                    foreach (Tech tech in entryMap.Keys)
                    {
                        if(!tech.IsComplete())
                        {
                            LocText researchName = Traverse.Create(entryMap[tech]).Field("researchName").GetValue<LocText>();
                            researchName.GetComponent<ToolTip>().toolTip = CreateTechTooltipText(tech);
                        }
                    }
                }
            }

            public static string CreateTechTooltipText(Tech targetTech)
            {
                string str1 = "";
                foreach (TechItem unlockedItem in targetTech.unlockedItems)
                {
                    if (str1 != "")
                        str1 += ", ";
                    str1 += unlockedItem.Name;
                }
                string str3 = string.Format((string)UI.RESEARCHSCREEN_UNLOCKSTOOLTIP, (object)str1);                
                string customDesc = TechRequirements.Instance.GetTechReq(targetTech.Id).GetDescription();

                return string.Format("{0}\n{1}\n\n{2}\n\n{3}", (object)targetTech.Name, (object)targetTech.desc, (object)str3, customDesc);
            }
        }
    }
}
