using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using ProcGen;

namespace DiseasesExpanded.Patches
{
    class DiseasesExpanded_Patches_Worldgen
    {
        [HarmonyPatch(typeof(SettingsCache))]
        [HarmonyPatch(nameof(SettingsCache.LoadSubworlds))]
        public static class SettingsCache_LoadSubworlds_Patch
        {
            public static void Postfix()
            {
                if (!Settings.Instance.RustDust.IncludeDisease)
                    return;

                foreach(string biomeKey in SettingsCache.biomes.BiomeBackgroundElementBandConfigurations.Keys)
                    foreach (ElementGradient eg in SettingsCache.biomes.BiomeBackgroundElementBandConfigurations[biomeKey])
                    {
                        if(eg.content == SimHashes.Rust.CreateTag())
                            eg.overrides = new SampleDescriber.Override(null, null, null, null, RustGerms.ID, 1000000);
                    }
            }
        }
    }
}
