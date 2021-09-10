using System.Reflection;

namespace MultiplayerStorage
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
                    _instance.StorageFilePath = System.IO.Path.GetDirectoryName(Assembly.GetAssembly(typeof(Settings)).Location);
                    JsonSerializer<Settings>.Serialize(_instance);
                }
                return _instance;
            }
            set { _instance = value; }
        }

        public string StorageFilePath;
        public bool OneTimeEnforceControl = false;
    }
}
