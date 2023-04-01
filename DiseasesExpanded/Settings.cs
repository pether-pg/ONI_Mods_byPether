using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PeterHan.PLib;
using PeterHan.PLib.Options;
using UnityEngine;

namespace DiseasesExpanded
{
    [Serializable]
    [RestartRequired]
    [ConfigFileAttribute("DiseasesExpanded.Settings.json", true)]
    public class Settings : IOptions
    {
        [Serializable]
        public class DiseaseSettings
        {
            [JsonProperty]
            [Option("IncludeDisease", "Do you want to play with this germ?")]
            public bool IncludeDisease { get; set; }

            [JsonProperty]
            [Limit(0.0, 5.0)]
            [Option("SeverityScale", "How severe should the disease be?")]
            public float SeverityScale { get; set; }

            [JsonProperty]
            [Option("GermColor", "Defines disease color in germ overlay?")]
            public Color32 GermColor { get; set; }

            public DiseaseSettings(bool include, float scale, Color32 color)
            {
                IncludeDisease = include;
                SeverityScale = scale;
                GermColor = color;
            }
        }

        [Serializable]
        public class VirusSettings
        {
            [JsonProperty]
            [Option("IncludeDisease", "Do you want to play with this germ?")]
            public bool IncludeDisease { get; set; }

            [JsonProperty]
            [Limit(0.0, 5.0)]
            [Option("SeverityScale", "How severe should the disease be?")]
            public float SeverityScale { get; set; }

            [JsonProperty]
            [Limit(0, 5000)]
            [Option("FinalMutationCycleEstimation", "Around this time the Virus will reach it's full power.")]
            public int FinalMutationCycleEstimation { get; set; }

            [JsonProperty]
            [Limit(0, 100)]
            [Option("MinimalMutationInterval", "The virus must wait this long to mutate again.")]
            public int MinimalMutationInterval { get; set; }

            [JsonProperty]
            [Limit(0.0, 1.0)]
            [Option("MutationFocusEqualizer", "0 = very specific mutations, 1 = fully random")]
            public float MutationFocusEqualizer { get; set; }

            [JsonProperty]
            [Option("ClearVirusMutationsOnLoad", "Panic Button if you are ready to admit defeat and want to save remaining dupes...")]
            public bool ClearVirusMutationsOnLoad { get; set; }

            public SortedDictionary<float, Color32> MutationVirusStageColors { get; set; }

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
                    BackupConfig.Instance.StoreBackup(JsonSerializer<Settings>.GetDefaultName());
                }
                return _instance;
            }
            set { _instance = value; }
        }

        public static void PLib_Initalize()
        {
            _instance = POptions.ReadSettings<Settings>();
        }

        public Settings()
        {
            RebalanceForDiseasesRestored = false;
            AutoDetectRelatedMods = true;
            EnableMedicalResearchPoints = true;

            AlienGoo = new DiseaseSettings(true, 1.0f, ColorPalette.NavyBlue);
            BogInsects = new DiseaseSettings(true, 1.0f, ColorPalette.BogViolet);
            FrostPox = new DiseaseSettings(true, 1.0f, ColorPalette.IcyBlue);
            MooFlu = new DiseaseSettings(true, 1.0f, ColorPalette.GassyOrange);
            HungerGerms = new DiseaseSettings(true, 1.0f, ColorPalette.HungryBrown);
            SleepingCurse = new DiseaseSettings(true, 1.0f, ColorPalette.PaleGreen);
            MedicalNanobots = new DiseaseSettings(true, 1.0f, ColorPalette.NanobotGray);
            MutatingVirus = new VirusSettings(true, 1.0f, 1000, 5, 0.5f, false);

            RandomEvents = new RandomEventsSettings();
        }

        [JsonProperty]
        [Option("Rebalance For Diseases Restored", "Makes things harder and your game - miserable...", category: "DiseasesExpanded: Additional")]
        public bool RebalanceForDiseasesRestored { get; set; }

        [JsonProperty]
        [Option("Auto Detect Related Mods", "Allows to detect other disease-related mods you have active and rebalances Diseases Expanded to align with them.", category: "DiseasesExpanded: Additional")]
        public bool AutoDetectRelatedMods { get; set; }

        [JsonProperty]
        [Option("Enable Medical Research Points", "Makes medical research tree more interactive.", category: "DiseasesExpanded: Additional")]
        public bool EnableMedicalResearchPoints { get; set; }

        [JsonProperty]
        [Option("AlienGoo", category: "New Disease - Alien Goo")]
        public DiseaseSettings AlienGoo { get; set; }

        [JsonProperty]
        [Option("BogInsects", category: "New Disease - Bog Insects")]
        public DiseaseSettings BogInsects { get; set; }

        [JsonProperty]
        [Option("FrostPox", category: "New Disease - Frost Pox")]
        public DiseaseSettings FrostPox { get; set; }

        [JsonProperty]
        [Option("MooFlu", category: "New Disease - Moo Flu")]
        public DiseaseSettings MooFlu { get; set; }

        [JsonProperty]
        [Option("HungerGerms", category: "New Disease - Hunger Germs")]
        public DiseaseSettings HungerGerms { get; set; }

        [JsonProperty]
        [Option("SleepingCurse", category: "New Disease - Sleeping Curse")]
        public DiseaseSettings SleepingCurse { get; set; }

        [JsonProperty]
        [Option("MedicalNanobots", category: "New Feature - Medical Nanobots")]
        public DiseaseSettings MedicalNanobots { get; set; }

        [JsonProperty]
        [Option("MutatingVirus", category: "New Disease - Mutating Virus")]
        public VirusSettings MutatingVirus { get; set; }

        public RandomEventsSettings RandomEvents { get; set; }

        public IEnumerable<IOptionsEntry> CreateOptions()
        {
            return new List<IOptionsEntry>();
        }

        public void OnOptionsChanged()
        {
            BackupConfig.Instance.StoreBackup(JsonSerializer<Settings>.GetDefaultName());
        }
    }
}
