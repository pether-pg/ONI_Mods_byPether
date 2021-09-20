using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerStorage
{
    class StorageOwnershipInfo
    {
        private static StorageOwnershipInfo _instance;

        public static StorageOwnershipInfo Instance
        {
            get
            {
                if (_instance == null)
                    DeserializeInstance();
                if (_instance == null)
                {
                    _instance = new StorageOwnershipInfo();
                    SerializeInstance();
                }
                return _instance;
            }
            set { _instance = value; }
        }

        public string CurrentOwner;
        public string LastContributor;
        public string LastModification;

        public static string GetOwner()
        {
            return string.Format("{0} - {1}", SaveLoader.Instance.GameInfo.colonyGuid, SaveGame.Instance.BaseName); 
        }

        public static string GetCurrentTimeString()
        {
            return System.DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss");
        }

        public static void SerializeInstance()
        {
            string dir = Settings.Instance.StorageFilePath;
            JsonSerializer<StorageOwnershipInfo>.Serialize(_instance, JsonSerializer<StorageOwnershipInfo>.GetFullPath(dir));
        }

        public static void DeserializeInstance()
        {
            string dir = Settings.Instance.StorageFilePath;
            _instance = JsonSerializer<StorageOwnershipInfo>.Deserialize(JsonSerializer<StorageOwnershipInfo>.GetFullPath(dir));
        }

        public static bool CanAssumeControl()
        {
            return string.IsNullOrEmpty(Instance.CurrentOwner);
        }

        public static bool InControl()
        {
            return Instance.CurrentOwner == GetOwner();
        }

        public static void AssumeControl()
        {
            Instance.CurrentOwner = GetOwner();
            Instance.LastModification = GetCurrentTimeString();
            SerializeInstance();
        }

        public static void ReleaseControl()
        {
            Instance.CurrentOwner = null;
            Instance.LastContributor = GetOwner();
            Instance.LastModification = GetCurrentTimeString();
            SerializeInstance();
        }
    }
}
