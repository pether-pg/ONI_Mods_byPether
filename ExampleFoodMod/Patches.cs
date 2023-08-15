using HarmonyLib;

namespace ExampleFoodMod
{
    public class Patches
    {
        [HarmonyPatch(typeof(EntityConfigManager), "LoadGeneratedEntities")]
        public static class EntityConfigManager_LoadGeneratedEntities_Patch
        {
            // TODO: See STRINGS class and modify string values
            // TODO: If you have more food items in your mod, make sure to call MakeFoodStrings for all of them
            public static void Prefix()
            {
                MakeFoodStrings(ModdedFoodItemConfig.ID, STRINGS.FOOD.SWAMP_MOUSSE.NAME, STRINGS.FOOD.SWAMP_MOUSSE.DESC);
            }

            public static void MakeFoodStrings(string id, string name, string desc)
            {
                Strings.Add($"STRINGS.ITEMS.FOOD.{id.ToUpperInvariant()}.NAME", name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{id.ToUpperInvariant()}.DESC", desc);
            }
        }
    }
}
