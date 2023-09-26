using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSerialization;

namespace DiseasesExpanded
{
    class PreviousGermIndex
    {
        public Dictionary<string, Dictionary<string, byte>> GermIdxById = new Dictionary<string, Dictionary<string, byte>>();

        private static PreviousGermIndex _instance;

        public static PreviousGermIndex Instance
        {
            get
            {
                if (_instance == null)
                    _instance = JsonSerializer<PreviousGermIndex>.Deserialize();
                if (_instance == null)
                {
                    _instance = new PreviousGermIndex();
                    _instance.Save();
                }
                return _instance;
            }
        }

        public static string GetCurrentBaseGuid()
        {
            if (SaveLoader.Instance == null)
                return string.Empty;

            return SaveLoader.Instance.GameInfo.colonyGuid.ToString();
        }

        public void Save()
        {
            JsonSerializer<PreviousGermIndex>.Serialize(_instance);
        }

        public Dictionary<string, byte> CreateCurrentGermIdxDict()
        {
            Dictionary<string, byte> dict = new Dictionary<string, byte>();
            
            for(byte idx = 0; idx < Db.Get().Diseases.Count; idx++)
                dict.Add(Db.Get().Diseases[idx].Id, idx);

            return dict;
        }

        public Dictionary<byte, byte> GetGermTranslationDict()
        {
            if (GermIdxById == null || GermIdxById.Count == 0)
                return null;

            Dictionary<byte, byte> dict = new Dictionary<byte, byte>();
            Dictionary<string, byte> currentGermDict = CreateCurrentGermIdxDict();
            string guid = GetCurrentBaseGuid();
            if (guid == string.Empty)
                return null;


            if (GermIdxById[guid] == null || GermIdxById[guid].Count == 0)
                return null;

            foreach (string germId in GermIdxById[guid].Keys)
            {
                byte newIdx = byte.MaxValue;
                if (currentGermDict.ContainsKey(germId))
                    newIdx = currentGermDict[germId];

                dict.Add(GermIdxById[guid][germId], newIdx);
            }

            Debug.Log($"{ModInfo.Namespace}: Germ OLD - NEW idx dictionary:");
            foreach (byte key in dict.Keys)
                Debug.Log($"{ModInfo.Namespace}: Old idx = {key}, New idx = {dict[key]}");

            return dict;
        }

        public void UpdateSavedDictionary()
        {
            string guid = GetCurrentBaseGuid();
            if(guid != string.Empty)
                GermIdxById[guid] = CreateCurrentGermIdxDict();
        }

        public void LogDictionary()
        {
            Debug.Log($"{ModInfo.Namespace}: Germ ID - IDX dictionary:");

            string guid = GetCurrentBaseGuid();
            if (guid == string.Empty)
            {
                Debug.Log($"{ModInfo.Namespace}: (base GUID empty)");
                return;
            }

            if (GermIdxById == null || GermIdxById.Count == 0)
            {
                Debug.Log($"{ModInfo.Namespace}: (global dict empty)");
                return;
            }

            if (GermIdxById[guid] == null || GermIdxById[guid].Count == 0)
            {
                Debug.Log($"{ModInfo.Namespace}: (base dict empty)");
                return;
            }

            foreach (string key in GermIdxById[guid].Keys)
                Debug.Log($"{ModInfo.Namespace}: Germ Id = {key}, Idx = {GermIdxById[guid][key]}");
        }
    }
}
