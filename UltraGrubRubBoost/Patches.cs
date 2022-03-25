using HarmonyLib;
using Klei.AI;
using System;

namespace UltraGrubRubBoost
{
    public class Patches
    {
        [HarmonyPatch(typeof(Klei.AI.Modifier))]
        [HarmonyPatch("Add")]
        [HarmonyPatch(new Type[] { typeof(AttributeModifier) } )]
        public class Modifier_Add_Patch
        {
            public static void Postfix(AttributeModifier modifier)
            {
                // That is just lazy coding, do not release it in that form, never ever.
                if (modifier.AttributeId == Db.Get().Amounts.Maturity.deltaAttribute.Id)
                    modifier.SetValue(1000 * modifier.Value);
            }
        }
    }
}
