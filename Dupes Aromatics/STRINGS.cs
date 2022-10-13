using STRINGS;

namespace Dupes_Aromatics
{
    class STRINGS
    {
        public class GERMS
        {
            public class ROSESCENT
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Rose Scent", nameof(ROSESCENT));
                public static LocString DESCRIPTION = (LocString)"Pretty nice smell of Roses.\n";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Rose Scent present.\n";
            }
            public class MALLOWSCENT
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Mallow Scent", nameof(MALLOWSCENT));
                public static LocString DESCRIPTION = (LocString)"Pretty nice smell of Mallows.\n";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Mallow Scent present.\n";
            }

            public class LAVENDERSCENT
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Lavender Scent", nameof(LAVENDERSCENT));
                public static LocString DESCRIPTION = (LocString)"Pretty nice smell of Lavenders.\n";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Lavender Scent present.\n";
            }
        }

        public class EFFECTS
        {
            public class SMELLEDROSE
            {
                public static LocString NAME = (LocString)"Smelled Roses";
                public static LocString DESC = (LocString)"This duplicant just smelled some Roses.";
            }
        }
    }
}
