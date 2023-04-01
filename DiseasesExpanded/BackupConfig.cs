using System;
using System.IO;
using System.Reflection;

namespace DiseasesExpanded
{
    class BackupConfig
    {
        private static BackupConfig _instance;

        public static BackupConfig Instance
        {
            get
            {
                if (_instance == null)
                    _instance = JsonSerializer<BackupConfig>.Deserialize();
                if (_instance == null)
                {
                    _instance = new BackupConfig();
                    JsonSerializer<BackupConfig>.Serialize(_instance);
                }
                return _instance;
            }
        }
        public string Instruction = $"When {nameof(StoreSettingsCopy)} = false, the backup feature will be ignored." +
            $" When {nameof(StoreSettingsCopy)} = true, the settings file will be copied and restored from {nameof(BackupPath)}. " +
            $"Use it to preserve your config from being overwritten by Steam updates to the mod. " +
            $"{nameof(BackupPath)} can be either absolute or relative.";
        public bool StoreSettingsCopy = false;
        public string BackupPath = "../PreservedModsFiles/";

        public void StoreBackup(string filename)
        {
            if (!StoreSettingsCopy)
                return;
            File.Copy(GetOriginalPath(filename), GetBackupPath(filename), true);
        }

        public void RestoreBackup(string filename)
        {
            if (!StoreSettingsCopy)
                return;
            if (!File.Exists(GetBackupPath(filename)))
                return;
            File.Copy(GetBackupPath(filename), GetOriginalPath(filename), true);
        }

        public string GetOriginalPath(string filename)
        {
            string moddir = Path.GetDirectoryName(Assembly.GetAssembly(typeof(BackupConfig)).Location);
            return Path.Combine(moddir, filename);
        }

        public string GetBackupPath(string filename)
        {
            string moddir = Path.GetDirectoryName(Assembly.GetAssembly(typeof(BackupConfig)).Location);
            string filedir = StoreSettingsCopy ? BackupPath : "";
            string fulldir = Path.Combine(moddir, filedir);
            if (!Directory.Exists(fulldir))
                Directory.CreateDirectory(fulldir);
            return Path.Combine(fulldir, filename);
        }
    }
}
