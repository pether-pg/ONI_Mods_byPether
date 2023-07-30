using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using Klei.AI;
using TUNING;
using System;
using System.Reflection;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Traits
    {
        [HarmonyPatch(typeof(ModifierSet))]
        [HarmonyPatch("LoadTraits")]
        public static class ModifierSet_LoadTraits_Patch
        {
            public static void Prefix()
            {
                TUNING.TRAITS.TRAIT_CREATORS.Add(TraitUtil.CreateNamedTrait(InsectAllergies.ID, (string)STRINGS.TRAITS.INSECTALLERGIES.NAME, (string)STRINGS.TRAITS.INSECTALLERGIES.NAME));
                TUNING.TRAITS.TRAIT_CREATORS.Add(TraitUtil.CreateNamedTrait(NotWashingHands.ID, (string)STRINGS.TRAITS.NOTWASHINGHANDS.NAME, (string)STRINGS.TRAITS.NOTWASHINGHANDS.NAME));
            }
        }

        [HarmonyPatch(typeof(MinionStartingStats))]
        [HarmonyPatch("GenerateTraits")]
        public static class MinionStartingStats_GenerateTraits_Patch
        {
            private static bool TraitsAdded = false;

            public static void Prefix()
            {
                if (!TraitsAdded)
                {
                    DUPLICANTSTATS.BADTRAITS.Add(InsectAllergies.GetTrait());
                    DUPLICANTSTATS.BADTRAITS.Add(NotWashingHands.GetTrait());
                    TraitsAdded = true;
                }
            }
        }

        [HarmonyPatch]
        public static class WashHandsReactable_InternalCanBegin_Patch
        {
            public static MethodBase TargetMethod()
            {
                Type type = AccessTools.TypeByName("HandSanitizer+WashHandsReactable");
                return AccessTools.Method(type, "InternalCanBegin");
            }

            public static void Postfix(GameObject new_reactor, ref bool __result)
            {
                if (new_reactor != null && NotWashingHands.HasAffectingTrait(new_reactor))
                    __result = false;
            }
        }
    }
}
