using HarmonyLib;

namespace ExoticCuisine
{
    public class Patches
    {
        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public class Db_Initialize_Patch
        {
            public static void Postfix()
            {
                TUNING.CROPS.CROP_TYPES.Add(new Crop.CropVal(DragonPlantFruitConfig.ID, DragonPlantConfig.CROP_DURATION));
            }
        }
    }
}
