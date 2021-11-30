using System;
using System.Collections.Generic;
using TUNING;

namespace ConfigurableBuildMenus
{
    class PlanorderHelper
    {
        public static void CreateNewMenu(Config.NewBuildMenu newMenu, Dictionary<HashedString, string> iconNameMap)
        {
            if (string.IsNullOrEmpty(newMenu.MenuId) || iconNameMap == null)
                return;

            Debug.Log($"{ModInfo.Namespace}: CreateNewMenu {newMenu.MenuId}");

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

        public static int FindCategoryIdForBuilding(string buildingId)
        {
            for(int i=0; i<BUILDINGS.PLANORDER.Count; i++)
                foreach (string id in BUILDINGS.PLANORDER[i].data)
                    if (id == buildingId)
                        return i;
            return -1;
        }

        public static void Move(Config.MoveBuildingItem movedItem)
        {
            Remove(movedItem);
            Add(movedItem);
        }

        public static void Remove(Config.MoveBuildingItem movedItem)
        {
            int oldCategory = FindCategoryIdForBuilding(movedItem.BuildingId);
            if (oldCategory < 0)
                return;
            BUILDINGS.PLANORDER[oldCategory].data.Remove(movedItem.BuildingId);
        }

        public static void Add(Config.MoveBuildingItem movedItem)
        {
            if (string.IsNullOrEmpty(movedItem.MoveToMenu))
                return;

            int newCategory = BUILDINGS.PLANORDER.FindIndex((x => x.category == (HashedString)movedItem.MoveToMenu));
            if (newCategory < 0)
                return;

            if(movedItem.OnListBeginning)
                BUILDINGS.PLANORDER[newCategory].data.Insert(0, movedItem.BuildingId);
            else if (string.IsNullOrEmpty(movedItem.JustAfter))
                BUILDINGS.PLANORDER[newCategory].data.Add(movedItem.BuildingId);
            else
                for (int i = 0; i < BUILDINGS.PLANORDER[newCategory].data.Count; i++)
                {
                    if (BUILDINGS.PLANORDER[newCategory].data[i] == movedItem.JustAfter)
                    {
                        BUILDINGS.PLANORDER[newCategory].data.Insert(i + 1, movedItem.BuildingId);
                        break;
                    }
                    else if (i == BUILDINGS.PLANORDER[newCategory].data.Count - 1)
                    {
                        BUILDINGS.PLANORDER[newCategory].data.Add(movedItem.BuildingId);
                        break;
                    }
                }
        }
    }
}
