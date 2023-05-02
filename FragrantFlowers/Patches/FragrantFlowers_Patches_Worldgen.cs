using HarmonyLib;
using Klei;
using KMod;
using ProcGen;
using System.Collections.Generic;
using System.Linq;
using TUNING;

namespace FragrantFlowers
{
    class FragrantFlowers_Patches_Worldgen
    {
        public static Dictionary<string, FragrantPlantsTuning.CropsTuning> CropsDictionary;

        public static void InitCropDictionary()
        {
            CropsDictionary = new Dictionary<string, FragrantPlantsTuning.CropsTuning>();
            CropsDictionary.Add(Plant_SpinosaConfig.ID, FragrantPlantsTuning.SpinrosaTuning);
            CropsDictionary.Add(Plant_DuskLavenderConfig.ID, FragrantPlantsTuning.DuskbloomTuning);
            CropsDictionary.Add(Plant_RimedMallowConfig.ID, FragrantPlantsTuning.MallowTuning);
        }

        [HarmonyPatch(typeof(Immigration), "ConfigureCarePackages")]
        public static class Immigration_ConfigureCarePackages_Patch
        {
            public static void Postfix(ref Immigration __instance)
            {
                Traverse traverse = Traverse.Create(__instance).Field("carePackages");
                List<CarePackageInfo> list = traverse.GetValue<CarePackageInfo[]>().ToList<CarePackageInfo>();
                list.Add(new CarePackageInfo(Plant_SpinosaConfig.SEED_ID , Settings.Instance.Rose.SeedsInCarePackage, null));
                list.Add(new CarePackageInfo(Plant_DuskLavenderConfig.SEED_ID, Settings.Instance.Lavender.SeedsInCarePackage, null));
                list.Add(new CarePackageInfo(Plant_RimedMallowConfig.SEED_ID, Settings.Instance.Mallow.SeedsInCarePackage, null));
                traverse.SetValue(list.ToArray());
            }
        }

        [HarmonyPatch(typeof(SettingsCache), "LoadFiles", new System.Type[] { typeof(string), typeof(string), typeof(List<YamlIO.Error>) })]
        public static class SettingsCache_LoadFiles_Patch
        {
            public static void Postfix()
            {
                ComposableDictionary<string, Mob> mobLookupTable = SettingsCache.mobs.MobLookupTable;
                foreach (string str in CropsDictionary.Keys)
                {
                    if (!mobLookupTable.ContainsKey(str))
                    {
                        FragrantPlantsTuning.CropsTuning tuning = CropsDictionary[str];
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
                        foreach (string str in FragrantFlowers_Patches_Worldgen.CropsDictionary.Keys)
                        {
                            FragrantPlantsTuning.CropsTuning tuning = FragrantFlowers_Patches_Worldgen.CropsDictionary[str];
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
