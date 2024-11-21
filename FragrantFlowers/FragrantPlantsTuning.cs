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
                biomesExcluded = new HashSet<string>() {
                    EARTH_BIOME_STRINGS.ASTHENOSPHERE,},
            };
            SpinosaSeedTuning = new SeedTuning
            {
                density = seedDensity,
                biomes = SpinrosaTuning.biomes,
                biomesExcluded = new HashSet<string>() {
                    EARTH_BIOME_STRINGS.SURFACE,},
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
                density = new MinMax(avgDens - 0.1f, avgDens + 0.1f),
                biomeTemperatures = new HashSet<Temperature.Range>() {
                    Temperature.Range.Cold,
                    Temperature.Range.Chilly,
                    Temperature.Range.Cool,
                    Temperature.Range.Mild,
                    Temperature.Range.Room,
                    Temperature.Range.HumanWarm,
                    Temperature.Range.HumanHot,
                    Temperature.Range.Hot,
                },
                biomes = new HashSet<string>()
                {
                    BIOMES_STRINGS.SEDIMENTARY,
                    BAATOR_BIOME_STRINGS.AVERNUS,
                    EARTH_BIOME_STRINGS.ASTHENOSPHERE,
                    EARTH_BIOME_STRINGS.SURFACE,
                    ROLLER_SNAKES_STRINGS.TESTDESERT,
                    EMPTY_WORLDS_BIOME_STRINGS.CUSTOMFOREST,
                },
                spawnLocation = Mob.Location.Floor,
            };
            SpinrosaTuning = tuning;
            return tuning;
        }

        public static CropsTuning MakeDuskbloomTuning()
        {
            float avgDens = Settings.Instance.Lavender.AverageDensity;
            CropsTuning tuning = new CropsTuning
            {
                density = new MinMax(avgDens - 0.1f, avgDens + 0.1f),
                biomeTemperatures = new HashSet<Temperature.Range>(){
                    Temperature.Range.Cold,
                    Temperature.Range.Chilly,
                    Temperature.Range.Cool,
                    Temperature.Range.Mild,
                    Temperature.Range.Room,
                    Temperature.Range.HumanWarm,
                    Temperature.Range.HumanHot,
                    Temperature.Range.Hot,
                },
                biomes = new HashSet<string>() {
                    BIOMES_STRINGS.FOREST,
                    BAATOR_BIOME_STRINGS.SHADOWFEL,
                    EARTH_BIOME_STRINGS.ASTHENOSPHERE,
                    EARTH_BIOME_STRINGS.SURFACE,
                    EMPTY_WORLDS_BIOME_STRINGS.CUSTOMFOREST,
                },
                spawnLocation = Mob.Location.Floor,
            };
            DuskbloomTuning = tuning;
            return tuning;
        }

        public static CropsTuning MakeMallowTuning()
        {
            float avgDens = Settings.Instance.Mallow.AverageDensity;
            CropsTuning tuning = new CropsTuning
            {
                density = new MinMax(avgDens - 0.1f, avgDens + 0.1f),
                biomeTemperatures = new HashSet<Temperature.Range>()
                {
                    Temperature.Range.Cool,
                    Temperature.Range.Chilly,
                    Temperature.Range.Cold,
                    Temperature.Range.VeryCold,
                    Temperature.Range.ExtremelyCold,
                },
                biomes = new HashSet<string>() {
                    BIOMES_STRINGS.FROZEN,
                    BAATOR_BIOME_STRINGS.STYGIA,
                    BAATOR_BIOME_STRINGS.CANIA,
                },
                spawnLocation = Mob.Location.Ceiling,
            };
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
        public class ROLLER_SNAKES_STRINGS // RollerSnakes mod
        {
            public static string TESTDESERT = "TestDesert";
        }
        public class EMPTY_WORLDS_BIOME_STRINGS //EmptyWorlds mod
        {
            public static string CUSTOMFOREST = "CustomForest";
        }
        public class EARTH_BIOME_STRINGS //Earth mod
        {
            public static string PREFIX = @"biomes/Earth/";
            public static string
                ASTHENOSPHERE = PREFIX + "Asthenosphere",
                CORE = PREFIX + "Core",
                MANTLE = PREFIX + "Mantle",
                MANTLE2 = PREFIX + "Mantle2",
                OCEAN = PREFIX + "Ocean",
                SKY = PREFIX + "Sky",
                SURFACE = PREFIX + "Surface";
        }
        public class BAATOR_BIOME_STRINGS //Baator mod
        {
            public static string PREFIX = @"biomes/";
            public static string
                SURFACECRAGS = PREFIX + "Baator_SurfaceCrags",
                SHADOWFEL = PREFIX + "Baator_Shadowfel",
                AVERNUS = PREFIX + "Baator_Avernus",
                DIS = PREFIX + "Baator_Dis",
                MINAUROS = PREFIX + "Baator_Minauros",
                PHLEGETHOS = PREFIX + "Baator_Phlegethos",
                STYGIA = PREFIX + "Baator_Stygia",
                MALBOLGE = PREFIX + "Baator_Malbolge",
                MALADOMINI = PREFIX + "Baator_Maladomini",
                CANIA = PREFIX + "Baator_Cania",
                NESSUS = PREFIX + "Baator_Nessus";
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