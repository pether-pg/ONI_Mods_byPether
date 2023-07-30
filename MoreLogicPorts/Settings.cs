using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreLogicPorts
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
                if (_instance == null)
                {
                    _instance = new Settings();
                    JsonSerializer<Settings>.Serialize(_instance);
                }
                return _instance;
            }
        }

        public Dictionary<string, bool> ShouldAddDisablePort;

        public Settings()
        {
            ShouldAddDisablePort = new Dictionary<string, bool>();
            foreach (Type config in LogPorts.ConfigsToAddPorts().Keys)
                ShouldAddDisablePort.Add(config.Name, true);
        }

        public bool CanAddPort(Type config)
        {
            string configName = config.Name;

            if (ShouldAddDisablePort.ContainsKey(configName))
                return ShouldAddDisablePort[configName];

            ShouldAddDisablePort.Add(configName, false);
            JsonSerializer<Settings>.Serialize(_instance);

            return false;
        }
    }
}
