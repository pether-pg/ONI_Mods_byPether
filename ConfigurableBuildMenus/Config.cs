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
                    _instance = JsonSerializer<Config>.Deserialize();
                if (_instance == null)
                {
                    _instance = new Config();
                    _instance.MakeDefaultLists();
                    JsonSerializer<Config>.Serialize(_instance);
                }
                return _instance;
            }
            set
            { _instance = value; }
        }

        private void MakeDefaultLists()
        {
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
                new MoveBuildingItem() { BuildingId = "MonumentTop", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "ParkSign", MoveToMenu = "DecorMenu"},
                new MoveBuildingItem() { BuildingId = "OreScrubber"},
                new MoveBuildingItem() { BuildingId = "CreatureTrap"},
                new MoveBuildingItem() { BuildingId = "FishTrap"},
                new MoveBuildingItem() { BuildingId = "FlyingCreatureBait"}
            };
        }

        private Config()
        {
            NewBuildMenus = new List<NewBuildMenu>();
            MoveBuildingItems = new List<MoveBuildingItem>();
            return;
        }

        public List<NewBuildMenu> NewBuildMenus;
        public List<MoveBuildingItem> MoveBuildingItems;

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
            public string JustAfter;
            public bool OnListBeginning;
        }
    }
}
