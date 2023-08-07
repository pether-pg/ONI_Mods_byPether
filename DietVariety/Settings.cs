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

        public int MinFoodTypesRequired = 3;
        public float MoralePerFoodType = 0.5f;
        public ushort MaxMealsCounted = 15;
        public ushort PreferencePenaltyForEatenTypes = 100;
    }
}
