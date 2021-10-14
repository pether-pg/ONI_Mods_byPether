using HarmonyLib;
using UnityEngine;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Beetas
    {
        [HarmonyPatch(typeof(Weapon))]
        [HarmonyPatch("AttackTarget")]
        public static class Weapon_AttackTarget_Patch
        {
            static bool modified = false;

            public static void Prefix(Weapon __instance, GameObject target)
            {
                if(__instance.gameObject.name.Contains(BeeConfig.ID) && InsectAllergies.HasAffectingTrait(target))
                {
                    __instance.properties.base_damage_max *= InsectAllergies.BeetaStingDamageModifier;
                    __instance.properties.base_damage_min *= InsectAllergies.BeetaStingDamageModifier;
                    modified = true;
                }
            }

            public static void Postfix(Weapon __instance)
            {
                if(modified)
                {
                    __instance.properties.base_damage_max /= InsectAllergies.BeetaStingDamageModifier;
                    __instance.properties.base_damage_min /= InsectAllergies.BeetaStingDamageModifier;
                    modified = false;
                }
            }
        }
    }
}
