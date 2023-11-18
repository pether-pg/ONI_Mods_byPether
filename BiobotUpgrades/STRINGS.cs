using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiobotUpgrades
{
    public class STRINGS
    {
        public class REFUEL_MODULE
        {

            public class INACTIVE
            {
                public static LocString NAME = "Zombie Recharge inactive";
                public static LocString TOOLTIP = "Ambient atmosphere lacks Zombie Spores for recharge";
            }

            public class RECHARGING
            {
                public static LocString NAME = "Zombie Recharge active";
                public static LocString TOOLTIP = "Biobot collects ambient Zombie Spores to recharge it's Biofuel Battery";
            }

            public class EFFECT
            {
                public static LocString NAME = "Collected Zombie Spores";
                public static LocString DESC = "Energy created with collected Zombie Spores";
            }
        }
    }
}
