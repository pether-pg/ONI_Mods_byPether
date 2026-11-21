using HarmonyLib;
using KMod;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

namespace ConveyorRailDisplay
{
    class ModInfo : KMod.UserMod2
    {
        public static string Namespace { get; private set; }
        private const string RelatedModStaticId = "pether-pg.CombinedConduitDisplay";

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);

            var requiredDlcs = this.mod.packagedModInfo.GetRequiredDlcIds();
            string dlcInfo = "All";
            if (requiredDlcs != null)
                dlcInfo = string.Join(", ", requiredDlcs);

            Namespace = GetType().Namespace;
            Debug.Log($"{Namespace}: Loaded from: {this.mod.ContentPath}");
            Debug.Log($"{Namespace}: DLL version: {GetType().Assembly.GetName().Version} " +
                        $"supporting game build {this.mod.packagedModInfo.minimumSupportedBuild} ({dlcInfo})");
        }

        public override void OnAllModsLoaded(Harmony harmony, IReadOnlyList<Mod> mods)
        {
            base.OnAllModsLoaded(harmony, mods);

            foreach (Mod mod in mods)
                if (mod.staticID == RelatedModStaticId && mod.IsActive())
                {
                    Debug.Log($"{Namespace}: {RelatedModStaticId} is found, aborting any patching not to duplicate functionality.");
                    return;
                }

            Debug.Log($"{Namespace}: {RelatedModStaticId} not found, trying to manually patch methods for {Namespace}");

            ConveyorRailDisplay_Patches.TryPatchAll(harmony);
        }
    }
}
