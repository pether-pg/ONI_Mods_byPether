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
        public string Info = $"DO NOT EDIT! This file contains information about previously enabled germs in your saves. " +
                                $"Without it, if you would disable some germs, some entombed items could change a type of germs infecting it, or even crash the game.";
        public Dictionary<string, Dictionary<string, byte>> GermIdxByIdAndGuid = new Dictionary<string, Dictionary<string, byte>>();

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
            if (GermIdxByIdAndGuid == null || GermIdxByIdAndGuid.Count == 0)
                return null;

            Dictionary<byte, byte> dict = new Dictionary<byte, byte>();
            Dictionary<string, byte> currentGermDict = CreateCurrentGermIdxDict();
            string guid = GetCurrentBaseGuid();
            if (guid == string.Empty || !GermIdxByIdAndGuid.ContainsKey(guid))
                return null;


            if (GermIdxByIdAndGuid[guid] == null || GermIdxByIdAndGuid[guid].Count == 0)
                return null;

            foreach (string germId in GermIdxByIdAndGuid[guid].Keys)
            {
                byte newIdx = byte.MaxValue;
                if (currentGermDict.ContainsKey(germId))
                    newIdx = currentGermDict[germId];

                dict.Add(GermIdxByIdAndGuid[guid][germId], newIdx);
            }

            Debug.Log($"{ModInfo.Namespace}: Germ OLD - NEW idx dictionary:");
            foreach (byte key in dict.Keys)
                Debug.Log($"{ModInfo.Namespace}: Old idx = {key}, New idx = {dict[key]}");

            return dict;
        }

        public void UpdateSavedDictionary()
        {
            string guid = GetCurrentBaseGuid();
            if (guid == string.Empty)
                return;

            if (!GermIdxByIdAndGuid.ContainsKey(guid))
                GermIdxByIdAndGuid.Add(guid, new Dictionary<string, byte>());
            GermIdxByIdAndGuid[guid] = CreateCurrentGermIdxDict();
        }

        public void LogDictionary()
        {
            Debug.Log($"{ModInfo.Namespace}: Germ ID - IDX dictionary:");

            string guid = GetCurrentBaseGuid();
            if (guid == string.Empty || !GermIdxByIdAndGuid.ContainsKey(guid))
            {
                Debug.Log($"{ModInfo.Namespace}: (base GUID empty)");
                return;
            }

            if (GermIdxByIdAndGuid == null || GermIdxByIdAndGuid.Count == 0)
            {
                Debug.Log($"{ModInfo.Namespace}: (global dict empty)");
                return;
            }

            if (GermIdxByIdAndGuid[guid] == null || GermIdxByIdAndGuid[guid].Count == 0)
            {
                Debug.Log($"{ModInfo.Namespace}: (base dict empty)");
                return;
            }

            foreach (string key in GermIdxByIdAndGuid[guid].Keys)
                Debug.Log($"{ModInfo.Namespace}: Germ Id = {key}, Idx = {GermIdxByIdAndGuid[guid][key]}");
        }
    }
}
