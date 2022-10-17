using STRINGS;
using Dupes_Aromatics.Plants;

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
                public static LocString SEED_DESC = "The black seed of a " + UI.FormatAsLink("Spinosa", Plant_SpinosaConfig.ID) + ".";
            }

            public class DUSKLAVENDER
            {
                public static LocString SEED_NAME = "Dusk Seed";
                public static LocString SEED_DESC = "The tiny seed of a " + UI.FormatAsLink("Duskbloom Lavender", Plant_DuskLavenderConfig.ID) + ".";
            }

            public class RIMEDMALLOW
            {
                public static LocString SEED_NAME = "Iced Mallow Seed";
                public static LocString SEED_DESC = "The chill seed of a " + UI.FormatAsLink("Iced Mallow", Plant_RimedMallowConfig.ID) + ".";
            }
        }

        public class PLANTS
        {
            public class SPINOSA
            {
                public static LocString NAME = "Blooming Spinosa";
                public static LocString DESC = string.Concat(new string[] { "A rather thorny sten plant that blooms with a beautiful " + UI.FormatAsLink("Spinosa Rose", Crop_SpinosaRoseConfig.ID) + "." });
                public static LocString DOMESTICATED_DESC = DESC + "/n/n In domesticated environment this crop requires copious amounts of" + UI.FormatAsLink("Water", "WATER") + ", and" + UI.FormatAsLink("Dirt", "DIRT") + "as fertilizer. Also requires direct exposure to sunlight.";
            }

            public class SUPERSPINOSA
            {
                public static LocString NAME = "Fruiting Spinosa";
                public static LocString DESC = string.Concat(new string[] { "A rather thorny sten plant that produces an edible " + UI.FormatAsLink("Spinosa Hips", Crop_SpinosaHipsConfig.ID) + "." });
                public static LocString DOMESTICATED_DESC = string.Concat(new string[] { "/n/n This domesticated plant requires copious amounts of" + UI.FormatAsLink("Water", "WATER") + ", and" + UI.FormatAsLink("Dirt", "DIRT") + "as fertilizer. Also requires direct exposure to sunlight." });
            }

            public class DUSKLAVENDER
            {
                public static LocString NAME = "Duskbloom Lavender";
                public static LocString DESC = string.Concat(new string[] { "A shrub-like plant blooms with a beautiful " + UI.FormatAsLink("Duskbloom", Crop_DuskbloomConfig.ID) + "." });
                public static LocString DOMESTICATED_DESC = string.Concat(new string[] { "/n/n In domesticated environment this crop requires the use of " + UI.FormatAsLink("Phosphorite", "PHOSPHORITE") + " as fertilization." });
            }

            public class SUPERDUSKLAVENDER
            {
                public static LocString NAME = "Duskberry Lavender";
                public static LocString DESC = string.Concat(new string[] { "A shrub-like plant sprouts with an edible " + UI.FormatAsLink("Duskberry", Crop_DuskberryConfig.ID) + "." });
                public static LocString DOMESTICATED_DESC = string.Concat(new string[] { "/n/n In domesticated environment this crop requires the use of " + UI.FormatAsLink("Phosphorite", "PHOSPHORITE") + " as fertilization." });
            }
            public class RIMEDMALLOW
            {
                public static LocString NAME = "Rimed Mallow";
                public static LocString DESC = string.Concat(new string[] { "An evergreen plant well adapt to thrive in very cold environments. Produces a fluffy " + UI.FormatAsLink("Rimed Cotton Boll", Crop_CottonBollConfig.ID) + "." });
                public static LocString DOMESTICATED_DESC = string.Concat(new string[] { "/n/n In domesticated environment this crop requires the use of pure water " + UI.FormatAsLink("Ice", "ICE") + " as fertilization." });
            }
        }

        public class CROPS
        {
            public class SPINOSAROSE
            {
                public static LocString NAME = UI.FormatAsLink("Spinosa Rose", Crop_SpinosaRoseConfig.ID.ToUpper());
                public static LocString DESC = ("The beautiful blossom of a " + UI.FormatAsLink("Spinosa", Plant_SpinosaConfig.ID) + ". It has a simultaneously sweet and spicy smell, giving ode to meadows, honey, and fruit notes.");
            }

            public class SPINOSAHIPS
            {
                public static LocString NAME = UI.FormatAsLink("Spinosa Hips", Crop_SpinosaHipsConfig.ID.ToUpper());
                public static LocString DESC = ("A small, berry-sized, reddish fruiting body of a " + UI.FormatAsLink("Fruiting Spinosa", Plant_SuperSpinosaConfig.ID) + ". These have a floral, slightly sweet flavor with a touch of tartness, rich in Vitamin C.");
            }

            public class DUSKBLOOM
            {
                public static LocString NAME = UI.FormatAsLink("Duskbloom", Crop_DuskbloomConfig.ID.ToUpper());
                public static LocString DESC = ("The gentle blossom of a " + UI.FormatAsLink("Duskbloom Lavender", Plant_DuskLavenderConfig.ID) + ". It has a delicate, sweet smell that is floral, herbal, and evergreen woodsy at the same time.");
            }

            public class DUSKBERRY
            {
                public static LocString NAME = UI.FormatAsLink("Duskberry", Crop_DuskberryConfig.ID.ToUpper());
                public static LocString DESC = ("A small, soft, jelly filled fruiting body of a " + UI.FormatAsLink("Duskberry Lavender", Plant_SuperDuskLavenderConfig.ID) + ". They taste delightful and have a slightly sweet tart flavor that is mixed with a little bit of acidic.");
            }

            public class COTTONBOLL
            {
                public static LocString NAME = UI.FormatAsLink("Rimed Cotton Boll", Crop_CottonBollConfig.ID.ToUpper());
                public static LocString DESC = $"A soft, fluffy staple fiber that grows in a boll encased in ice crystals.  The fiber of the boll is almost pure cellulose, but also contains high concentration of aromatic oils that gives it a smooth and pleasing scent.";
            }
        }

        public class TRANSLATION
        {
            public class AUTHOR
            {
                public static LocString NAME = "Ronivan + pether.pg";
            }
        }
    }
}
