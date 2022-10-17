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

        public class SEEDS
        {
            public class SPINOSA
            {
                public static LocString SEED_NAME = "Spinosa Black Seed";
                public static LocString SEED_DESC = "The black seed of a " + UI.FormatAsLink("Spinosa", "SpinosaPlant") + ".";
            }

            public class DUSKLAVENDER
            {
                public static LocString SEED_NAME = "Dusk Seed";
                public static LocString SEED_DESC = "The tiny seed of a " + UI.FormatAsLink("Duskbloom Lavender", "DuskbloomLavender") + ".";
            }

            public class RIMEDMALLOW
            {
                public static LocString SEED_NAME = "Iced Mallow Seed";
                public static LocString SEED_DESC = "SEED_DESC";
            }
        }

        public class PLANTS
        {
            public class SPINOSA
            {
                public static LocString NAME = "Blooming Spinosa";
                public static LocString DESC = string.Concat(new string[] { "A rather thorny sten plant that blooms with a beautiful " + UI.FormatAsLink("Spinosa Rose", "SpinosaRose") + "." });
                public static LocString DOMESTICATED_DESC = DESC + "/n/n In domesticated environment this crop requires copious amounts of" + UI.FormatAsLink("Water", "WATER") + ", and" + UI.FormatAsLink("Dirt", "DIRT") + "as fertilizer. Also requires direct exposure to sunlight.";
            }

            public class SUPERSPINOSA
            {
                public static LocString NAME = "Fruiting Spinosa";
                public static LocString DESC = string.Concat(new string[] { "A rather thorny sten plant that produces an edible " + UI.FormatAsLink("Spinosa Hips", "SpinosaHips") + "." });
                public static LocString DOMESTICATED_DESC = string.Concat(new string[] { "/n/n This domesticated plant requires copious amounts of" + UI.FormatAsLink("Water", "WATER") + ", and" + UI.FormatAsLink("Dirt", "DIRT") + "as fertilizer. Also requires direct exposure to sunlight." });
            }

            public class DUSKLAVENDER
            {
                public static LocString NAME = "Duskbloom Lavender";
                public static LocString DESC = string.Concat(new string[] { "A shrub-like plant blooms with a beautiful " + UI.FormatAsLink("Duskbloom", "Duskbloom") + "." });
                public static LocString DOMESTICATED_DESC = string.Concat(new string[] { "/n/n In domesticated environment this crop requires the use of " + UI.FormatAsLink("Phosphorite", "PHOSPHORITE") + " as fertilization." });
            }

            public class SUPERDUSKLAVENDER
            {
                public static LocString NAME = "Duskberry Lavender";
                public static LocString DESC = string.Concat(new string[] { "A shrub-like plant sprouts with an edible " + UI.FormatAsLink("Duskberry", "Duskberry") + "." });
                public static LocString DOMESTICATED_DESC = string.Concat(new string[] { "/n/n In domesticated environment this crop requires the use of " + UI.FormatAsLink("Phosphorite", "PHOSPHORITE") + " as fertilization." });
            }
            public class RIMEDMALLOW
            {
                public static LocString NAME = "Rimed Mallow";
                public static LocString DESC = string.Concat(new string[] { "An evergreen plant well adapt to thrive in very cold environments. Produces a fluffy " + UI.FormatAsLink("Rimed Cotton Boll", "RimedCotton") + "." });
                public static LocString DOMESTICATED_DESC = string.Concat(new string[] { "/n/n In domesticated environment this crop requires the use of pure water " + UI.FormatAsLink("Ice", "ICE") + " as fertilization." });
            }
        }
    }
}
