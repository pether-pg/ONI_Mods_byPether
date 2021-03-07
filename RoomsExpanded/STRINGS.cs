using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomsExpanded
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

        public class ROOMS
        {
            public class TYPES
            {
                public class LABORATORY
                {
                    public static LocString NAME = (LocString)"Laboratory";
                    public static LocString EFFECT = (LocString)" - Faster research";
                    public static LocString TOOLTIP = (LocString)"Conducting research in a designated room proves to be more efficient.";
                }

                public class AGRICULTURAL
                {
                    public static LocString NAME = (LocString)"Agricultural Room";
                    public static LocString EFFECT = (LocString)" - Enables Farm and Grooming Stations";
                    public static LocString TOOLTIP = (LocString)"Area, where all critters and plants can happily live together in harmony.";
                }

                public class KITCHEN
                {
                    public static LocString NAME = (LocString)"Kitchen";
                    public static LocString EFFECT = (LocString)" - More food produced";
                    public static LocString TOOLTIP = (LocString)"It appears that nobody dares to steal food from chef's kitchen.";
                }

                public class SHOWERROOM
                {
                    public static LocString NAME = (LocString)"Shower Room";
                    public static LocString EFFECT = (LocString)" - Shorter shower time";
                    public static LocString TOOLTIP = (LocString)"Just entering such a nice bathroom make duplicants feel cleaner.";
                }

                public class GRAVEYARD
                {
                    public static LocString NAME = (LocString)"Graveyard";
                    public static LocString EFFECT = (LocString)" - (no additional effect)";
                    public static LocString TOOLTIP = (LocString)"It makes duplicants happy to think that they are still alive.";
                }

                public class GYMROOM
                {
                    public static LocString NAME = (LocString)"Gym Room";
                    public static LocString EFFECT = (LocString)" - Faster athletics increase";
                    public static LocString TOOLTIP = (LocString)"Professional Gym Room allows duplicants to exercise more efficiently.";
                }

                public class INDUSTRIAL
                {
                    public static LocString NAME = (LocString)"Industrial Room";
                    public static LocString EFFECT = (LocString)" - (no additional effect)";
                    public static LocString TOOLTIP = (LocString)"This room contains some noisy, greasy machinery.";
                }

                public class NURSERY
                {
                    public static LocString NAME = (LocString)"Plant Nursery";
                    public static LocString EFFECT = (LocString)" - Chance for additional seeds";
                    public static LocString TOOLTIP = (LocString)"Enriched environment improves plant development.";
                }

                public class AQUARIUM
                {
                    public static LocString NAME = (LocString)"Aquarium";
                    public static LocString EFFECT = (LocString)" - Bonus decor for inhabitating fish";
                    public static LocString TOOLTIP = (LocString)"Properly exposed critters seem to be much prettier.";
                }

                public class BOTANICAL
                {
                    public static LocString NAME = (LocString)"Botanical Garden";
                    public static LocString EFFECT = (LocString)" - Morale bonus";
                    public static LocString TOOLTIP = (LocString)"Serene harmony of exotic plants amazes everyone who enters Botanical Garden.";
                }

                public class MUSEUM
                {
                    public static LocString NAME = (LocString)"Museum";
                    public static LocString EFFECT = (LocString)" - Morale bonus";
                    public static LocString TOOLTIP = (LocString)"It used to be a storehouse, before Meep confused Pedestal content with art.";
                }
            }

            public class CRITERIA
            {
                public class AGRICULTURAL
                {
                    public static LocString NAME = (LocString)"Grooming or Farm Station";
                    public static LocString DESCRIPTION = (LocString)"Requires a single Grooming Station or Farm Station";
                }

                public class COOKING
                {
                    public static LocString NAME = (LocString)"Cooking Station";
                    public static LocString DESCRIPTION = (LocString)"At least one Microbe Musher, Electric Grill or Gas Range.";
                }

                public class FRIDGE
                {
                    public static LocString NAME = (LocString)"Refrigerator";
                    public static LocString DESCRIPTION = (LocString)"At least one Refrigerator.";
                }

                public class SHOWER
                {
                    public static LocString NAME = (LocString)"Shower";
                    public static LocString DESCRIPTION = (LocString)"At least one Shower";
                }

                public class GRAVE
                {
                    public static LocString NAME = (LocString)"Tasteful Memorial";
                    public static LocString DESCRIPTION = (LocString)"At least one Tasteful Memorial";
                }

                public class MANUALGENERATOR
                {
                    public static LocString NAME = (LocString)"Manual Generator";
                    public static LocString DESCRIPTION = (LocString)"At least one Manual Generator";
                }

                public class WATERCOOLER
                {
                    public static LocString NAME = (LocString)"Water Cooler";
                    public static LocString DESCRIPTION = (LocString)"At least one Water Cooler";
                }

                public class INDUSTRIAL
                {
                    public static LocString NAME = (LocString)"Industrial Machinery";
                    public static LocString DESCRIPTION = (LocString)"At least one Industrial Machinery building";
                }

                public class PLANTERBOX
                {
                    public static LocString NAME = (LocString)"Planter Box";
                    public static LocString DESCRIPTION = (LocString)"At least one Planter Box";
                }

                public class SEEDPLANTS
                {
                    public static LocString NAME = (LocString)"{0} Different Seed Plants";
                    public static LocString DESCRIPTION = (LocString)"At least {0} Plants growing seeds of different types.";
                }

                public class FISHFEEDER
                {
                    public static LocString NAME = (LocString)"Fish Feeder";
                    public static LocString DESCRIPTION = (LocString)"At least one Fish Feeder";
                }

                public class FISHRELEASE
                {
                    public static LocString NAME = (LocString)"Fish Release";
                    public static LocString DESCRIPTION = (LocString)"At least one Fish Release";
                }

                public class SWIMMINGCREATURES
                {
                    public static LocString NAME = (LocString)"{0} Swimming Critters";
                    public static LocString DESCRIPTION = (LocString)"At least {0} swimming critters";
                }

                public class DIFFERENTFISH
                {
                    public static LocString NAME = (LocString)"{0} Different Fish";
                    public static LocString DESCRIPTION = (LocString)"At least {0} Fish critters of different type.";
                }

                public class FLOWERPOT
                {
                    public static LocString NAME = (LocString)"Flower Pot of any kind";
                    public static LocString DESCRIPTION = (LocString)"At least one Flower Pot, Wall Pot, Hanging Pot or Aero Pot.";
                }

                public class NOWILTING
                {
                    public static LocString NAME = (LocString)"No Wilting Plants";
                    public static LocString DESCRIPTION = (LocString)"Room conditions must allow all plants to thrive.";
                }

                public class PLANTCOUNT
                {
                    public static LocString NAME = (LocString)"{0} Plants";
                    public static LocString DESCRIPTION = (LocString)"At least {0} plants of any kind.";
                }

                public class NOWILDPLANTS
                {
                    public static LocString NAME = (LocString)"No wild plants";
                    public static LocString DESCRIPTION = (LocString)"All plants must be domesticated.";
                }

                public class DECORPLANTS
                {
                    public static LocString NAME = (LocString)"{0} different Decorative Plants";
                    public static LocString DESCRIPTION = (LocString)"At least {0} decorative plants of different types.";
                }

                public class PEDESTAL
                {
                    public static LocString NAME = (LocString)"Pedestal";
                    public static LocString DESCRIPTION = (LocString)"At least one Pedestal";
                }

                public class MASTERPIECES
                {
                    public static LocString NAME = (LocString)"{0} Masterpieces";
                    public static LocString DESCRIPTION = (LocString)"At least {0} Masterpieces.";
                }

                public class PAINTINGS
                {
                    public static LocString NAME = (LocString)"{0} Different Paintings";
                    public static LocString DESCRIPTION = (LocString)"At least {0} Paintings of different type.";
                }

                public class SCULPTURES
                {
                    public static LocString NAME = (LocString)"{0} Different Sculptures";
                    public static LocString DESCRIPTION = (LocString)"At least {0} Sculptures of different type.";
                }
            }

            public class EFFECTS
            {
                public class MUSEUM
                {
                    public static LocString NAME = (LocString)"Museum";
                    public static LocString DESCRIPTION = (LocString)"Visited Museum";
                }

                public class GRAVE_GOOD
                {
                    public static LocString NAME = (LocString)"Graveyard Serenity";
                    public static LocString DESCRIPTION = (LocString)"This Duplicant has accepted their inevitable fate and is ready to embrace it.";
                }

                public class GRAVE_BAD
                {
                    public static LocString NAME = (LocString)"Graveyard Dread";
                    public static LocString DESCRIPTION = (LocString)"This Duplicant feels cold claws of Death crawling up their spine.";
                }
            }
        }
    }
}
