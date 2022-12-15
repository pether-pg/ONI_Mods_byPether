using HarmonyLib;
using Klei;
using KMod;
using ProcGen;
using System.Collections.Generic;
using System.Linq;
using TUNING;

namespace Dupes_Aromatics
{
    class Aromatics_Patches_Worldgen
    {
        public static Dictionary<string, AromaticsPlantsTuning.CropsTuning> CropsDictionary;

        [HarmonyPatch(typeof(Immigration), "ConfigureCarePackages")]
        public static class Immigration_ConfigureCarePackages_Patch
        {
            public static void Postfix(ref Immigration __instance)
            {
                Traverse traverse = Traverse.Create(__instance).Field("carePackages");
                List<CarePackageInfo> list = traverse.GetValue<CarePackageInfo[]>().ToList<CarePackageInfo>();
                list.Add(new CarePackageInfo(Plant_SpinosaConfig.SEED_ID , 3f, null));
                list.Add(new CarePackageInfo(Plant_DuskLavenderConfig.SEED_ID, 3f, null));
                list.Add(new CarePackageInfo(Plant_RimedMallowConfig.SEED_ID, 3f, null));
                traverse.SetValue(list.ToArray());
            }
        }

        [HarmonyPatch(typeof(SettingsCache), "LoadFiles", new System.Type[] { typeof(string), typeof(string), typeof(List<YamlIO.Error>) })]
        public static class SettingsCache_LoadFiles_Patch
        {
            public static void Postfix()
            {
                ComposableDictionary<string, Mob> mobLookupTable = SettingsCache.mobs.MobLookupTable;
                foreach (string str in Aromatics_Patches_Worldgen.CropsDictionary.Keys)
                {
                    if (!mobLookupTable.ContainsKey(str))
                    {
                        AromaticsPlantsTuning.CropsTuning tuning = Aromatics_Patches_Worldgen.CropsDictionary[str];
                        Mob mob1 = new Mob(tuning.spawnLocation);
                        mob1.name = str;
                        Mob root = mob1;
                        Traverse traverse = Traverse.Create(root);
                        traverse.Property("width", null).SetValue(1);
                        traverse.Property("height", null).SetValue(1);
                        traverse.Property("density", null).SetValue(tuning.density);
                        mobLookupTable.Add(str, root);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(SettingsCache), "LoadSubworlds")]
        public static class SettingsCache_LoadSubworlds_Patch
        {
            public static void Postfix()
            {
                foreach (SubWorld world in SettingsCache.subworlds.Values)
                    foreach (WeightedBiome biome in world.biomes)
                        foreach (string str in Aromatics_Patches_Worldgen.CropsDictionary.Keys)
                        {
                            AromaticsPlantsTuning.CropsTuning tuning = Aromatics_Patches_Worldgen.CropsDictionary[str];
                            if (tuning.ValidBiome(world, biome.name))
                            {
                                if (ReferenceEquals(biome.tags, null))
                                {
                                    Traverse.Create(biome).Property("tags", null).SetValue(new List<string>());
                                }
                                biome.tags.Add(str);
                            }
                        }
            }
        }
    }
}
