using System.IO;
using System.Reflection;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

namespace ConfigurableBuildMenus
{
    class PlanorderHelper
    {
        public static void CreateNewMenu(Config.NewBuildMenu newMenu, Dictionary<HashedString, string> iconNameMap)
        {
            if (string.IsNullOrEmpty(newMenu.MenuId) || iconNameMap == null)
            {
                Debug.Log($"{ModInfo.Namespace}: Could not create new menu...");
                return;
            }

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

        public static bool Exists(string buildingId)
        {
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
            if (Exists(movedItem.BuildingId))
            {
                Remove(movedItem);
                Add(movedItem);
            }
            else
                Debug.Log($"{ModInfo.Namespace}: Building {movedItem.BuildingId} not found in build menus, can't move it to {movedItem.MoveToMenu}");
        }

        public static void Remove(Config.MoveBuildingItem movedItem)
        {
            int oldCategory = FindCategoryIdForBuilding(movedItem.BuildingId);
            if (oldCategory < 0)
            {
                Debug.Log($"{ModInfo.Namespace}: Could not find building {movedItem.BuildingId}");
                return;
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

            string category = "uncategorized"; 
            if(BUILDINGS.PLANSUBCATEGORYSORTING.ContainsKey(movedItem.BuildingId))
                category = BUILDINGS.PLANSUBCATEGORYSORTING[movedItem.BuildingId];
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

        public static string GetIconDirectory()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string dir = Path.Combine(path, "icons");
            return dir;
        }

        public static string GetPathForIcon(string iconName)
        {
            string iconFile = string.Format("{0}.png", iconName);
            string path = Path.Combine(GetIconDirectory(), iconFile);
            return path;
        }

        public static void LoadIcon(Config.NewBuildMenu newBuildMenu)
        {
            HashedString key = new HashedString(newBuildMenu.Icon);
            HashedString keyDisabled = new HashedString(newBuildMenu.Icon + "_disabled");
            string path = GetPathForIcon(newBuildMenu.Icon);
            
            if(!File.Exists(path))
            {
                Debug.Log($"{ModInfo.Namespace}: Could not find file {path}");
                return;
            }
            if(Assets.Sprites.ContainsKey(key))
            {
                Debug.Log($"{ModInfo.Namespace}: Assets.Sprites already contains {newBuildMenu.Icon} icon. Your file will not be loaded.");
                return;
            }

            byte[] data = File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(data);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            sprite.name = newBuildMenu.Icon;
            Assets.Sprites.Add(key, sprite);

            Sprite grayscale = Sprite.Create(Grayscale(tex), new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            grayscale.name = newBuildMenu.Icon;
            Assets.Sprites.Add(keyDisabled, grayscale);

            Debug.Log($"{ModInfo.Namespace}: Loaded icon file {path}");
        }

        private static Texture2D Grayscale(Texture2D source)
        {
            Texture2D result = new Texture2D(source.width, source.height);
            for(int x=0; x<source.width; x++)
                for(int y =0; y<source.height; y++)
                {
                    Color pixel = source.GetPixel(x, y);
                    float gray = 0.2989f * pixel.r 
                                + 0.5870f * pixel.g 
                                + 0.1140f * pixel.b;
                    result.SetPixel(x, y, new Color(gray, gray, gray, pixel.a));
                }
            result.Apply();
            return result;
        }
    }
}
