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
        public class DiseaseSettings
        {
            public bool IncludeDisease;
            public float SeverityScale;
            public Color32 GermColor;

            public DiseaseSettings(bool include, float scale, Color32 color)
            {
                IncludeDisease = include;
                SeverityScale = scale;
                GermColor = color;
            }
        }

        public class VirusSettings
        {
            public bool IncludeDisease;
            public float SeverityScale;
            public int FinalMutationCycleEstimation;
            public int MinimalMutationInterval;
            public float MutationFocusEqualizer;
            public bool ClearVirusMutationsOnLoad;
            public SortedDictionary<float, Color32> MutationVirusStageColors;

            public VirusSettings(bool include, float scale, int finalCycle, int minimalInterval, float equalizer, bool clearOnLoad)
            {
                IncludeDisease = include;
                SeverityScale = scale;
                FinalMutationCycleEstimation = finalCycle;
                MinimalMutationInterval = minimalInterval;
                MutationFocusEqualizer = equalizer;
                ClearVirusMutationsOnLoad = clearOnLoad;
                MutationVirusStageColors = new SortedDictionary<float, Color32>(){
                    { 0.00f, ColorPalette.PaleGreen },
                    { 0.20f, ColorPalette.FreshGreen },
                    { 0.40f, ColorPalette.BrightYellow },
                    { 0.60f, ColorPalette.BloodyRed },
                    { 1.00f, ColorPalette.ReddyPurple }
                };
            }
        }

        public class RandomEventsSettings
        {
            public bool EnableTwitchEvents = true;
            public bool ShowDetailedEventNames = false;
            public float RelativeEventsWeight = 1;
        }

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
                    SettingsBackup.Instance.StoreBackup(JsonSerializer<Settings>.GetDefaultName());
                }
                return _instance;
            }
            set { _instance = value; }
        }

        public bool RebalanceForDiseasesRestored = false;
        public bool AutoDetectRelatedMods = true;
        public bool EnableMedicalResearchPoints = true;

        public DiseaseSettings AlienGoo = new DiseaseSettings(true, 1.0f, ColorPalette.NavyBlue);
        public DiseaseSettings BogInsects = new DiseaseSettings(true, 1.0f, ColorPalette.BogViolet);
        public DiseaseSettings FrostPox = new DiseaseSettings(true, 1.0f, ColorPalette.IcyBlue);
        public DiseaseSettings MooFlu = new DiseaseSettings(true, 1.0f, ColorPalette.GassyOrange);
        public DiseaseSettings HungerGerms = new DiseaseSettings(true, 1.0f, ColorPalette.HungryBrown);
        public DiseaseSettings SleepingCurse = new DiseaseSettings(true, 1.0f, ColorPalette.PaleGreen);
        public DiseaseSettings MedicalNanobots = new DiseaseSettings(true, 1.0f, ColorPalette.NanobotGray);
        public VirusSettings MutatingVirus = new VirusSettings(true, 1.0f, 1000, 5, 0.5f, false);
        public RandomEventsSettings RandomEvents = new RandomEventsSettings();
    }
}
