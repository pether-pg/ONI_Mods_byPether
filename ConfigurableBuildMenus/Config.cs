using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurableBuildMenus
{
    class Config
    {
        public InstructionHelper Instruction;
        public List<NewBuildMenu> NewBuildMenus;
        public List<NewBuildingCategory> NewBuildingCategories;
        public List<MoveBuildingItem> MoveBuildingItems;

        private static Config _instance;

        public static Config Instance
        {
            get
            {
                if (_instance == null)
                    _instance = JsonSerializer<Config>.Deserialize(FilePath());
                if (_instance == null)
                {
                    _instance = new Config();
                    _instance.MakeDefaultLists();
                    if (JsonSerializer<Config>.Dirty == false)
                        JsonSerializer<Config>.Serialize(_instance, FilePath());
                    else
                        Debug.Log($"{ModInfo.Namespace}: Invalid config file - will use default configuration this game, but your file is unchanged");
                }
                return _instance;
            }
            set
            { _instance = value; }
        }

        public static string FilePath()
        {
            string filename = JsonSerializer<Config>.GetDefaultFilename();
            return ConfigPath.Intance.GetPathForFile(filename);
        }

        private void MakeDefaultLists()
        {
            Instruction = new InstructionHelper() {
                NewBuildMenuInstruction = new InstructionHelper.NewBuildMenuHelper()
                {
                    MenuId = "Required. Id for your new menu, pick any one you like.",
                    JustAfter = "ID of a menu. Your menu will be placed just after the specified one. Optional.",
                    OnListBeginning = "If set to true, your menu will be placed on the very beginning, ignoring JustAfter value. Optional.",
                    Name = "Name of your new menu that will be displayed in your game. Can be anything you like.",
                    Tooltip = "Tooltip text that will be displayed above your menu. Can be anything you like.",
                    Icon = "Icon name for your menu. You can use existing game icons or name of the file in the \"icons\" directory under mod path (without .png suffix here)"
                },
                NewBuildingCategoryInstruction = new InstructionHelper.NewBuildingCategoryHelper()
                { 
                    CategoryId = "Required. ID of new category. Pick any one you like",
                    Name = "Name of new category you will see in build menu. Can be anything, but try to keep it short."
                },
                MoveBuildingItemInstruction = new InstructionHelper.MoveBuildingItemHelper()
                {
                    BuildingId = "Required. Must match Id of an existing building you want to customize.",
                    MoveToMenu = "ID of the menu you want to move your building to (by default on the end of the list). Optional - if not provided, the building will be removed",
                    Category = "ID of a category. Buildings are grouped by category in each menu. Must match existing or new category. Optional - original category will be used by default.",
                    JustAfter = "ID of a building. Your building will be placed just after the specified one. Optional.",
                    OnListBeginning = "If set to true, your building will be placed on the very beginning of the menu, ignoring JustAfter value. Optional. In case many buildings have this, all of them will end up on the beginning of the menu and the last building in the config will be first in the menu."
                }
            };

            NewBuildMenus = new List<NewBuildMenu>() {
                new NewBuildMenu() { MenuId = "DecorMenu",
                                    JustAfter = "Furniture",
                                    Name = "Decoration",
                                    Tooltip = "Many pretty things for your base {Hotkey}",
                                    Icon = "icon_errand_art"
                }
            };

            NewBuildingCategories = new List<NewBuildingCategory>() {
                new NewBuildingCategory() { CategoryId = PlanorderHelper.DEFAULT_CATEGORY_ID, Name = "Uncategorized" },
                new NewBuildingCategory() { CategoryId = "flowers", Name = "Flower Pots" },
                new NewBuildingCategory() { CategoryId = "paintings", Name = "Paintings" },
                new NewBuildingCategory() { CategoryId = "sculptures", Name = "Sculptures" },
                new NewBuildingCategory() { CategoryId = "monument", Name = "Monument" }
            };

            MoveBuildingItems = new List<MoveBuildingItem>() {
                new MoveBuildingItem() { BuildingId = "GasReservoir", MoveToMenu = "HVAC"},
                new MoveBuildingItem() { BuildingId = "LiquidReservoir", MoveToMenu = "Plumbing"},
                new MoveBuildingItem() { BuildingId = "StorageLocker", MoveToMenu = "Conveyance", OnListBeginning = true},
                new MoveBuildingItem() { BuildingId = "StorageLockerSmart", MoveToMenu = "Conveyance", JustAfter = "StorageLocker"},
                new MoveBuildingItem() { BuildingId = "ObjectDispenser", MoveToMenu = "Conveyance", JustAfter = "StorageLockerSmart"},
                new MoveBuildingItem() { BuildingId = "WashSink", MoveToMenu = "Plumbing", JustAfter = "WallToilet"},
                new MoveBuildingItem() { BuildingId = "WashBasin", MoveToMenu = "Plumbing", JustAfter = "WallToilet"},
                new MoveBuildingItem() { BuildingId = "BunkerDoor", MoveToMenu = "Rocketry", JustAfter = "Gantry"},
                new MoveBuildingItem() { BuildingId = "ModularLaunchpadPortLiquid", MoveToMenu = "Rocketry"},
                new MoveBuildingItem() { BuildingId = "ModularLaunchpadPortLiquidUnloader", MoveToMenu = "Rocketry"},
                new MoveBuildingItem() { BuildingId = "ModularLaunchpadPortGas", MoveToMenu = "Rocketry"},
                new MoveBuildingItem() { BuildingId = "ModularLaunchpadPortGasUnloader", MoveToMenu = "Rocketry"},
                new MoveBuildingItem() { BuildingId = "ModularLaunchpadPortSolid", MoveToMenu = "Rocketry"},
                new MoveBuildingItem() { BuildingId = "ModularLaunchpadPortSolidUnloader", MoveToMenu = "Rocketry"},
                new MoveBuildingItem() { BuildingId = "FlowerVase", MoveToMenu = "DecorMenu", Category = "flowers"},
                new MoveBuildingItem() { BuildingId = "FlowerVaseWall", MoveToMenu = "DecorMenu", Category = "flowers"},
                new MoveBuildingItem() { BuildingId = "FlowerVaseHanging", MoveToMenu = "DecorMenu", Category = "flowers"},
                new MoveBuildingItem() { BuildingId = "FlowerVaseHangingFancy", MoveToMenu = "DecorMenu", Category = "flowers"},
                new MoveBuildingItem() { BuildingId = "SmallSculpture", MoveToMenu = "DecorMenu", Category = "sculptures"},
                new MoveBuildingItem() { BuildingId = "Sculpture", MoveToMenu = "DecorMenu", Category = "sculptures"},
                new MoveBuildingItem() { BuildingId = "IceSculpture", MoveToMenu = "DecorMenu", Category = "sculptures"},
                new MoveBuildingItem() { BuildingId = "MarbleSculpture", MoveToMenu = "DecorMenu", Category = "sculptures"},
                new MoveBuildingItem() { BuildingId = "MetalSculpture", MoveToMenu = "DecorMenu", Category = "sculptures"},
                new MoveBuildingItem() { BuildingId = "CrownMoulding", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "CornerMoulding", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "Canvas", MoveToMenu = "DecorMenu", Category = "paintings"},
                new MoveBuildingItem() { BuildingId = "CanvasWide", MoveToMenu = "DecorMenu", Category = "paintings"},
                new MoveBuildingItem() { BuildingId = "CanvasTall", MoveToMenu = "DecorMenu", Category = "paintings"},
                new MoveBuildingItem() { BuildingId = "ItemPedestal", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "MonumentBottom", MoveToMenu = "DecorMenu", Category = "monument"},
                new MoveBuildingItem() { BuildingId = "MonumentMiddle", MoveToMenu = "DecorMenu", Category = "monument"},
                new MoveBuildingItem() { BuildingId = "MonumentTop", MoveToMenu = "DecorMenu", Category = "monument"}
            };
        }

        private Config()
        {
            NewBuildMenus = new List<NewBuildMenu>();
            NewBuildingCategories = new List<NewBuildingCategory>();
            MoveBuildingItems = new List<MoveBuildingItem>();
            return;
        }

        public class InstructionHelper
        {
            public NewBuildMenuHelper NewBuildMenuInstruction;
            public NewBuildingCategoryHelper NewBuildingCategoryInstruction;
            public MoveBuildingItemHelper MoveBuildingItemInstruction;

            public class NewBuildMenuHelper
            {
                public string MenuId;
                public string JustAfter;
                public string OnListBeginning;
                public string Name;
                public string Tooltip;
                public string Icon;
            }

            public class NewBuildingCategoryHelper
            {
                public string CategoryId;
                public string Name;
            }

            public class MoveBuildingItemHelper
            {
                public string BuildingId;
                public string MoveToMenu;
                public string Category;
                public string JustAfter;
                public string OnListBeginning;
            }
        }

        public class NewBuildMenu
        {
            public string MenuId;
            public string JustAfter;
            public bool OnListBeginning;
            public string Name;
            public string Tooltip;
            public string Icon;
        }

        public class NewBuildingCategory
        {
            public string CategoryId;
            public string Name;
        }

        public class MoveBuildingItem
        {
            public string BuildingId;
            public string MoveToMenu;
            public string Category;
            public string JustAfter;
            public bool OnListBeginning;
        }
    }
}
