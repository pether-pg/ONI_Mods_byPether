using Database;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using static InventoryOrganization;

namespace IlluminationSensor
{
    // Based on code by SGT_Imalas:
    // https://github.com/Sgt-Imalas/Sgt_Imalas-Oni-Mods/tree/master/CrownMouldSkin
    class IlluminationSensor_Patches_Skins
    {
        public const string SUBCATEGORY_ID = "BUILDING_LIGHT_SENSOR";
        public static readonly string[] IlluminationSensorIds = { "IlluminationSensor" };

        public class AddNewSkins
        {
            // manually patching, because referencing BuildingFacades class will load strings too early
            public static void Patch(Harmony harmony)
            {
                var targetType = AccessTools.TypeByName("Database.BuildingFacades");
                var target = AccessTools.Constructor(targetType, new[] { typeof(ResourceSet) });
                var postfix = AccessTools.Method(typeof(InitFacadePatchForLightSensor), "PostfixPatch");

                harmony.Patch(target, postfix: new HarmonyMethod(postfix));
            }

            public class InitFacadePatchForLightSensor
            {
                public static void PostfixPatch(object __instance)
                {
                    var resource = (ResourceSet<BuildingFacadeResource>)__instance;
                    AddFacade(resource,
                        IlluminationSensorIds[0], 
                        STRINGS.ILLUMINATIONSENSOR.NAME, 
                        STRINGS.ILLUMINATIONSENSOR.DESCRIPTION, 
                        PermitRarity.Universal, 
                        LogicLightSensorConfig.ID, 
                        "luxsensor_kanim");
                }

                public static void AddFacade(
                    ResourceSet<BuildingFacadeResource> set,
                    string id,
                    LocString name,
                    LocString description,
                    PermitRarity rarity,
                    string prefabId,
                    string animFile,
                    Dictionary<string, string> workables = null)
                {
                    set.resources.Add(new BuildingFacadeResource(id, name, description, rarity, prefabId, animFile, workables));
                }
            }
        }

        [HarmonyPatch(typeof(Db), "Initialize")]
        public static class Assets_OnPrefabInit_Patch
        {
            public static void Prefix()
            {
                AddNewSkins.Patch(ModInfo.HarmonyInstance);
                SublevelCategoryPatch.Patch(ModInfo.HarmonyInstance);
            }
        }

        // [HarmonyPatch(typeof(InventoryOrganization), "GenerateSubcategories")]
        public static class SublevelCategoryPatch
        {
            public static void Patch(Harmony harmony)
            {
                var targetType = AccessTools.TypeByName("InventoryOrganization");
                var target = AccessTools.Method(targetType, "GenerateSubcategories");
                var postfix = AccessTools.Method(typeof(SublevelCategoryPatch), "PostfixMethod");

                harmony.Patch(target, postfix: new HarmonyMethod(postfix));
            }

            public static void PostfixMethod()
            {
                AddSubcategory(InventoryPermitCategories.BUILDINGS, SUBCATEGORY_ID, Def.GetUISprite(LogicLightSensorConfig.ID).first, 132, IlluminationSensorIds);
            }

            public static void AddSubcategory(string mainCategory, string subcategoryID, Sprite icon, int sortkey, string[] permitIDs)
            {

                if (categoryIdToSubcategoryIdsMap.ContainsKey(mainCategory))
                {
                    if (!subcategoryIdToPermitIdsMap.ContainsKey(subcategoryID))
                    {
                        subcategoryIdToPresentationDataMap.Add(subcategoryID, new SubcategoryPresentationData(subcategoryID, icon, sortkey));
                        subcategoryIdToPermitIdsMap.Add(subcategoryID, new HashSet<string>());

                        if (!categoryIdToSubcategoryIdsMap[mainCategory].Contains(subcategoryID))
                            categoryIdToSubcategoryIdsMap[mainCategory].Add(subcategoryID);
                        else
                            Debug.Log("How did this happen? subcategory is registered to this main category but didnt exist?!");

                    }
                    else
                    {
                        Debug.Log("Supply Closet Item subcategory already existing! Use AddItemsToSubcategory instead");
                    }
                    for (int i = 0; i < permitIDs.Length; i++)
                    {
                        subcategoryIdToPermitIdsMap[subcategoryID].Add(permitIDs[i]);
                    }
                }
                else
                    Debug.Log("Supply Closet Main Category not found!");
            }
        }


        [HarmonyPatch(typeof(KleiPermitDioramaVis), "GetPermitVisTarget")]
        public static class KleiPermitDioramaVis_GetPermitVisTarget_Patch
        {
            public static void Postfix(ref IKleiPermitDioramaVisTarget __result, KleiPermitDioramaVis __instance, PermitResource permit)
            {
                if(permit.Category == PermitCategory.Building)
                {
                    (bool hasValue6, BuildLocationRule buildLocationRule2) = KleiPermitVisUtil.GetBuildLocationRule(permit);
                    if(hasValue6 && buildLocationRule2 == BuildLocationRule.Anywhere)
                    {
                        KleiPermitDioramaVis_PedestalAndItem pedestalAndItemVis;
                        pedestalAndItemVis = Traverse.Create(__instance).Field("pedestalAndItemVis").GetValue<KleiPermitDioramaVis_PedestalAndItem>();
                        if(pedestalAndItemVis != null)
                            __result = pedestalAndItemVis;
                    }
                }
            }
        }
    }
}
