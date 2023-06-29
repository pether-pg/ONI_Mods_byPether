using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurableBuildMenus
{
    class Config
    {
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
                    MenuId = "required. Id for your new menu, pick any one you like.",
                    JustAfter = "Id of a menu. Your menu will be placed just after the specified one. Optional.",
                    OnListBeginning = "if set to true, your menu will be placed on the very beginning, ignoring JustAfter value. Optional.",
                    Name = "Name of your new menu that will be displayed in your game. Can be anything you like.",
                    Tooltip = "Tooltip text that will be displayed above your menu. Can be anything you like.",
                    Icon = "icon name for your menu. You can use existing game icons or name of the file in the \"icons\" directory under mod path (without .png suffix here)"
                },
                MoveBuildingItemInstruction = new InstructionHelper.MoveBuildingItemHelper()
                {
                    BuildingId = "required. Must match Id of an existing building you want to customize.",
                    MoveToMenu = "Id of the menu you want to move your building to (by default on the end of the list). Optional - if not provided, the building will be removed",
                    Category = "Id of a building category. Buildings are grouped by category in each menu. Optional, original category or 'uncategorized' used if not provided",
                    JustAfter = "Id of a building. Your building will be placed just after the specified one. Optional.",
                    OnListBeginning = "if set to true, your building will be placed on the very beginning of the menu, ignoring JustAfter value. Optional. In case many buildings have this, all of them will end up on the beginning of the menu and the last building in the config will be first in the menu."
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
                new MoveBuildingItem() { BuildingId = "FlowerVase", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "FlowerVaseWall", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "FlowerVaseHanging", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "FlowerVaseHangingFancy", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "SmallSculpture", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "Sculpture", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "IceSculpture", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "MarbleSculpture", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "MetalSculpture", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "CrownMoulding", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "CornerMoulding", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "Canvas", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "CanvasWide", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "CanvasTall", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "ItemPedestal", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "MonumentBottom", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "MonumentMiddle", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "MonumentTop", MoveToMenu = "DecorMenu"}
            };
        }

        private Config()
        {
            NewBuildMenus = new List<NewBuildMenu>();
            MoveBuildingItems = new List<MoveBuildingItem>();
            return;
        }

        public InstructionHelper Instruction;
        public List<NewBuildMenu> NewBuildMenus;
        public List<MoveBuildingItem> MoveBuildingItems;

        public class InstructionHelper
        {
            public NewBuildMenuHelper NewBuildMenuInstruction;
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
