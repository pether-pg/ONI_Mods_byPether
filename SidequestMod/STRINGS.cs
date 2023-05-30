using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidequestMod
{
    class STRINGS
    {
        public class EFFECTS
        {
            public class SMALL_HAPPINESS
            {
                public static LocString NAME = "Little Joy";
                public static LocString DESC = "It's the little things that matter.";
            }

            public class MEDIUM_HAPPINESS
            {
                public static LocString NAME = "Smiley Day";
                public static LocString DESC = "Today, this duplicant can't stop smiling.";
            }

            public class BIG_HAPPINESS
            {
                public static LocString NAME = "Overflowing Satisfaction";
                public static LocString DESC = "This duplicant is confident for the future of our colony and is thrilled to help developing it.";
            }

            public class SMALL_SADNESS
            {
                public static LocString NAME = "Small Sadness";
                public static LocString DESC = "Due to recent events, this duplicant is rather sad.";
            }

            public class MEDIUM_SADNESS
            {
                public static LocString NAME = "Gloomy Mood";
                public static LocString DESC = "After recent event, this duplicant is full of sad thoughts.";
            }

            public class BIG_SADNESS
            {
                public static LocString NAME = "Breaking Point";
                public static LocString DESC = "With all plans and dreams crashed, this duplicant has no hope for the future...";
            }
        }

        public class SIDEQUESTS
        {
            public class TUTORIAL_TOILETS
            {
                public static LocString NAME = "First Toilets";
                public static LocString STARTING_TEXT = "We need to quickly setup some toilets!";
                public static LocString FAILING_TEXT = "Oh no...";
                public static LocString PASSING_TEXT = "Yay! With this toilets our lives won't be so messy and soggy!";
            }

            public class INTEREST_PILOTING_SURFACE_BREACH
            {
                public static LocString NAME = "To See The Stars";
                public static LocString STARTING_TEXT = "Are we burried in dust and rocks? I'm sure there must be something beyond! I would love to reach the surface and see what lies beyond...";
                public static LocString FAILING_TEXT = "It seems there is nothing to run from here... We are cofined in here, and this will be our tomb...";
                public static LocString PASSING_TEXT = "Are those... stars...? They are so pretty... And there is so many of them... I could stare at them all the time!";
            }
        }
    }
}
