using Harmony;

namespace ConfigurableDurability
{
    public class ConfigurableDurability_Patches
    {
        public static class Mod_OnLoad
        {
            public static void OnLoad()
            {
            }
        }

        [HarmonyPatch(typeof(Durability))]
        [HarmonyPatch("OnPrefabInit")]
        public class Durability_OnPrefabInit_Patch
        {
            public static void Postfix(ref Durability __instance)
            {
                __instance.durabilityLossPerCycle = Settings.Instance.durabilityLossPerCycle;
            }
        }
    }
}
