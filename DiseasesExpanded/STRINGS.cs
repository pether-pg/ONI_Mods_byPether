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
                public static LocString LEGEND_HOVERTEXT = (LocString)"Hungerms present\n";
            }
            public class ALIENGERMS
            {
                public static LocString NAME = (LocString) UI.FormatAsLink("Alien Goo", nameof(ALIENGERMS));
                public static LocString LEGEND_HOVERTEXT = (LocString)"Alien Goo present\n";
            }
            public class MUTATINGGERMS
            {
                public static LocString NAME = (LocString) UI.FormatAsLink("Unstable Virus", nameof(MUTATINGGERMS));
                public static LocString LEGEND_HOVERTEXT = (LocString)"Unstable Virus present\n";
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
                public static LocString NAME = (LocString) UI.FormatAsLink("Frost Pox", nameof(FROSTSICKNESS));
                public static LocString DESCRIPTIVE_SYMPTOMS = (LocString)"Pretty, but pretty cold frost patterns on the skin.";
                public static LocString DESCRIPTION = (LocString)"Little shards of frost cover duplicant's skin in a pretty patterns, reducing their resistance for temperature and diseases.";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Area causes Frost Pox.";
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

            public class SPINDLYCURSE
            {
                public static LocString NAME = (LocString) UI.FormatAsLink("Sleeping Curse", nameof(SPINDLYCURSE));
                public static LocString DESCRIPTIVE_SYMPTOMS = (LocString)"Periodically causes duplicant to fall asleep in random places.";
                public static LocString DESCRIPTION = (LocString)"The tale says one duplicant slept 100 cycles after hurting herself on the Spindle, but it is possible she just faked it not to clean the toilets...";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Area affected by Sleeping Curse.";
                public static LocString EXPOSURE_INFO = (LocString)"Got hurt by the Spindle of a Grubfruit.\n\nAs a result, this duplicant will fall asleep in random places.\nImprove Farming skill or wear protective equipment to prevent future infections.\nUse Espresso Machine to prevent Narcoleptic Naps.\n";
            }

            public class ALIENSYMBIOT
            {
                public static LocString NAME = (LocString) UI.FormatAsLink("Alien Symbiot", nameof(ALIENSYMBIOT));
                public static LocString DESCRIPTIVE_SYMPTOMS = (LocString)"Enhances and stresses the duplicant.";
                public static LocString DESCRIPTION = (LocString)"Alien microbes increase duplicant's skills. However, this feels unnatural and makes infected duplicant stressed out.";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Area affected by Alien Goo.";
                public static LocString EXPOSURE_INFO = (LocString)"Got exposed to Alien Lifeform";
            }

            public class MUTATINGDISEASE
            {
                public static LocString NAME = (LocString) UI.FormatAsLink("Mutating Disease", nameof(MUTATINGDISEASE));
                public static LocString DESCRIPTIVE_SYMPTOMS = (LocString)"May affect duplicants in many different ways";
                public static LocString DESCRIPTION = (LocString)"Virus causing the disease is very unstable and often mutates. Results may be different each time.";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Area affected by Mutating Disease.";
                public static LocString EXPOSURE_INFO = (LocString)"Got infected by Mutating Virus.";
                public static LocString POPFXTEXT = (LocString)"Painful cough!";
            }

            public class TEMPORALDISPLACEMENT
            {
                public static LocString NAME = (LocString) UI.FormatAsLink("Temporal Displacement", nameof(TEMPORALDISPLACEMENT));
                public static LocString DESCRIPTIVE_SYMPTOMS = (LocString)"Sometimes displace duplicant to another location";
                public static LocString DESCRIPTION = (LocString)"Sometimes displace duplicant to another location";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Area causes Temporal Displacement.";
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
                public static LocString NAME = (LocString)"Frost Pox Recovery";
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

            public class SPINDLYRECOVERY
            {
                public static LocString NAME = (LocString)"Sleeping Curse Recovery";
                public static LocString DESC = (LocString)"This duplicant just recovered from Sleeping Curse.";
            }

            public class ALIENRECOVERY
            {
                public static LocString NAME = (LocString)"Alien Symbiot Recovery";
                public static LocString DESC = (LocString)"Despite Alien Symbiot's influence recently waned, the horrifying menace of it still lives on... ";
            }

            public class MUTATEDSYMPTHOMS
            {
                public static LocString NAME = (LocString)"Mutating Disease Sympthoms";
                public static LocString DESC = (LocString)"This duplicant is suffering from various sympthoms of mutating disease.";
            }

            public class MUTATEDRECOVERY
            {
                public static LocString NAME = (LocString)"Mutating Disease Recovery";
                public static LocString DESC = (LocString)"This duplicant just recovered from Mutating Disease.";
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

            public class VACCINE
            {
                public static LocString NAME = (LocString)"{0} Vaccine";
                public static LocString DESC = (LocString)"Grants immunity to {0} disease.";
            }

            public class SUPERSERUM
            {
                public static LocString NAME = (LocString)"Super Serum";
                public static LocString DESC = (LocString)"Enhances duplicant's attributes.";
            }

            public class MUTATINGANTIVIRAL
            {
                public static LocString NAME = (LocString)"Unstable Antiviral";
                public static LocString DESC = (LocString)"Cures from, and grants immunity to Mutating Disease for a short time.";
            }
        }

        public class GERMFLASK
        {
            public static LocString NAME = (LocString)"{0} Flask";
            public static LocString DESC = (LocString)"Contains gathered {0} germs.";
            public static LocString DESC_NOGERM = (LocString)"Contains gathered {0}.";
        }

        public class MEDICALRESEARCH
        {
            public static LocString NAME = (LocString)"Medical Research";
            public static LocString DESC = (LocString)(UI.FormatAsLink("Medical Research", nameof(RESEARCH)) + " is required to unlock medical technologies.\nIt can be conducted by studying germ samples at an " + UI.FormatAsLink("Apothecary", "APOTHECARY") + " or obtained by doctors while tending their patients.");
            public static LocString RECIPEDESC = (LocString)"Unlocks medical technologies.";
        }

        public class CONTROLLEDMUTATION
        {
            public class ATTACK
            {
                public static LocString NAME = (LocString)"Controlled Severity Mutation";
                public static LocString DESC = (LocString)"Conducted in laboratory conditions, forces Unstable Virus to mutate back it's severity genoms to a weaker form.";
                public static LocString RECIPEDESC = (LocString)"Weakens Unstable Virus severity";
            }
            public class ENVIRONMENTAL
            {
                public static LocString NAME = (LocString)"Controlled Environmental Mutation";
                public static LocString DESC = (LocString)"Conducted in laboratory conditions, forces Unstable Virus to mutate back it's environmental genoms to a weaker form.";
                public static LocString RECIPEDESC = (LocString)"Weakens Unstable Virus ability to live in various environments";
            }
            public class RESILIANCE
            {
                public static LocString NAME = (LocString)"Controlled Resiliance Mutation";
                public static LocString DESC = (LocString)"Conducted in laboratory conditions, forces Unstable Virus to mutate back it's resiliance genoms to a weaker form.";
                public static LocString RECIPEDESC = (LocString)"Weakens Unstable Virus resiliance";
            }

        }

        public class BUILDINGS
        {
            public class GERMCATCHER
            {
                public static LocString NAME = (LocString)"Germcatcher";
                public static LocString DESC = (LocString)"Gathers ambient germs and stores them in the flask for future research at an Apothecary and vaccine production.";
                public static LocString EFFECCT = (LocString)"Germcatchers collect germ samples for medical study and vaccine production.";
            }

            public class VACCINEAPOTHECARY
            {
                public static LocString NAME = (LocString)"Vaccine Apothecary";
                public static LocString DESC = (LocString)"Vaccine Apothecary uses radiation to weaken germs stored in a flasks to produce life-saving vaccines. \n\nIt must be operated by highly-trained medical personel and it is suggested to use radiation protection for the time of vaccine preparation.";
                public static LocString EFFECCT = (LocString)"Vaccines produced here can grant your duplicants immunity for various diseases.";
            }
        }

        public class STATUSITEMS
        {
            public class GATHERING
            {
                public static LocString NAME = (LocString)"Gathering";
                public static LocString TOOLTIP = (LocString)"This building is currently gathering ambient germs.";
                public static LocString PROGRESS = (LocString)" {GERMS} - {PROGRESS}%";
            }
        }

        public class TRAITS
        {
            public class INSECTALLERGIES
            {
                public static LocString NAME = (LocString)"Insect Allergies";
                public static LocString DESC = (LocString)"Insects bites will cause additional damage to this Duplicant.";
                public static LocString SHORT_DESC = (LocString)"Allergic reaction to insect bites";
                public static LocString SHORT_DESC_TOOLTIP = (LocString)"Insect Allergies make small bites more dangerous.";
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
