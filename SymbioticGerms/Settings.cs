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

        public float MaxWheatChance = 0.2f;
        public float MaxBeansChance = 0.2f;
        public float MaxPacuBonus = 0.2f;
        public float MaxDreckoBonus = 0.2f;
        public float MaxPuftBonus = 0.2f;
        public float MaxSlicksterBonus = 0.2f;
        public float MaxMealLiceBonus = 0.2f;
        public float MaxBogBucketBonus = 0.2f;
        public float MaxDuskCupBonus = 2f;
        public float MaxPepperTempScale = 1.1f;
    }
}
