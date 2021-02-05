using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RocketPresenceAutomationPort
{
    public class PresencePortStrings
    {
        private static string GreenSignal = (string)STRINGS.UI.FormatAsAutomationState("Green Signal", STRINGS.UI.AutomationState.Active);
        private static string RedSignal = (string)STRINGS.UI.FormatAsAutomationState("Red Signal", STRINGS.UI.AutomationState.Standby);

        public static readonly string Id = "PresencePort";
        public static readonly string Description = "Rocket Present";
        public static readonly string Active = $"Sends a {GreenSignal} if the rocket is present in the dock.";
        public static readonly string Inactive = $"After rocket launches, sends a {RedSignal} until rocket's return.";
    }
}
