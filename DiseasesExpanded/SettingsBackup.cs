using System;
using System.IO;
using System.Reflection;

namespace DiseasesExpanded
{
    class SettingsBackup
    {
        private static SettingsBackup _instance;

        public static SettingsBackup Instance
        {
            get
            {
                if (_instance == null)
                    _instance = JsonSerializer<SettingsBackup>.Deserialize();
                if (_instance == null)
                {
                    _instance = new SettingsBackup();
                    JsonSerializer<SettingsBackup>.Serialize(_instance);
                }
                return _instance;
            }
        }

        public bool UseSettingsBackup = false;
        public string BackupPath = "../PreservedModsFiles/";

        public void StoreBackup(string filename)
        {
            if (!UseSettingsBackup)
                return;
            File.Copy(GetOriginalPath(filename), GetBackupPath(filename), true);
        }

        public void RestoreBackup(string filename)
        {
            if (!UseSettingsBackup)
                return;
            if (!File.Exists(GetBackupPath(filename)))
                return;
            File.Copy(GetBackupPath(filename), GetOriginalPath(filename), true);
        }

        public string GetOriginalPath(string filename)
        {
            string moddir = Path.GetDirectoryName(Assembly.GetAssembly(typeof(SettingsBackup)).Location);
            return Path.Combine(moddir, filename);
        }

        public string GetBackupPath(string filename)
        {
            string moddir = Path.GetDirectoryName(Assembly.GetAssembly(typeof(SettingsBackup)).Location);
            string filedir = UseSettingsBackup ? BackupPath : "";
            string fulldir = Path.Combine(moddir, filedir);
            if (!Directory.Exists(fulldir))
                Directory.CreateDirectory(fulldir);
            return Path.Combine(fulldir, filename);
        }
    }
}
