using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using ProcGen;
using static ProcGen.World;
using static ProcGen.World.AllowedCellsFilter;
using static ProcGen.World.TemplateSpawnRules;

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

                if (!DlcManager.IsContentSubscribed(DlcManager.DLC3_ID))
                    return;

                foreach (string biomeKey in SettingsCache.biomes.BiomeBackgroundElementBandConfigurations.Keys)
                    foreach (ElementGradient eg in SettingsCache.biomes.BiomeBackgroundElementBandConfigurations[biomeKey])
                    {
                        if(eg.content == SimHashes.Rust.CreateTag())
                            eg.overrides = new SampleDescriber.Override(null, null, null, null, RustGerms.ID, 1000000);
                    }
            }
        }

        [HarmonyPatch(typeof(GeyserGenericConfig))]
        [HarmonyPatch("GenerateConfigs")]
        public static class GeyserGenericConfig_GenerateConfigs_Patch
        {
            public static void Postfix(ref List<GeyserGenericConfig.GeyserPrefabParams> __result)
            {
                if (!Settings.Instance.RustDust.IncludeDisease)
                    return;

                if (!DlcManager.IsContentSubscribed(DlcManager.DLC3_ID))
                    return;

                // __result.RemoveAll(p => p.geyserType.element == SimHashes.Water);
                __result.Add(RustyWaterGeyser_Data.GetGenericGeyserPrefabParams());
                __result.Add(RustyWaterGeyser_Data.GetGenericGeyserPrefabParams(2));
            }
        }
    }
}
