using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using KMod;

namespace RoomsExpanded
{
    class ModInfo : KMod.UserMod2
    {
        public static string Namespace { get; private set; }

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);

            Namespace = GetType().Namespace;
            Debug.Log($"{Namespace}: Loaded from: {this.mod.ContentPath}");
            Debug.Log($"{Namespace}: Mod version: {this.mod.packagedModInfo.version} " +
                        $"supporting game build {this.mod.packagedModInfo.minimumSupportedBuild} ({this.mod.packagedModInfo.supportedContent})");

            BackupConfig.Instance.RestoreBackup(JsonSerializer<Settings>.GetDefaultFilename());

            PUtil.InitLibrary();
            new POptions().RegisterOptions(this, typeof(Settings));
            Settings.PLib_Initalize();

            Debug.Log($"{Namespace}: POptions registered!");
        }

        public override void OnAllModsLoaded(Harmony harmony, IReadOnlyList<Mod> mods)
        {
            base.OnAllModsLoaded(harmony, mods);
            List<string> activeMods = new List<string>();
            foreach (Mod mod in mods)            
                if (mod.IsActive())
                    activeMods.Add(mod.staticID);
            CrossModManager.Initalize(activeMods);
        }
    }
}
