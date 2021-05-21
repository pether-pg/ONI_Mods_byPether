using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace RoomsExpanded
{
    class ModInfo
    {
        private static ModInfo prv_instance = null;

        public static ModInfo Instance
        {
            get {
                if (prv_instance != null)
                    return prv_instance;
                throw new NotImplementedException("Mod info not initalized correctly...");
            }            
        }

        string LastUpdate { set; get; }

        bool UsesExpansion { set; get; }

        int LastBuild { set; get; }

        private ModInfo(string version, bool usesExpansion, int build)
        {
            LastUpdate = version;
            UsesExpansion = usesExpansion;
            LastBuild = build;
        }

        public static void Initalize(System.DateTime updatedDate, bool usesExpansion, int build)
        {
            prv_instance = new ModInfo(updatedDate.ToString("yyyy.MM.dd"), usesExpansion, build);
        }

        public static void Initalize(string updateVersion, bool usesExpansion, int build)
        {
            prv_instance = new ModInfo(updateVersion, usesExpansion, build);
        }

        public static string GetAssemblyVersion()
        {
            string assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string fileVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
            return fileVersion;
        }

        public void LogDetails()
        {
            Debug.Log(GetDescription());
            Debug.Log($"{GetType().Namespace}: Loaded from: {Assembly.GetExecutingAssembly().Location}");
        }

        public string GetDescription()
        {            
            string modName = GetType().Namespace;
            string dlc = UsesExpansion ? "DLC" : "Vanilla";
            return $"{modName}: Loaded {dlc} version of the mod. Last update: {LastUpdate} for build {LastBuild}.";
        }

        public void VersionAlert(bool expectDLC, string details = "")
        {
            if (expectDLC != UsesExpansion)
            {
                string message = "Resolve Vanilla/DLC version differences";
                if (!string.IsNullOrEmpty(details))
                    message += $" : {details}";
                throw new NotSupportedException(message);
            }
        }

        public bool ExecutedWithDLC()
        {
            return UsesExpansion;
        }
    }
}
