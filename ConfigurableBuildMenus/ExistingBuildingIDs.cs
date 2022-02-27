using System;
using System.Collections.Generic;
using TUNING;
using HarmonyLib;

namespace ConfigurableBuildMenus
{
    class ExistingBuildingIDs
    {
        public List<BuildMenu> BuildMenus;

        public class BuildMenu
        {
            public string Name;
            public string Icon;
            public List<KeyValuePair<string, string>> BuildingsAndCategories;
        }

        public ExistingBuildingIDs()
        {
            BuildMenus = new List<BuildMenu>();
            List<string> buildingCategories = new List<string>() 
            { 
                "Base",
                "Oxygen",
                "Power",
                "Food",
                "Plumbing",
                "HVAC",
                "Refining",
                "Medical",
                "Furniture",
                "Equipment",
                "Utilities",
                "Automation",
                "Conveyance",
                "Rocketry",
                "HEP" 
            };

            for(int i=0; i< BUILDINGS.PLANORDER.Count; i++)
            {
                int index = buildingCategories.FindIndex(x => (HashedString)x == BUILDINGS.PLANORDER[i].category);
                Dictionary<HashedString, string> iconNameMap = Traverse.Create(typeof(PlanScreen)).Field("iconNameMap").GetValue<Dictionary<HashedString, string>>();
                BuildMenus.Add(new BuildMenu()
                {
                    Name = index >= 0 ? buildingCategories[index] : "(unknown category)",
                    Icon = iconNameMap != null && iconNameMap.ContainsKey(BUILDINGS.PLANORDER[i].category) ? iconNameMap[BUILDINGS.PLANORDER[i].category] : "(unknown icon)",
                    BuildingsAndCategories = new List<KeyValuePair<string, string>>(BUILDINGS.PLANORDER[i].buildingAndSubcategoryData)
                });
            }
        }
    }
}
