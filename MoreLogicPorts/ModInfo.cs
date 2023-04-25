using HarmonyLib;
using KMod;
using System.Collections.Generic;

namespace MoreLogicPorts
{
    class ModInfo : KMod.UserMod2
    {
        public static string Namespace { get; private set; }
        public static bool IsCheckpointAutomationActive { get; private set; }

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            Namespace = GetType().Namespace;
            Debug.Log($"{Namespace}: Loaded from: {this.mod.ContentPath}");
            Debug.Log($"{Namespace}: Mod version: {this.mod.packagedModInfo.version} " +
                        $"supporting game build {this.mod.packagedModInfo.minimumSupportedBuild} ({this.mod.packagedModInfo.supportedContent})");
        }

        public override void OnAllModsLoaded(Harmony harmony, IReadOnlyList<Mod> mods)
        {
            foreach (var mod in mods)
                if(mod.staticID == "pether-pg.CheckpointAutomation")
                {
                    IsCheckpointAutomationActive = mod.IsActive();
                    break;
                }
        }
    }
}
