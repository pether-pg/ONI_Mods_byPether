using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomsExpanded
{
    [Serializable]
    public class Settings
    {
        public class RoomSettings
        {
            public bool IncludeRoom;
            public int MaxSize;
            public float? Bonus;

            public RoomSettings(bool include, int max, float? bonus = null)
            {
                IncludeRoom = include;
                MaxSize = max;
                Bonus = bonus;
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

        private Settings()
        { }

        public bool HideLegendEffect = true;
        public string EnforcedLanguage = "";
        public int ResizeMaxRoomSize64 = 64;
        public int ResizeMaxRoomSize96 = 96;
        public int ResizeMaxRoomSize120 = 120;
        public RoomSettings Laboratory = new RoomSettings(true, 64, 0.1f);
        public RoomSettings Kitchen = new RoomSettings(true, 64, 0.1f);
        public RoomSettings Bathroom = new RoomSettings(true, 64, 0.2f);
        public RoomSettings Industrial = new RoomSettings(false, 96);
        public RoomSettings Graveyard = new RoomSettings(false, 96, 0.2f);
        public RoomSettings Agricultural = new RoomSettings(true, 96);
        public RoomSettings Gym = new RoomSettings(true, 64, 0.1f);
        public RoomSettings Nursery = new RoomSettings(true, 64, 0.1f);
        public RoomSettings Aquarium = new RoomSettings(true, 96, 0.2f);
        public RoomSettings Botanical = new RoomSettings(true, 96, null);
        public RoomSettings Museum = new RoomSettings(true, 96, 0.3f);
        public RoomSettings HospitalUpdate = new RoomSettings(true, 96, null);
    }
}
