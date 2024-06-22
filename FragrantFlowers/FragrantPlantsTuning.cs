using ProcGen;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace FragrantFlowers
{
    class FragrantPlantsTuning
    {
        public static bool DebugMode = false;
        public static string[] SupportedVersions = DlcManager.AVAILABLE_EXPANSION1_ONLY;
        public static CropsTuning SpinrosaTuning;
        public static CropsTuning DuskbloomTuning;
        public static CropsTuning MallowTuning;

        public static SeedTuning DuskLavenderSeedTuning;
        public static SeedTuning SpinosaSeedTuning;
        public static SeedTuning RimedMallowSeedTuning;
        public static MinMax seedDensity = new MinMax(0.015f, 0.03f);

        static FragrantPlantsTuning()
        {
            MakeSpinrosaTuning();
            MakeDuskbloomTuning();
            MakeMallowTuning();
            MakeSeedsTuning();
        }

        public static void MakeSeedsTuning()
        {
            DuskLavenderSeedTuning = new SeedTuning
            {
                density = seedDensity,
                biomes = DuskbloomTuning.biomes,
            };
            SpinosaSeedTuning = new SeedTuning
            {
                density = seedDensity,
                biomes = SpinrosaTuning.biomes,
            };
            RimedMallowSeedTuning = new SeedTuning
            {
                density = seedDensity,
                biomes = MallowTuning.biomes,
            };
        }
        public static CropsTuning MakeSpinrosaTuning()
        {
            float avgDens = Settings.Instance.Rose.AverageDensity;
            CropsTuning tuning = new CropsTuning
            {
                density = new MinMax(avgDens - 0.1f, avgDens + 0.1f)
            };
            HashSet<Temperature.Range> set1 = new HashSet<Temperature.Range>();
            set1.Add(Temperature.Range.Mild);
            set1.Add(Temperature.Range.Room);
            set1.Add(Temperature.Range.HumanWarm);
            set1.Add(Temperature.Range.HumanHot);
            set1.Add(Temperature.Range.Hot);
            tuning.biomeTemperatures = set1;
            HashSet<string> set2 = new HashSet<string>();
            set2.Add(BIOMES_STRINGS.SEDIMENTARY);
            tuning.biomes = set2;
            tuning.spawnLocation = Mob.Location.Floor;
            SpinrosaTuning = tuning;
            return tuning;
        }

        public static CropsTuning MakeDuskbloomTuning()
        {
            float avgDens = Settings.Instance.Lavender.AverageDensity;
            CropsTuning tuning = new CropsTuning
            {
                density = new MinMax(avgDens - 0.1f, avgDens + 0.1f)
            };
            HashSet<Temperature.Range> set3 = new HashSet<Temperature.Range>();
            set3.Add(Temperature.Range.Mild);
            set3.Add(Temperature.Range.Room);
            set3.Add(Temperature.Range.HumanWarm);
            set3.Add(Temperature.Range.HumanHot);
            set3.Add(Temperature.Range.Hot);
            tuning.biomeTemperatures = set3;
            HashSet<string> set4 = new HashSet<string>();
            set4.Add(BIOMES_STRINGS.FOREST);
            tuning.biomes = set4;
            tuning.spawnLocation = Mob.Location.Floor;
            DuskbloomTuning = tuning;
            return tuning;
        }

        public static CropsTuning MakeMallowTuning()
        {
            float avgDens = Settings.Instance.Mallow.AverageDensity;
            CropsTuning tuning = new CropsTuning
            {
                density = new MinMax(avgDens - 0.1f, avgDens + 0.1f)
            };
            HashSet<Temperature.Range> set5 = new HashSet<Temperature.Range>();
            set5.Add(Temperature.Range.Chilly);
            set5.Add(Temperature.Range.Cold);
            set5.Add(Temperature.Range.VeryCold);
            set5.Add(Temperature.Range.ExtremelyCold);
            tuning.biomeTemperatures = set5;
            HashSet<string> set6 = new HashSet<string>();
            set6.Add(BIOMES_STRINGS.FROZEN);
            tuning.biomes = set6;
            tuning.spawnLocation = Mob.Location.Ceiling;
            MallowTuning = tuning;
            return tuning;
        }

        public class BIOMES_STRINGS
        {
            public static string PREFIX = "biomes/";
            public static string BARREN = "Barren";
            public static string DEFAULT = (PREFIX + "Default");
            public static string FOREST = (PREFIX + "Forest");
            public static string FROZEN = (PREFIX + "Frozen");
            public static string MARSH = (PREFIX + "HotMarsh");
            public static string JUNGLE = (PREFIX + "Jungle");
            public static string MAGMA = (PREFIX + "Magma");
            public static string MISC = (PREFIX + "Misc");
            public static string EMPTY = (PREFIX + "Misc/Empty");
            public static string OCEAN = (PREFIX + "Ocean");
            public static string OIL = (PREFIX + "Oil");
            public static string RUST = (PREFIX + "Rust");
            public static string SEDIMENTARY = (PREFIX + "Sedimentary");
            public static string AQUATIC = (PREFIX + "Aquatic");
            public static string METALLIC = (PREFIX + "Metallic");
            public static string MOO = (PREFIX + "Moo");
            public static string NIOBIUM = (PREFIX + "Niobium");
            public static string RADIOACTIVE = (PREFIX + "Radioactive");
            public static string SWAMP = (PREFIX + "Swamp");
            public static string WASTELAND = (PREFIX + "Wasteland");
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CropsTuning
        {
            public MinMax density;
            public ISet<Temperature.Range> biomeTemperatures;
            public ISet<string> biomes;
            public ISet<string> biomesExcluded;
            public Mob.Location spawnLocation;
            public bool ValidBiome(SubWorld subworld, string biome) =>
                ((this.biomeTemperatures.Contains(subworld.temperatureRange) && ((this.biomesExcluded == null) || !this.biomesExcluded.Any<string>(b => biome.Contains(b)))) && this.biomes.Any<string>(b => biome.Contains(b)));
        }

        public struct SeedTuning
        {
            public MinMax density;
            public ISet<string> biomes;
            public ISet<string> biomesExcluded;

            public bool ValidBiome(string biome)
            {
                return (biomesExcluded == null || !biomesExcluded.Any(b => biome.Contains(b))) && biomes.Any(b => biome.Contains(b));
            }
        }
    }
}