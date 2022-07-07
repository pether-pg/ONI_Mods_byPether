using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded
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
                if(_instance == null)
                {
                    _instance = new Settings();
                    JsonSerializer<Settings>.Serialize(_instance);
                }
                return _instance;
            }
            set { _instance = value; }
        }

        public bool RebalanceForDiseasesRestored = false;
        public bool AutoDetectRelatedMods = true;
        public int UnstableVirusFinalMutationCycleEstimation = 1000;
        public int UnstableVirusMinimalMutationInterval = 10;
        public bool ClearVirusMutationsOnLoad = false;
    }
}
