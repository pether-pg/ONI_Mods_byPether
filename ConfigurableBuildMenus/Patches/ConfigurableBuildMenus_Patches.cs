using HarmonyLib;
using System.Collections.Generic;
using TUNING;
using System;

namespace ConfigurableBuildMenus
{
    public class ConfigurableBuildMenus_Patches
    {
        [HarmonyPatch(typeof(GeneratedBuildings))]
        [HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
        public class GeneratedBuildings_LoadGeneratedBuildings_Patch
        {
            public static void Finalizer()
            {
                JsonSerializer<ExistingBuildingIDs>.Serialize(new ExistingBuildingIDs());

                Dictionary<HashedString, string> iconNameMap = Traverse.Create(typeof(PlanScreen)).Field("iconNameMap").GetValue<Dictionary<HashedString, string>>();
                if (iconNameMap == null)
                    Debug.Log($"{ModInfo.Namespace}: Error - iconNameMap == null"); // Do not stop the flow, this is only for error logging

                if (Config.Instance.NewBuildMenus != null)
                    foreach (Config.NewBuildMenu newMenu in Config.Instance.NewBuildMenus)
                        PlanorderHelper.CreateNewMenu(newMenu, iconNameMap);

                if(Config.Instance.NewBuildingCategories != null)
                    foreach (Config.NewBuildingCategory newCategory in Config.Instance.NewBuildingCategories)
                        PlanorderHelper.CreateNewCategory(newCategory);

                if (Config.Instance.MoveBuildingItems != null)
                    foreach (Config.MoveBuildingItem movedItem in Config.Instance.MoveBuildingItems)
                        PlanorderHelper.Move(movedItem);
            }
        }

        [HarmonyPatch(typeof(Assets))]
        [HarmonyPatch("OnPrefabInit")]
        public class Assets_OnPrefabInit_Patch
        {
            public static void Postfix()
            {
                foreach (Config.NewBuildMenu newBuildMenu in Config.Instance.NewBuildMenus)
                    SpriteHelper.LoadBuilMenuIcon(newBuildMenu);
            }
        }
    }
}
