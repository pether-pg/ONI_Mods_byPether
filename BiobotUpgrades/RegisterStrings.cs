using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiobotUpgrades
{
    class RegisterStrings
    {
        public static void MakeStatusItemStrings(string id, string name, string tooltip)
        {
            Strings.Add($"STRINGS.CREATURES.STATUSITEMS.{id.ToUpperInvariant()}.NAME", name);
            Strings.Add($"STRINGS.CREATURES.STATUSITEMS.{id.ToUpperInvariant()}.TOOLTIP", tooltip);
        }
    }
}
