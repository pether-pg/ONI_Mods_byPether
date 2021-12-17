using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResearchRequirements
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
                    _instance.PopulateDictionary();
                    JsonSerializer<Settings>.Serialize(_instance);
                }
                return _instance;
            }
        }

        public float DiffictultyScale = 1f;
        public Dictionary<string, bool> Ignored;

        private void PopulateDictionary()
        {
            Ignored = new Dictionary<string, bool>();
            foreach (string tech in TechRequirements.Instance.GetAllTechIds())
                Ignored.Add(tech, false);
        }

    }
}
