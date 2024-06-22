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
        public static Dictionary<string, FragrantPlantsTuning.SeedTuning> SeedDictionary;

        public static void InitCropDictionary()
        {
            CropsDictionary = new Dictionary<string, FragrantPlantsTuning.CropsTuning>
            {
                { Plant_SpinosaConfig.ID, FragrantPlantsTuning.SpinrosaTuning },
                { Plant_DuskLavenderConfig.ID, FragrantPlantsTuning.DuskbloomTuning },
                { Plant_RimedMallowConfig.ID, FragrantPlantsTuning.MallowTuning }
            };

            SeedDictionary = new Dictionary<string, FragrantPlantsTuning.SeedTuning>()
            {
                { Plant_RimedMallowConfig.SEED_ID, FragrantPlantsTuning.RimedMallowSeedTuning },
                { Plant_SpinosaConfig.SEED_ID, FragrantPlantsTuning.SpinosaSeedTuning },
                { Plant_DuskLavenderConfig.SEED_ID, FragrantPlantsTuning.DuskLavenderSeedTuning },
            };
        }

        [HarmonyPatch(typeof(Immigration), "ConfigureCarePackages")]
        public static class Immigration_ConfigureCarePackages_Patch
        {
            public static void Postfix(ref Immigration __instance)
            {
                Traverse traverse = Traverse.Create(__instance).Field("carePackages");
                List<CarePackageInfo> list = traverse.GetValue<CarePackageInfo[]>().ToList<CarePackageInfo>();
                list.Add(new CarePackageInfo(Plant_SpinosaConfig.SEED_ID, Settings.Instance.Rose.SeedsInCarePackage, null));
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
                        Mob root = new Mob(tuning.spawnLocation) { name = str };
                        Traverse traverse = Traverse.Create(root);
                        traverse.Property("width", null).SetValue(1);
                        traverse.Property("height", null).SetValue(1);
                        traverse.Property("density", null).SetValue(tuning.density);
                        traverse.Property("selectMethod", null).SetValue(1);
                        mobLookupTable.Add(str, root);
                    }
                }
                if (Settings.Instance.SeedsSpawn)
                    foreach (string seedName in SeedDictionary.Keys)
                    {
                        if (mobLookupTable.ContainsKey(seedName))
                            continue;

                        var tuning = SeedDictionary[seedName];
                        Mob plant = new Mob(Mob.Location.Solid) { name = seedName };

                        var p = Traverse.Create(plant);
                        p.Property("width").SetValue(1);
                        p.Property("height").SetValue(1);
                        p.Property("density").SetValue(tuning.density);
                        p.Property("selectMethod", null).SetValue(1);
                        mobLookupTable.Add(seedName, plant);
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
                    {
                        if (ReferenceEquals(biome.tags, null))
                            Traverse.Create(biome).Property("tags", null).SetValue(new List<string>());

                        foreach (string str in CropsDictionary.Keys)
                            if (CropsDictionary[str].ValidBiome(world, biome.name))
                                if (!biome.tags.Contains(str))
                                    biome.tags.Add(str);

                        if (Settings.Instance.SeedsSpawn)
                            foreach (string seedName in SeedDictionary.Keys)
                                if (SeedDictionary[seedName].ValidBiome(biome.name))
                                    if (!biome.tags.Contains(seedName))
                                        biome.tags.Add(seedName);
                    }
            }
        }
    }
}
