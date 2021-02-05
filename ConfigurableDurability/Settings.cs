using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigurableDurability
{
    class Settings
    {
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

        public float durabilityLossPerCycle = -0.1f;
    }
}
