using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerStorage
{
    public class STRINGS
    {
        public class TRANSLATION
        {
            public class AUTHOR
            {
                public static LocString NAME = (LocString)"pether.pg";
            }
        }

        public class UI
        {
            public class TOOLTIPS
            {
                public static LocString HELP_BUILDLOCATION_ONLY_ONE_STORAGE = (LocString)"You can have only one building of this kind.";
            }
        }

        public class BUILDINGS
        {
            public class PREFABS
            {
                public class SHAREDSTORAGE
                {
                    public static LocString NAME = (LocString)"Multireality Storage";
                    public static LocString DESC = (LocString)"Enables shared access to resources accross different realities.";
                    public static LocString EFFECT = (LocString)"Share your resources with colonies in different realities.";
                }
            }
        }
    }
}
