using System;
using Newtonsoft.Json;
using PeterHan.PLib;
using PeterHan.PLib.Options;

namespace FragrantFlowers
{
    [Serializable]
    [RestartRequired]
    [ConfigFileAttribute("FragrantFlowers.Settings.json", true)]
    public class Settings
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

        public static void PLib_Initalize()
        {
            _instance = POptions.ReadSettings<Settings>();
        }

        [JsonProperty]
        [Option("Duskbloom Lavender", category: "New Flower - Duskbloom Lavender")]
        public LavenderSettings Lavender { get; set; }

        [JsonProperty]
        [Option("Rimed Mallow", category: "New Flower - Rimed Mallow")]
        public BasicFlowerSettings Mallow { get; set; }

        [JsonProperty]
        [Option("Spinosa Rose", category: "New Flower - Spinosa Rose")]
        public BasicFlowerSettings Rose { get; set; }

        [JsonProperty]
        [Option("Seeds spawn", "Do you want to spawn buries seeds?", category: "Additional Settings")]
        public bool SeedsSpawn { get; set; }

        public Settings()
        {
            Lavender = new LavenderSettings();
            Mallow = new BasicFlowerSettings();
            Rose = new BasicFlowerSettings();
            SeedsSpawn = false;
        }

        [Serializable]
        public class BasicFlowerSettings
        {
            [JsonProperty]
            [Limit(1, 10)]
            [Option("Attribute Bonus", "How much will it affect attributes?")]
            public int AttributeBonus { get; set; }

            [JsonProperty]
            [Limit(60, 600)]
            [Option("Effect Duration", "How many seconds will the effect last?")]
            public int EffectDuration { get; set; }

            [JsonProperty]
            [Limit(0.1f, 0.75f)]
            [Option("Average Density", "How densly will the flowers spawn?")]
            public float AverageDensity { get; set; }

            [JsonProperty]
            [Limit(1f, 10f)]
            [Option("Seeds In Care Package", "How many seeds will spawn in each care package?")]
            public int SeedsInCarePackage { get; set; }

            public BasicFlowerSettings()
            {
                AttributeBonus = 3;
                EffectDuration = 300;
                AverageDensity = 0.3f;
                SeedsInCarePackage = 3;
            }
        }

        [Serializable]
        public class LavenderSettings : BasicFlowerSettings
        {
            [JsonProperty]
            [Limit(0, 10)]
            [Option("Critter Happiness Bonus", "How happy will the critters be after smelling the flowers?")]
            public int CritterHappinessBonus { get; set; }

            [JsonProperty]
            [Limit(0.0f, 10.0f)]
            [Option("Critter Fertility Bonus", "Fertility per cycle after smelling the flowers.")]
            public float CritterFertilityBonus { get; set; }

            [JsonProperty]
            [Limit(0, 100)]
            [Option("Critter Wilderness Bonus", "Wilderness drop per cycle after smelling the flowers.")]
            public int CritterWildernessBonus { get; set; }

            public LavenderSettings()
            {
                AttributeBonus = 3;
                EffectDuration = 300;
                AverageDensity = 0.3f;
                SeedsInCarePackage = 3;
                CritterHappinessBonus = 1;
                CritterFertilityBonus = 1.0f;
                CritterWildernessBonus = 5;
            }
        }
    }
}
