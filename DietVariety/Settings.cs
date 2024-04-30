using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PeterHan.PLib;
using PeterHan.PLib.Options;

namespace DietVariety
{
    [Serializable]
    [RestartRequired]
    [ConfigFileAttribute("DietVariety.Settings.json", true)]
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

        [JsonProperty]
        [Limit(0, 15)]
        [Option("Min Food Types Required", "Duplicants need that many different meals to satisfy their requirements and reach 0 morale bonus.")]
        public int MinFoodTypesRequired { get; set; }

        [JsonProperty]
        [Limit(1, 25)]
        [Option("Max Meals Counted", "This many meals will be counted. Older meals will not be tracked.")]
        public int MaxMealsCounted { get; set; }

        [JsonProperty]
        [Limit(0.0, 5.0)]
        [Option("Morale PerFood Type", "Each unique meal will grant that much morale bonus.")]
        public float MoralePerFoodType { get; set; }

        public ushort PreferencePenaltyForEatenTypes { get; set; }

        public static void PLib_Initalize()
        {
            _instance = POptions.ReadSettings<Settings>();
        }

        public Settings()
        {
            MinFoodTypesRequired = 5;
            MoralePerFoodType = 1.0f;
            MaxMealsCounted = 15;
            PreferencePenaltyForEatenTypes = 100;
        }
    }
}
