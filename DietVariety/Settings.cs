using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietVariety
{
    class Settings
    {
        private static Settings _instance;

        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Settings();
                return _instance;
            }
        }

        public int StartingMorale = -1;
        public float MoralePerFoodType = 0.5f;
        public int MaxMealsCounted = 15;
        public int PreferencePenaltyForEatenTypes = 100;
    }
}
