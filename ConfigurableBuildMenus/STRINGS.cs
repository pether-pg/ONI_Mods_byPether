using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurableBuildMenus
{
    class STRINGS
    {
        public class INSTRUCTION
        {
            public class NEW_MENU
            {
                public static LocString MENU_ID = "Required. Id for your new menu, pick any one you like.";
                public static LocString JUST_AFTER = "ID of a menu. Your menu will be placed just after the specified one. Optional.";
                public static LocString ON_BEGINING = "If set to true, your menu will be placed on the very beginning, ignoring JustAfter value. Optional.";
                public static LocString NAME = "Name of your new menu that will be displayed in your game. Can be anything you like.";
                public static LocString TOOLTIP = "Tooltip text that will be displayed above your menu. Can be anything you like.";
                public static LocString ICON = "Icon name for your menu. You can use existing game icons or name of the file in the \"icons\" directory under mod path (without .png suffix here)";
            }

            public class NEW_CATEGORY
            {
                public static LocString CATEGORY_ID = "Required. ID of new category. Pick any one you like";
                public static LocString NAME = "Name of new category you will see in build menu. Can be anything, but try to keep it short.";
            }

            public class MOVE_BUILDING
            {
                public static LocString BUILDING_ID = "Required. Must match Id of an existing building you want to customize.";
                public static LocString MOVE_TO_MENU = "ID of the menu you want to move your building to (by default on the end of the list). Optional - if not provided, the building will be removed";
                public static LocString CATEGORY = "ID of a category. Buildings are grouped by category in each menu. Must match existing or new category. Optional - original category will be used by default.";
                public static LocString JUST_AFTER = "ID of a building. Your building will be placed just after the specified one. Optional.";
                public static LocString ON_BEGINING = "If set to true, your building will be placed on the very beginning of the menu, ignoring JustAfter value. Optional. In case many buildings have this, all of them will end up on the beginning of the menu and the last building in the config will be first in the menu.";
            }
        }

        public class CUSTOM
        {
            public class DECOR_MENU
            {
                public static LocString NAME = "Decoration";
                public static LocString TOOLTIP = "Many pretty things for your base {Hotkey}";
            }

            public class CATEGORIES
            {
                public static LocString DEFAULT = "Uncategorized";
                public static LocString FLOWERS = "Flower Pots";
                public static LocString PAINTINGS = "Paintings";
                public static LocString SCULPTURES = "Sculptures";
                public static LocString MONUMENT = "Monument";
            }
        }

        public class TRANSLATION
        {
            public class AUTHOR
            {
                public static LocString NAME = "pether.pg";
            }
        }
    }
}
