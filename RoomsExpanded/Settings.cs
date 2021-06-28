using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using PeterHan.PLib;
using PeterHan.PLib.Options;

namespace RoomsExpanded
{
    [Serializable]
    [RestartRequired]
    [ConfigFileAttribute("RoomsExpanded.Settings.json", true)]
    public class Settings
    {
        [Serializable]
        public class RoomSettings
        {
            [JsonProperty]
            [Option("IncludeRoom", "Do you want to play with this room?")]
            public bool IncludeRoom { get; set; }

            [JsonProperty]
            [Limit(12, 256)]
            [Option("MaxSize", "How big this room can be?")]
            public int MaxSize { get; set; }
            
            [JsonProperty]
            [Limit(0.0, 1.0)]
            [Option("Bonus", "How big bonus the room should provide?")]
            public float? Bonus { get; set; }

            public RoomSettings(bool include, int max, float? bonus = null)
            {
                IncludeRoom = include;
                MaxSize = max;
                Bonus = bonus;
            }
        }

        [Serializable]
        public class PlainRoomSettings
        {
            [JsonProperty]
            [Option("IncludeRoom", "Do you want to play with this room?")]
            public bool IncludeRoom { get; set; }

            [JsonProperty]
            [Limit(12, 256)]
            [Option("MaxSize", "How big this room can be?")]
            public int MaxSize { get; set; }

            public PlainRoomSettings(bool include, int max)
            {
                IncludeRoom = include;
                MaxSize = max;
            }
        }
            
        private static Settings _instance = null;
        public static Settings Instance
        {
            get 
            {
                if (_instance == null)
                    _instance =  JsonSerializer<Settings>.Deserialize();
                if (_instance == null)
                {
                    _instance = new Settings();
                    JsonSerializer<Settings>.Serialize(_instance);
                }
                return _instance; 
            }
        }

        public Settings()
        {
            HideLegendEffect = true; 
            EnforcedLanguage = "";

            Laboratory = new RoomSettings(true, 64, 0.1f);
            Kitchen = new RoomSettings(true, 64, 0.1f);
            Bathroom = new RoomSettings(true, 64, 0.2f);
            Industrial = new PlainRoomSettings(false, 96);
            Graveyard = new RoomSettings(false, 96, 0.2f);
            Agricultural = new PlainRoomSettings(true, 96);
            Gym = new RoomSettings(true, 64, 0.1f);
            Nursery = new RoomSettings(true, 64, 0.1f);
            Aquarium = new RoomSettings(true, 96, 0.2f);
            Botanical = new PlainRoomSettings(true, 96);
            Museum = new RoomSettings(true, 96, 0.3f);
            HospitalUpdate = new PlainRoomSettings(true, 96);
            PrivateBedroom = new PlainRoomSettings(true, 32);

            ResizeMaxRoomSize64 = 64;
            ResizeMaxRoomSize96 = 96;
            ResizeMaxRoomSize120 = 120;
            ResizeMaxRoomSize = 128;
        }

        [JsonProperty]
        [Option("Hide Legend Effect", "HideLegendEffect", category: "RoomsExpanded: Additional")]
        public bool HideLegendEffect { get; set; }

        [JsonProperty]
        [Limit(0, 2)]
        [Option("Enforced Language", "EnforcedLanguage", category: "RoomsExpanded: Additional")]
        public string EnforcedLanguage { get; set; }

        [JsonProperty]
        [Option("Laboratory", category: "New Room - Laboratory")]
        public RoomSettings Laboratory { get; set; }

        [JsonProperty]
        [Option("Kitchen", category: "New Room - Kitchen")]
        public RoomSettings Kitchen { get; set; }

        [JsonProperty]
        [Option("Bathroom", category: "New Room - Shower Room")]
        public RoomSettings Bathroom { get; set; }

        [JsonProperty]
        [Option("Industrial", category: "New Room - Industrial Room")]
        public PlainRoomSettings Industrial { get; set; }

        [JsonProperty]
        [Option("Graveyard", category: "New Room - Graveyard")]
        public RoomSettings Graveyard { get; set; }

        [JsonProperty]
        [Option("Agricultural", category: "New Room - Agricultural")]
        public PlainRoomSettings Agricultural { get; set; }

        [JsonProperty]
        [Option("Gym", category: "New Room - Gym")]
        public RoomSettings Gym { get; set; }

        [JsonProperty]
        [Option("Nursery", category: "New Room - Plant Nursery")]
        public RoomSettings Nursery { get; set; }

        [JsonProperty]
        [Option("Aquarium", category: "New Room - Aquarium")]
        public RoomSettings Aquarium { get; set; }

        [JsonProperty]
        [Option("Botanical Garden", category: "New Room - Botanical Garden")]
        public PlainRoomSettings Botanical { get; set; }

        [JsonProperty]
        [Option("Museum", category: "New Room - Museum")]
        public RoomSettings Museum { get; set; }

        [JsonProperty]
        [Option("Hospital Update", category: "Room Modification - Hospital")]
        public PlainRoomSettings HospitalUpdate { get; set; }

        [JsonProperty]
        [Option("Private Bedroom", category: "New Room - Private Bedroom")]
        public PlainRoomSettings PrivateBedroom { get; set; }

        [JsonProperty]
        [Limit(12, 256)]
        [Option("ResizeMaxRoomSize64", "Change max room size from 64 tiles - temporal DLC support for \"Room Size\" mod id=1715802131", category: "RoomSize")]
        public int ResizeMaxRoomSize64 { get; set; }

        [JsonProperty]
        [Limit(12, 256)]
        [Option("ResizeMaxRoomSize96", "Change max room size from 96 tiles - temporal DLC support for \"Room Size\" mod id=1715802131", category: "RoomSize")]
        public int ResizeMaxRoomSize96 { get; set; }

        [JsonProperty]
        [Limit(12, 256)]
        [Option("ResizeMaxRoomSize120", "Change max room size from 120 tiles - temporal DLC support for \"Room Size\" mod id=1715802131", category: "RoomSize")]
        public int ResizeMaxRoomSize120 { get; set; }

        [JsonProperty]
        [Limit(128, 256)]
        [Option("ResizeMaxRoomSize", "Change max room size - temporal DLC support for \"Room Size\" mod id=1715802131", category: "RoomSize")]
        public int ResizeMaxRoomSize { get; set; }

        public static void PLib_Initalize()
        {
            _instance = POptions.ReadSettings<Settings>();
        }
    }
}
