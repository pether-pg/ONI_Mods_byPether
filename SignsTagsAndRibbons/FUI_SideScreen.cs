using HarmonyLib;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using static DetailsScreen;

namespace SignsTagsAndRibbons
{
    // Using Aki's code from https://github.com/aki-art/ONI-Mods/blob/master/Futility/FUI/SideScreen.cs
    class FUI_SideScreen
    {
        public static GameObject AddClonedSideScreen<T>(string name, string originalName, Type originalType, SidescreenTabTypes targetTab = SidescreenTabTypes.Config)
        {
            bool elementsReady = GetElements(out List<SideScreenRef> screens, out var tabs);
            if (elementsReady)
            {
                GameObject contentBody = GetContentBodyForTab(targetTab, tabs);
                var oldPrefab = FindOriginal(originalName, screens);
                var newPrefab = Copy<T>(oldPrefab, contentBody, name, originalType);

                screens.Add(NewSideScreen(name, newPrefab, targetTab));
                return contentBody;
            }
            return null;
        }

        public static void AddCustomSideScreen<T>(string name, GameObject prefab, SidescreenTabTypes targetTab = SidescreenTabTypes.Config)
        {
            bool elementsReady = GetElements(out List<SideScreenRef> screens, out var tabs);
            if (elementsReady)
            {
                var newScreen = prefab.AddComponent(typeof(T)) as SideScreenContent;
                screens.Add(NewSideScreen(name, newScreen, targetTab));
            }
            else
                Debug.LogWarning($"{ModInfo.Namespace}: Couldnt add custom sidescreen {name}, sidescreen vars not found");
        }

        private static bool GetElements(out List<SideScreenRef> screens, out List<SidescreenTab> tabs)
        {
            var detailsScreen = Traverse.Create(DetailsScreen.Instance);
            screens = detailsScreen.Field("sideScreens").GetValue<List<SideScreenRef>>();
            tabs = detailsScreen.Field("sidescreenTabs").GetValue<SidescreenTab[]>().ToList();
            return screens != null && tabs != null;
        }

        private static GameObject GetContentBodyForTab(SidescreenTabTypes targetTab, List<SidescreenTab> tabs)
        {
            foreach (var tab in tabs)
            {
                if (tab.type == targetTab)
                {
                    return tab.bodyInstance;
                }
            }
            Debug.LogWarning($"{ModInfo.Namespace}: {targetTab} not found!");
            return null;
        }

        private static SideScreenContent Copy<T>(SideScreenContent original, GameObject contentBody, string name, Type originalType)
        {
            var screen = Util.KInstantiateUI<SideScreenContent>(original.gameObject, contentBody).gameObject;
            UnityEngine.Object.Destroy(screen.GetComponent(originalType));

            var prefab = screen.AddComponent(typeof(T)) as SideScreenContent;
            prefab.name = name.Trim();

            screen.SetActive(false);
            return prefab;
        }

        private static SideScreenContent FindOriginal(string name, List<SideScreenRef> screens)
        {
            var result = screens.Find(s => s.name == name).screenPrefab;

            if (result == null)
                Debug.LogWarning($"{ModInfo.Namespace}: Could not find a sidescreen with the name {name}");

            return result;
        }

        private static SideScreenRef NewSideScreen(string name, SideScreenContent prefab, SidescreenTabTypes targetTab)
        {
            return new SideScreenRef
            {
                name = name,
                offset = Vector2.zero,
                screenPrefab = prefab,
                tab = targetTab
            };
        }

        // Using Aki's code from https://github.com/aki-art/ONI-Mods/blob/master/Futility/FUI/Helper.cs#L44
        public static ToolTip AddSimpleToolTip(GameObject gameObject, string message, bool alignCenter = false, float wrapWidth = 0)
        {
            if (gameObject.GetComponent<ToolTip>() != null)
            {
                Debug.Log("GO already had a tooltip! skipping");
                return null;
            }

            ToolTip toolTip = gameObject.AddComponent<ToolTip>();
            toolTip.tooltipPivot = alignCenter ? new Vector2(0.5f, 0f) : new Vector2(1f, 0f);
            toolTip.tooltipPositionOffset = new Vector2(0f, 20f);
            toolTip.parentPositionAnchor = new Vector2(0.5f, 0.5f);

            if (wrapWidth > 0)
            {
                toolTip.WrapWidth = wrapWidth;
                toolTip.SizingSetting = ToolTip.ToolTipSizeSetting.MaxWidthWrapContent;
            }
            ToolTipScreen.Instance.SetToolTip(toolTip);
            toolTip.SetSimpleTooltip(message);
            return toolTip;
        }
    }
}
