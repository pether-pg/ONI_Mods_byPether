using HarmonyLib;
using TUNING;

namespace RadiationRebalanced
{
    class RadiationRebalance_Patches_Eater
    {

        [HarmonyPatch(typeof(RadiationEater.States))]
        [HarmonyPatch("InitializeStates")]
        public static class RadiationEaterStates_InitializeStates_Patch
        {
            public static void Postfix(RadiationEater.States __instance)
            {
                if (!Settings.Instance.RadiationEater.ApplyContinousEffect)
                    return;

                if (Settings.Instance.RadiationEater.ConsumedRadsPerCycle <= 0)
                    return;

                __instance.root.Enter(smi => smi.gameObject.AddOrGet<ContinousRadiationConsumption>());
            }
        }

        [HarmonyPatch(typeof(MinionStartingStats))]
        [HarmonyPatch("GenerateTraits")]
        public static class MinionStartingStats_GenerateTraits_Patch
        {
            private static bool TraitAdded = false;

            public static void Prefix()
            {
                if (TraitAdded)
                    return;

                for(int i=0; i<DUPLICANTSTATS.GOODTRAITS.Count; i++)
                    if(DUPLICANTSTATS.GOODTRAITS[i].id == "RadiationEater")
                    {
                        DUPLICANTSTATS.TraitVal eater = DUPLICANTSTATS.GOODTRAITS[i];
                        eater.rarity = Settings.Instance.RadiationEater.TraitRarity;
                        DUPLICANTSTATS.GOODTRAITS.RemoveAt(i);
                        DUPLICANTSTATS.GOODTRAITS.Add(eater);
                        TraitAdded = true;
                        break;
                    }

                /*
                for (int i = 0; i < DUPLICANTSTATS.GOODTRAITS.Count; i++)
                    if (DUPLICANTSTATS.GOODTRAITS[i].id == "RadiationEater")
                        Debug.Log($"RadiationEater rarity = {DUPLICANTSTATS.GOODTRAITS[i].rarity}");
                */
            }
        }
    }
}
