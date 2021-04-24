using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;

namespace IlluminationSensor
{
    class STRINGS
    {
        public class ILLUMINATIONSENSOR
        {
            public static LocString LOGIC_PORT = (LocString)("Surrounding" + UI.FormatAsLink("Illumination", "Light"));
            public static LocString LOGIC_PORT_ACTIVE = (LocString)("Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if " + UI.FormatAsLink("Illumination", "Light") + " is within the selected range");
            public static LocString LOGIC_PORT_INACTIVE = (LocString)("Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby));

            public static LocString NAME = (LocString)"Illumination Sensor";
            public static LocString DESCRIPTION = (LocString)"Illumination sensors can turn additional light sources on when the surrounding is too dim.";
            public static LocString EFFECT = (LocString)("Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " or a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " when " + UI.FormatAsLink("Illumination", "Light") + " enters the chosen range.");
            public static LocString TOOLTIP_PATTERN_ABOVE = (LocString)("Will send a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the " + UI.PRE_KEYWORD + "Illumination" + UI.PST_KEYWORD + " is above <b>{0} Lux</b>");
            public static LocString TOOLTIP_PATTERN_BELOW = (LocString)("Will send a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the " + UI.PRE_KEYWORD + "Illumination" + UI.PST_KEYWORD + " is below <b>{0} Lux</b>");
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
