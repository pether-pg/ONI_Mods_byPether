﻿using System.IO;
using System.Reflection;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

namespace ConfigurableBuildMenus
{
    class PlanorderHelper
    {
        public const string DEFAULT_CATEGORY_ID = "uncategorized";

        public static void CreateNewMenu(Config.NewBuildMenu newMenu, Dictionary<HashedString, string> iconNameMap)
        {
            if (string.IsNullOrEmpty(newMenu.MenuId) || iconNameMap == null)
            {
                Debug.Log($"{ModInfo.Namespace}: Could not create new menu...");
                return;
            }

            Debug.Log($"{ModInfo.Namespace}: Create new menu: {newMenu.MenuId}");

            PlanScreen.PlanInfo newPlanInfo = new PlanScreen.PlanInfo((HashedString)newMenu.MenuId, true, new List<string>());

            if (newMenu.OnListBeginning)
                BUILDINGS.PLANORDER.Insert(0, newPlanInfo);
            else if (string.IsNullOrEmpty(newMenu.JustAfter))
                BUILDINGS.PLANORDER.Add(newPlanInfo);
            else 
                for(int i=0; i< BUILDINGS.PLANORDER.Count; i++)
                {
                    if( BUILDINGS.PLANORDER[i].category == (HashedString)newMenu.JustAfter)
                    {
                        BUILDINGS.PLANORDER.Insert(i + 1, newPlanInfo);
                        break;
                    }
                    else if (i == BUILDINGS.PLANORDER.Count - 1)
                    {
                        BUILDINGS.PLANORDER.Add(newPlanInfo);
                        break;
                    }
                }

            Strings.Add($"STRINGS.UI.BUILDCATEGORIES.{newMenu.MenuId.ToUpperInvariant()}.NAME", newMenu.Name);
            Strings.Add($"STRINGS.UI.BUILDCATEGORIES.{newMenu.MenuId.ToUpperInvariant()}.TOOLTIP", newMenu.Tooltip);

            HashedString key = HashCache.Get().Add(newMenu.MenuId);
            if(!iconNameMap.ContainsKey(key))
                iconNameMap.Add(key, newMenu.Icon);
        }

        public static void CreateNewCategory(Config.NewBuildingCategory newCategory)
        {
            if (newCategory == null || string.IsNullOrEmpty(newCategory.CategoryId))
                return;

            Debug.Log($"{ModInfo.Namespace}: Defined new category: {newCategory.CategoryId}");

            Strings.Add($"STRINGS.UI.NEWBUILDCATEGORIES." + newCategory.CategoryId.ToUpper() + ".BUILDMENUTITLE", newCategory.Name);
        }

        public static bool Exists(string buildingId)
        {
            if (string.IsNullOrEmpty(buildingId))
                return false;
            return FindCategoryIdForBuilding(buildingId) >= 0;
        }

        public static int FindCategoryIdForBuilding(string buildingId)
        {
            for (int i = 0; i < BUILDINGS.PLANORDER.Count; i++)
                foreach (KeyValuePair<string, string> pair in BUILDINGS.PLANORDER[i].buildingAndSubcategoryData)
                    if (pair.Key == buildingId)
                        return i;

            for (int i = 0; i < BUILDINGS.PLANORDER.Count; i++)
                foreach (string id in BUILDINGS.PLANORDER[i].data)
                    if (id == buildingId)
                        return i;
            return -1;
        }

        public static void Move(Config.MoveBuildingItem movedItem)
        {
            if(movedItem == null)
            {
                Debug.Log($"{ModInfo.Namespace}: Trying to move null, ignoring...");
                return;
            }

            if (Exists(movedItem.BuildingId) && RequiredDLCsActive(movedItem.BuildingId))
            {
                Remove(movedItem);
                Add(movedItem);
            }
            else
                Debug.Log($"{ModInfo.Namespace}: Building {movedItem.BuildingId} not found in build menus, can't move it to {movedItem.MoveToMenu}");
        }

        public static bool RequiredDLCsActive(string buildingId)
        {
            BuildingDef def = Assets.GetBuildingDef(buildingId);
            if (def == null)
            {
                Debug.Log($"{ModInfo.Namespace}: Could not find {buildingId} in Assets.GetBuildingDef");
                return false;
            }

            List<string> requiredDLCs = new List<string>();
            List<string> forbiddenDLCs = new List<string>();

            if (def.RequiredDlcIds != null)
                foreach (string dlc in def.RequiredDlcIds)
                    requiredDLCs.Add(dlc);

            if (def.ForbiddenDlcIds != null)
                foreach (string dlc in def.ForbiddenDlcIds)
                    forbiddenDLCs.Add(dlc);

            foreach (string dlc in requiredDLCs)
                if (!DlcManager.IsContentSubscribed(dlc))
                    return false;

            foreach (string dlc in forbiddenDLCs)
                if (DlcManager.IsContentSubscribed(dlc))
                    return false;

            return true;
        }

        public static void Remove(Config.MoveBuildingItem movedItem)
        {
            int oldCategory = FindCategoryIdForBuilding(movedItem.BuildingId);
            if (oldCategory < 0)
            {
                Debug.Log($"{ModInfo.Namespace}: Could not find building {movedItem.BuildingId}");
                return;
            }

            // If an existing modded building is removed and added elsewhere, remember its subcategory,
            // unless explicitly given.
            if(string.IsNullOrEmpty(movedItem.Category))
            {
                var planOrderData = BUILDINGS.PLANORDER[oldCategory];
                foreach (KeyValuePair<string, string> datum in planOrderData.buildingAndSubcategoryData)
                    if (datum.Key == movedItem.BuildingId)
                    {
                        movedItem.Category = datum.Value;
                        break;
                    }
            }

            BUILDINGS.PLANORDER[oldCategory].data.RemoveAll(x => x == movedItem.BuildingId);
            BUILDINGS.PLANORDER[oldCategory].buildingAndSubcategoryData.RemoveAll(x => x.Key == movedItem.BuildingId);
        }

        public static void Add(Config.MoveBuildingItem movedItem)
        {
            if (string.IsNullOrEmpty(movedItem.MoveToMenu))
            {
                Debug.Log($"{ModInfo.Namespace}: MoveToMenu empty, {movedItem.BuildingId} will be removed");
                return;
            }

            int newCategory = BUILDINGS.PLANORDER.FindIndex((x => x.category == (HashedString)movedItem.MoveToMenu));
            if (newCategory < 0)
            {
                Debug.Log($"{ModInfo.Namespace}: MoveToMenu = \"{movedItem.MoveToMenu}\" is invalid, {movedItem.BuildingId} will be removed");
                return;
            }

            string category = DEFAULT_CATEGORY_ID; 
            if(BUILDINGS.PLANSUBCATEGORYSORTING.ContainsKey(movedItem.BuildingId))
                category = BUILDINGS.PLANSUBCATEGORYSORTING[movedItem.BuildingId];
            if(!string.IsNullOrEmpty(movedItem.Category))
                category = movedItem.Category;

            KeyValuePair<string, string> movedPair = new KeyValuePair<string, string>(movedItem.BuildingId, category);

            if (movedItem.OnListBeginning)
                BUILDINGS.PLANORDER[newCategory].buildingAndSubcategoryData.Insert(0, movedPair);
            else if (string.IsNullOrEmpty(movedItem.JustAfter))
                BUILDINGS.PLANORDER[newCategory].buildingAndSubcategoryData.Add(movedPair);
            else
                for (int i = 0; i < BUILDINGS.PLANORDER[newCategory].buildingAndSubcategoryData.Count; i++)
                {
                    if (BUILDINGS.PLANORDER[newCategory].buildingAndSubcategoryData[i].Key == movedItem.JustAfter)
                    {
                        BUILDINGS.PLANORDER[newCategory].buildingAndSubcategoryData.Insert(i + 1, movedPair);
                        break;
                    }
                    else if (i == BUILDINGS.PLANORDER[newCategory].buildingAndSubcategoryData.Count - 1)
                    {
                        BUILDINGS.PLANORDER[newCategory].buildingAndSubcategoryData.Add(movedPair);
                        break;
                    }
                }
        }        
    }
}
