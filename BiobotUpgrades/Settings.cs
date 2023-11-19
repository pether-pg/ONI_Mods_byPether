using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PeterHan.PLib;
using PeterHan.PLib.Options;
using UnityEngine;

namespace BiobotUpgrades
{
    [Serializable]
    [RestartRequired]
    [ConfigFileAttribute("RoomsExpanded.Settings.json", true)]
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
        [Limit(0.1f, 100.0f)]
        [DynamicOption(typeof(LogFloatOptionsEntry))]
        [Option("GermsToSustainPerSecond", "Clearing that many Zombie Spores will regenerate 30W/s of Biobot's battery")]
        public float GermsToSustainPerSecondInThousands { get; set; }

        public Settings()
        {
            GermsToSustainPerSecondInThousands = 1.0f;
        }
    }
}
