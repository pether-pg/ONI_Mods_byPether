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
                    MenuId = STRINGS.INSTRUCTION.NEW_MENU.MENU_ID,
                    JustAfter = STRINGS.INSTRUCTION.NEW_MENU.JUST_AFTER,
                    OnListBeginning = STRINGS.INSTRUCTION.NEW_MENU.ON_BEGINING,
                    Name = STRINGS.INSTRUCTION.NEW_MENU.NAME,
                    Tooltip = STRINGS.INSTRUCTION.NEW_MENU.TOOLTIP,
                    Icon = STRINGS.INSTRUCTION.NEW_MENU.ICON
                },
                NewBuildingCategoryInstruction = new InstructionHelper.NewBuildingCategoryHelper()
                { 
                    CategoryId = STRINGS.INSTRUCTION.NEW_CATEGORY.CATEGORY_ID,
                    Name = STRINGS.INSTRUCTION.NEW_CATEGORY.NAME
                },
                MoveBuildingItemInstruction = new InstructionHelper.MoveBuildingItemHelper()
                {
                    BuildingId = STRINGS.INSTRUCTION.MOVE_BUILDING.BUILDING_ID,
                    MoveToMenu = STRINGS.INSTRUCTION.MOVE_BUILDING.MOVE_TO_MENU,
                    Category = STRINGS.INSTRUCTION.MOVE_BUILDING.CATEGORY,
                    JustAfter = STRINGS.INSTRUCTION.MOVE_BUILDING.JUST_AFTER,
                    OnListBeginning = STRINGS.INSTRUCTION.MOVE_BUILDING.ON_BEGINING
                }
            };

            NewBuildMenus = new List<NewBuildMenu>() {
                new NewBuildMenu() { MenuId = "DecorMenu",
                                    JustAfter = "Furniture",
                                    Name = STRINGS.CUSTOM.DECOR_MENU.NAME,
                                    Tooltip = STRINGS.CUSTOM.DECOR_MENU.TOOLTIP,
                                    Icon = "icon_errand_art"
                }
            };

            NewBuildingCategories = new List<NewBuildingCategory>() {
                new NewBuildingCategory() { CategoryId = PlanorderHelper.DEFAULT_CATEGORY_ID, Name = STRINGS.CUSTOM.CATEGORIES.DEFAULT },
                new NewBuildingCategory() { CategoryId = "flowers", Name = STRINGS.CUSTOM.CATEGORIES.FLOWERS },
                new NewBuildingCategory() { CategoryId = "paintings", Name = STRINGS.CUSTOM.CATEGORIES.PAINTINGS },
                new NewBuildingCategory() { CategoryId = "sculptures", Name = STRINGS.CUSTOM.CATEGORIES.SCULPTURES },
                new NewBuildingCategory() { CategoryId = "monument", Name = STRINGS.CUSTOM.CATEGORIES.MONUMENT }
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
