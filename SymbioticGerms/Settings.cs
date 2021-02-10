using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymbioticGerms
{
    class Settings
    {
        private static Settings _instance = null;

        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Settings();
                return _instance;
            }
        }

        public float MaxSlicksterBonus = 0.2f;
        public float MaxMealLiceBonus = 0.2f;
        public float MaxDuskCupBonus = 2f;
    }
}
