using STRINGS;

namespace DiseasesExpanded
{
    public class STRINGS
    {
        public class GERMS
        {
            public class BOGINSECTS
            {
                public static LocString NAME = (LocString) UI.FormatAsLink("Bog Bugs", nameof(BOGINSECTS));
                public static LocString LEGEND_HOVERTEXT = (LocString)"Bog Bugs present.\n";
            }
            public class FROSTHARDS
            {
                public static LocString NAME = (LocString) UI.FormatAsLink("Frost Shards", nameof(FROSTHARDS));
                public static LocString LEGEND_HOVERTEXT = (LocString)"Frost Shards present.\n";
            }
            public class GASSYGERMS
            {
                public static LocString NAME = (LocString) UI.FormatAsLink("Gassy Germs", nameof(GASSYGERMS));
                public static LocString LEGEND_HOVERTEXT = (LocString)"GassyGerms present\n";
            }
            public class HUNGERGERMS
            {
                public static LocString NAME = (LocString) UI.FormatAsLink("Hungerms", nameof(HUNGERGERMS));
                public static LocString LEGEND_HOVERTEXT = (LocString)"Gassy Germs present\n";
            }
        }

        public class DISEASES
        {
            public class BOGSICKNESS
            {
                public static LocString NAME = (LocString) UI.FormatAsLink("Bog Bugs Infestation", nameof(BOGSICKNESS));
                public static LocString DESCRIPTIVE_SYMPTOMS = (LocString)"Annoying bites of small Bog Bugs";
                public static LocString POPFXTEXT = (LocString)"Bog Bug Bite!";
                public static LocString DESCRIPTION = (LocString)"Bog Bugs are lured by Bog Bucket sweet scent, but they can also feed on duplicants...";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Area can result in Bog Bugs bites";
            }

            public class FROSTSICKNESS
            {
                public static LocString NAME = (LocString) UI.FormatAsLink("Frost-Covered Skin", nameof(FROSTSICKNESS));
                public static LocString DESCRIPTIVE_SYMPTOMS = (LocString)"Pretty, but pretty cold frost patterns on the skin.";
                public static LocString DESCRIPTION = (LocString)"Little shards of frost cover duplicant's skin in a pretty patterns, reducing their resistance for temperature and diseases.";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Area causes Frost-Covered Skin.";
            }

            public class GASSICKNESS
            {
                public static LocString NAME = (LocString) UI.FormatAsLink("Moomoza", nameof(GASSICKNESS));
                public static LocString DESCRIPTIVE_SYMPTOMS = (LocString)"Makes duplicant fart like a mad Moo.";
                public static LocString DESCRIPTION = (LocString)"Whatever it is in the Gas Grass, it makes everything around fart...";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Area causes Moomoza.";
            }

            public class HUNGERSICKNESS
            {
                public static LocString NAME = (LocString) UI.FormatAsLink("Everlasting Hunger", nameof(HUNGERSICKNESS));
                public static LocString DESCRIPTIVE_SYMPTOMS = (LocString)"Makes duplicant hungry all the time.";
                public static LocString DESCRIPTION = (LocString)"The hunger of the Experiment 52B seem to be infectious...";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Area causes Everlasting Hunger.";
            }
        }

        public class EFFECTS
        {
            public class BOGRECOVERY
            {
                public static LocString NAME = (LocString)"Bog Bugs Infestation Recovery";
                public static LocString DESC = (LocString)"This duplicant just recovered from Bog Bugs Infestation.";
            }

            public class FROSTRECOVERY
            {
                public static LocString NAME = (LocString)"Frost-Covered Skin Recovery";
                public static LocString DESC = (LocString)"This duplicant just recovered from Frost-Covered Skin.";
            }

            public class GASRECOVERY
            {
                public static LocString NAME = (LocString)"Moomoza Recovery";
                public static LocString DESC = (LocString)"This duplicant just recovered from Moomoza.";
            }

            public class HUNGERRECOVERY
            {
                public static LocString NAME = (LocString)"Everlasting Hunger Recovery";
                public static LocString DESC = (LocString)"This duplicant just recovered from Everlasting Hunger.";
            }
        }

        public class CURES
        {
            public class GASCURE
            {
                public static LocString NAME = (LocString)"Sprouting Tablet";
                public static LocString DESC = (LocString)"If a berry sprouts in a duplicant's belly, their farts will smell like flowers";
            }

            public class MUDMASK
            {
                public static LocString NAME = (LocString)"Muddy Salve";
                public static LocString DESC = (LocString)"When covered in mud, duplicants are not so tasty for Bog Bugs.";
            }

            public class SAPSHOT
            {
                public static LocString NAME = (LocString)"Sweet Sap Shot";
                public static LocString DESC = (LocString)"Thick and full of shugar, Sweet Sap Shot condenses many calories into one syringe to quickly remove overwhelming hunger.";
            }
            public class ANTIHISTAMINEBOOSTER
            {
                public static LocString NAME = (LocString)"Allergy Immuno Booster";
                public static LocString DESC = (LocString)"Prevents allergic reactions in the future.";
            }
        }

        public class TRAITS
        {
            public class INSECTALLERGIES
            {
                public static LocString NAME = (LocString)"Insect Allergies";
                public static LocString DESC = (LocString)("Insects bites will cause additional damage to this Duplicant.");
                public static LocString SHORT_DESC = (LocString)"Allergic reaction to insect bites";
                public static LocString SHORT_DESC_TOOLTIP = "Insect Allergies make small bites more dangerous.";
            }
        }

        public class TRANSLATION
        {
            public class AUTHOR
            {
                public static LocString NAME = (LocString)"pether.pg";
            }
        }
    }
}
