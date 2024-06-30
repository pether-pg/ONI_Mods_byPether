using STRINGS;
using FragrantFlowers;

namespace FragrantFlowers
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

        public class BUILDINGS
        {
            public class VAPORIZER
            {
                public static LocString NAME = "Vaporizer";
                public static LocString DESC = "While working, releases various scents into the air.";
                public static LocString EFFECT = "While working, releases various scents into the air.";
            }
        }

        public class AROMACANS
        {
            public class FLORAL
            {
                public static LocString NAME = "Floral Scent Can";
                public static LocString DESC = "Can full of Floral Scent, ready for Vaporizer.";
            }

            public class ROSE
            {
                public static LocString NAME = "Rose Scent Can";
                public static LocString DESC = "Can full of Rose Scent, ready for Vaporizer.";
            }

            public class LAVENDER
            {
                public static LocString NAME = "Lavender Scent Can";
                public static LocString DESC = "Can full of Lavender Scent, ready for Vaporizer.";
            }

            public class MALLOW
            {
                public static LocString NAME = "Mallow Scent Can";
                public static LocString DESC = "Can full of Rose Scent, ready for Vaporizer.";
            }
        }

        public class FOOD
        {
            public class DUSKJAM
            {
                public static LocString NAME = UI.FormatAsLink("Duskjam", DuskjamConfig.ID);
                public static LocString DESC = "A long lasting " + UI.FormatAsLink(CROPS.DUSKBERRY.NAME, Crop_DuskberryConfig.ID) + " jam preserved in " + UI.FormatAsLink("Sucrose", "SUCROSE") + ".";
            }

            public class SPINOSASYRUP
            {
                public static LocString NAME = UI.FormatAsLink("Spinosa Syrup", SpinosaSyrupConfig.ID);
                public static LocString DESC = "A long lasting " + UI.FormatAsLink(CROPS.SPINOSAHIPS.NAME, Crop_SpinosaHipsConfig.ID) + " syrup preserved in " + UI.FormatAsLink("Sucrose", "SUCROSE") + ".";
            }
            public class DUSKBUN
            {
                public static LocString NAME = UI.FormatAsLink("Duskbun", DuskbunConfig.ID);
                public static LocString DESC = "A bun filled with " + UI.FormatAsLink(FOOD.DUSKJAM.NAME, DuskjamConfig.ID) + ".";
            }

            public class SPINOSACAKE
            {
                public static LocString NAME = UI.FormatAsLink("Spinosa Cake", SpinosaCakeConfig.ID);
                public static LocString DESC = "A delicious cake with " + UI.FormatAsLink(CROPS.SPINOSAHIPS.NAME, Crop_SpinosaHipsConfig.ID) + " on top.";
            }
        }

        public class DESCRIPTORS
        {
            public class SPAWNGERMS
            {
                public static LocString NAME = "{GERMS}";
                public static LocString DESC = "While working, releases {GERMS} into the air.";
            }
        }

        public class EFFECTS
        {
            public class SMELLEDROSE
            {
                public static LocString NAME = (LocString)"Smelled Roses";
                public static LocString DESC = (LocString)"This duplicant just smelled some Roses and is now inspired to create great things.";
            }
            public class SMELLEDLAVENDER
            {
                public static LocString NAME = (LocString)"Smelled Lavender";
                public static LocString DESC = (LocString)"This one just smelled some Lavender, feeling much closer to the nature as a result.";
            }
            public class SMELLEDMALLOW
            {
                public static LocString NAME = (LocString)"Smelled Mallow";
                public static LocString DESC = (LocString)"This duplicant just smelled some Mallow and is invigorted to work faster.";
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
                public static LocString DOMESTICATED_DESC = DESC + "/nIn domesticated environment this crop requires copious amounts of" + UI.FormatAsLink("Water", "WATER") + ", and" + UI.FormatAsLink("Dirt", "DIRT") + "as fertilizer. Also requires direct exposure to sunlight.";
            }

            public class SUPERSPINOSA
            {
                public static LocString NAME = "Fruiting Spinosa";
                public static LocString DESC = string.Concat(new string[] { "A rather thorny sten plant that produces an edible " + UI.FormatAsLink("Spinosa Hips", Crop_SpinosaHipsConfig.ID) + "." });
                public static LocString DOMESTICATED_DESC = string.Concat(new string[] { "/nThis domesticated plant requires copious amounts of" + UI.FormatAsLink("Water", "WATER") + ", and" + UI.FormatAsLink("Dirt", "DIRT") + "as fertilizer. Also requires direct exposure to sunlight." });
            }

            public class DUSKLAVENDER
            {
                public static LocString NAME = "Duskbloom Lavender";
                public static LocString DESC = string.Concat(new string[] { "A shrub-like plant blooms with a beautiful " + UI.FormatAsLink("Duskbloom", Crop_DuskbloomConfig.ID) + "." });
                public static LocString DOMESTICATED_DESC = string.Concat(new string[] { "/nIn domesticated environment this crop requires the use of " + UI.FormatAsLink("Phosphorite", "PHOSPHORITE") + " as fertilization." });
            }

            public class SUPERDUSKLAVENDER
            {
                public static LocString NAME = "Duskberry Lavender";
                public static LocString DESC = string.Concat(new string[] { "A shrub-like plant sprouts with an edible " + UI.FormatAsLink("Duskberry", Crop_DuskberryConfig.ID) + "." });
                public static LocString DOMESTICATED_DESC = string.Concat(new string[] { "/nIn domesticated environment this crop requires the use of " + UI.FormatAsLink("Phosphorite", "PHOSPHORITE") + " as fertilization." });
            }
            public class RIMEDMALLOW
            {
                public static LocString NAME = "Rimed Mallow";
                public static LocString DESC = string.Concat(new string[] { "An evergreen plant well adapt to thrive in very cold environments. Produces a fluffy " + UI.FormatAsLink("Rimed Cotton Boll", Crop_CottonBollConfig.ID) + "." });
                public static LocString DOMESTICATED_DESC = string.Concat(new string[] { "/nIn domesticated environment this crop requires the use of pure water " + UI.FormatAsLink("Ice", "ICE") + " as fertilization." });
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
                public static LocString DESC = $"A soft, fluffy staple fiber that grows in a boll encased in ice crystals. The fiber of the boll is almost pure cellulose, but also contains high concentration of aromatic oils that gives it a smooth and pleasing scent.";
            }
        }

        public class SPICES
        {
            public class ROSE
            {
                public static LocString NAME = "Inspiring Spice";
                public static LocString DESC = "Made with delicate Spinosa petals, this spice inspires duplicants to achieve great things.";
            }

            public class LAVENDER
            {
                public static LocString NAME = "Wild Spice";
                public static LocString DESC = "With faint Duskbloom aroma, this spice develops deep bond with the nature in duplicants.";
            }

            public class MALLOW
            {
                public static LocString NAME = "Rimed Spice";
                public static LocString DESC = "Fresh like an icicle in the morning, this spice invigorates duplicants with icy energy.";
            }
        }

        public class CODEX
        {
            public static LocString AROMATICPLANTSUBTITLE = "Aromatic Plant";
            public static LocString FOODPLANTSUBTITLE = "Food Plant";
            public static LocString PERFUMEINGREDIENTTITLE = "Perfume Ingredient";
            public static LocString CONSUMABLETTITLE = "Consumable";
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
