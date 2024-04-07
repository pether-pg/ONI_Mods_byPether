using HarmonyLib;
using UnityEngine;
using System;
using System.Collections.Generic;
using static DetailsScreen;

namespace SignsTagsAndRibbons
{
    // Using Aki's code from https://github.com/aki-art/ONI-Mods/blob/master/Futility/FUI/SideScreen.cs
    class FUI_SideScreen
    {
        public static void AddClonedSideScreen<T>(string name, string originalName, Type originalType)
        {
            bool elementsReady = GetElements(out List<SideScreenRef> screens, out GameObject contentBody);
            if (elementsReady)
            {
                var oldPrefab = FindOriginal(originalName, screens);
                var newPrefab = Copy<T>(oldPrefab, contentBody, name, originalType);

                screens.Add(NewSideScreen(name, newPrefab));
            }
        }

        private static bool GetElements(out List<SideScreenRef> screens, out GameObject contentBody)
        {
            var detailsScreen = Traverse.Create(DetailsScreen.Instance);
            screens = detailsScreen.Field("sideScreens").GetValue<List<DetailsScreen.SideScreenRef>>();
            contentBody = detailsScreen.Field("sideScreenConfigContentBody").GetValue<GameObject>();

            return screens != null && contentBody != null;
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
            //foreach (var screen in screens)
            //    Debug.Log(screen.name + screen.GetType());

            var result = screens.Find(s => s.name == name).screenPrefab;

            if (result == null)
                Debug.LogWarning("Could not find a sidescreen with the name " + name);

            return result;
        }

        private static DetailsScreen.SideScreenRef NewSideScreen(string name, SideScreenContent prefab)
        {
            return new DetailsScreen.SideScreenRef
            {
                name = name,
                offset = Vector2.zero,
                screenPrefab = prefab
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
