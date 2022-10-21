using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
        public int UnstableVirusMinimalMutationInterval = 5;
        public float UnstableVirusMutationFocusEqualizer = 0.5f;
        public bool ClearVirusMutationsOnLoad = false;
        //public bool FullyMutateOnLoad = false;
        public bool EnableMedicalResearchPoints = false;

        public SortedDictionary<float, Color32> MutationVirusStageColors = new SortedDictionary<float, Color32>(){
                { 0.00f, ColorPalette.PaleGreen },
                { 0.20f, ColorPalette.FreshGreen },
                { 0.40f, ColorPalette.BrightYellow },
                { 0.60f, ColorPalette.BloodyRed },
                { 1.00f, ColorPalette.ReddyPurple }
            };
    }
}
