using System;
using System.IO;
using System.Reflection;

namespace ConfigurableBuildMenus
{
    class ConfigPath
    {

        public static ConfigPath _instance;

        public static ConfigPath Intance
        {
            get
            {
                if (_instance == null)
                    _instance = JsonSerializer<ConfigPath>.Deserialize();
                if (_instance == null)
                {
                    _instance = new ConfigPath();
                    JsonSerializer<ConfigPath>.Serialize(_instance);
                }
                return _instance;
            }
        }

        public string Instruction = "When UseExternalDirectory = false, the mod will load config from mod's directory." +
            " When UseExternalDirectory = true, the config will be loaded from ExternalDirectoryPath. Use it to preserve your config from being overwritten by Steam updates to the mod.";
        public bool UseExternalDirectory = false;
        public string ExternalDirectoryPath = "../PreservedModsFiles/";

        public string GetPathForFile(string filename)
        {
            string moddir = Path.GetDirectoryName(Assembly.GetAssembly(typeof(ConfigPath)).Location);
            string filedir = UseExternalDirectory ? ExternalDirectoryPath : "";
            string fulldir = Path.Combine(moddir, filedir);
            if (!Directory.Exists(fulldir))
                Directory.CreateDirectory(fulldir);
            return Path.Combine(fulldir, filename);
        }
    }
}
