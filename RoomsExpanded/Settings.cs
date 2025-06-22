﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PeterHan.PLib;
using PeterHan.PLib.Options;
using UnityEngine;

namespace RoomsExpanded
{
    [Serializable]
    [RestartRequired]
    [ConfigFileAttribute("RoomsExpanded.Settings.json", true)]
    public class Settings :IOptions
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
            public float Bonus { get; set; }

            [JsonProperty]
            [Option("RoomColor", "What color do you want to see in the Room Overlay?")]
            public Color32 RoomColor { get; set; }

            public RoomSettings(bool include, int max, Color32 color, float bonus)
            {
                IncludeRoom = include;
                MaxSize = max;
                Bonus = bonus;
                RoomColor = color;
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

            [JsonProperty]
            [Option("RoomColor", "What color do you want to see in the Room Overlay?")]
            public Color32 RoomColor { get; set; }

            public PlainRoomSettings(bool include, int max, Color32 color)
            {
                IncludeRoom = include;
                MaxSize = max;
                RoomColor = color;
            }
        }
            
        private static Settings _instance = null;
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

        public Settings()
        {
            HideLegendEffect = true; 
            EnforcedLanguage = "";

            Kitchenette = new RoomSettings(true, 64, ColorPalette.RoomFood, 0.1f);
            Bathroom = new RoomSettings(true, 64, ColorPalette.RoomBathroom, 0.2f);
            Industrial = new PlainRoomSettings(false, 96, ColorPalette.RoomIndustrial);
            Graveyard = new RoomSettings(false, 96, ColorPalette.RoomPark, 0.2f);
            Agricultural = new PlainRoomSettings(true, 96, ColorPalette.RoomAgricultural);
            Gym = new RoomSettings(true, 64, ColorPalette.RoomRecreation, 0.1f);
            Nursery = new RoomSettings(!DlcManager.IsContentSubscribed(DlcManager.EXPANSION1_ID), 64, ColorPalette.RoomAgricultural, 0.1f);
            Aquarium = new RoomSettings(false, 96, ColorPalette.RoomBathroom, 0.2f);
            Botanical = new PlainRoomSettings(true, 96, ColorPalette.RoomPark);
            Museum = new RoomSettings(true, 120, ColorPalette.RoomHospital, 0.3f);
            MuseumSpace = new RoomSettings(true, 120, ColorPalette.RoomRecreation, 0.3f);
            MuseumHistory = new RoomSettings(DlcManager.IsContentSubscribed(DlcManager.DLC4_ID), 120, ColorPalette.RoomRecreation, 0.3f);
            HospitalUpdate = new PlainRoomSettings(true, 96, ColorPalette.RoomHospital);
            NurseryGenetic = new RoomSettings(true, 96, ColorPalette.RoomAgricultural, 0.2f);
            MissionControl = new PlainRoomSettings(true, 96, ColorPalette.RoomScience);
            BionicWorkshop = new PlainRoomSettings(DlcManager.IsContentSubscribed(DlcManager.DLC3_ID), 64, ColorPalette.RoomBathroom);
            DataMiningCenter = new RoomSettings(DlcManager.IsContentSubscribed(DlcManager.DLC3_ID), 96, ColorPalette.RoomScience, 0.2f);

            ResizeMinRoomSize12 = 12;
            ResizeMinRoomSize24 = 24;
            ResizeMinRoomSize32 = 32;
            ResizeMaxRoomSize64 = 64;
            ResizeMaxRoomSize96 = 96;
            ResizeMaxRoomSize120 = 120;
        }

        [JsonProperty]
        [Option("Hide Legend Effect", "HideLegendEffect", category: "RoomsExpanded: Additional")]
        public bool HideLegendEffect { get; set; }

        [JsonProperty]
        [Limit(0, 2)]
        [Option("Enforced Language", "EnforcedLanguage", category: "RoomsExpanded: Additional")]
        public string EnforcedLanguage { get; set; }

        [JsonProperty]
        [Option("Kitchenette", category: "New Room - Kitchenette")]
        public RoomSettings Kitchenette { get; set; }

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
        [Option("Genetic Nursery", category: "New Room - Genetic Nursery")]
        public RoomSettings NurseryGenetic { get; set; }

        [JsonProperty]
        [Option("Space Museum", category: "New Room - Space Museum")]
        public RoomSettings MuseumSpace { get; set; }

        [JsonProperty]
        [Option("Mission Control", category: "New Room - Mission Control")]
        public PlainRoomSettings MissionControl { get; set; }

        [JsonProperty]
        [Option("Bionic Workshop", category: "New Room - Bionic Workshop")]
        public PlainRoomSettings BionicWorkshop { get; set; }

        [JsonProperty]
        [Option("Data Mining Center", category: "New Room - Data Mining Center")]
        public RoomSettings DataMiningCenter { get; set; }

        [JsonProperty]
        [Option("History Museum", category: "New Room - History Museum")]
        public RoomSettings MuseumHistory { get; set; }

        [JsonProperty]
        [Limit(1, 12)]
        [Option("ResizeMinRoomSize12", "Change min room size from 12 tiles", category: "RoomSize")]
        public int ResizeMinRoomSize12 { get; set; }

        [JsonProperty]
        [Limit(1, 24)]
        [Option("ResizeMinRoomSize24", "Change min room size from 24 tiles", category: "RoomSize")]
        public int ResizeMinRoomSize24 { get; set; }

        [JsonProperty]
        [Limit(1, 32)]
        [Option("ResizeMinRoomSize32", "Change min room size from 32 tiles", category: "RoomSize")]
        public int ResizeMinRoomSize32 { get; set; }

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

        //[JsonProperty]
        //[Limit(128, 256)]
        //[Option("ResizeMaxRoomSize", "Change max room size - temporal DLC support for \"Room Size\" mod id=1715802131", category: "RoomSize")]
        //public int ResizeMaxRoomSize { get; set; }

        public int GetMaxRoomSize()
        {
            int max = 128;
            max = Math.Max(max, ResizeMaxRoomSize64);
            max = Math.Max(max, ResizeMaxRoomSize96);
            max = Math.Max(max, ResizeMaxRoomSize120);
            max = Math.Max(max, Kitchenette.MaxSize);
            max = Math.Max(max, Bathroom.MaxSize);
            max = Math.Max(max, Industrial.MaxSize);
            max = Math.Max(max, Graveyard.MaxSize);
            max = Math.Max(max, Agricultural.MaxSize);
            max = Math.Max(max, Gym.MaxSize);
            max = Math.Max(max, Nursery.MaxSize);
            max = Math.Max(max, Aquarium.MaxSize);
            max = Math.Max(max, Botanical.MaxSize);
            max = Math.Max(max, Museum.MaxSize);
            max = Math.Max(max, MuseumHistory.MaxSize);
            max = Math.Max(max, MuseumSpace.MaxSize);
            max = Math.Max(max, HospitalUpdate.MaxSize);
            max = Math.Max(max, MissionControl.MaxSize);
            max = Math.Max(max, NurseryGenetic.MaxSize);
            max = Math.Max(max, BionicWorkshop.MaxSize);

            return max;
        }

        public IEnumerable<IOptionsEntry> CreateOptions()
        {
            // list of additional options, can be empty
            return new List<IOptionsEntry>();
        }

        public void OnOptionsChanged()
        {
            BackupConfig.Instance.StoreBackup(JsonSerializer<Settings>.GetDefaultFilename());
        }
    }
}
