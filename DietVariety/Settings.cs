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
                    _instance = JsonSerializer<Settings>.Deserialize();
                if (_instance == null)
                {
                    _instance = new Settings();
                    JsonSerializer<Settings>.Serialize(_instance);
                }
                return _instance;
            }
        }

        public int MinFoodTypesRequired;
        public ushort MaxMealsCounted;
        public float MoralePerFoodType;
        public ushort PreferencePenaltyForEatenTypes;

        public Settings()
        {
            MinFoodTypesRequired = 3;
            MoralePerFoodType = 0.5f;
            MaxMealsCounted = 15;
            PreferencePenaltyForEatenTypes = 100;
        }
    }
}
