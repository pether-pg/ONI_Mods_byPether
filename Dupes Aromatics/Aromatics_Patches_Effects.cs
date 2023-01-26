using HarmonyLib;
using UnityEngine;
using Database;
using Klei.AI;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Dupes_Aromatics
{
    class Aromatics_Patches_Effects
    {

        [HarmonyPatch(typeof(EntityTemplates))]
        [HarmonyPatch("ExtendEntityToWildCreature")]
        public static class EntityTemplates_ExtendEntityToWildCreature_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
                __result.AddOrGet<LavenderSmelling>();
            }
        }

        [HarmonyPatch(typeof(Artable))]
        [HarmonyPatch("OnCompleteWork")]
        public static class Artable_OnCompleteWork_Patch
        {
            static FieldInfo lookingUglyFieldInfo = AccessTools.Field(
                typeof(ArtableStatuses),
                nameof(ArtableStatuses.LookingUgly));

            static FieldInfo lookingOkayFieldInfo = AccessTools.Field(
                typeof(ArtableStatuses),
                nameof(ArtableStatuses.LookingOkay));

            static MethodInfo myExtraCodeMethodInfo = AccessTools.Method(
                typeof(Artable_OnCompleteWork_Patch),
                nameof(Artable_OnCompleteWork_Patch.UpliftArtistSkill));

            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                if (myExtraCodeMethodInfo == null)
                    Debug.Log($"{ModInfo.Namespace}: Artable_OnCompleteWork_Patch encountered null MethodInfo, no changes will take place...");
                if (lookingUglyFieldInfo == null || lookingOkayFieldInfo == null)
                    Debug.Log($"{ModInfo.Namespace}: Artable_OnCompleteWork_Patch encountered null FieldInfo, no changes will take place...");

                foreach (CodeInstruction instruction in instructions)
                {
                    // each original instruction should be left untouched
                    yield return instruction;

                    // in case there is some issue with required infos, custom logic would break. avoid it by continue command
                    if (myExtraCodeMethodInfo == null || lookingUglyFieldInfo == null || lookingOkayFieldInfo == null)
                        continue;

                    // if last instruction put ArtableStatusItem on the stack, my logic kicks in to improve it
                    if(instruction.operand is FieldInfo fi && (fi == lookingOkayFieldInfo || fi == lookingUglyFieldInfo))
                    {
                        // put argument1 (Worker worker) on the stack
                        yield return new CodeInstruction(OpCodes.Ldarg_1);

                        // call UpliftArtistSkill(ArtableStatusItem current, Worker worker)
                        // ArtableStatusItem was already on the stack, Worker was added in the previous line
                        // the method will return ArtableStatusItem, so original code won't notice anything happened
                        yield return new CodeInstruction(OpCodes.Call, myExtraCodeMethodInfo);
                    }

                }
            }

            public static ArtableStatusItem UpliftArtistSkill(ArtableStatusItem current, Worker worker)
            {
                if (worker == null || worker.gameObject == null)
                    return current;

                Effects effects = worker.gameObject.GetComponent<Effects>();
                if (effects == null || !effects.HasEffect(RoseScent.EFFECT_ID))
                    return current;

                Db db = Db.Get();
                if (current == db.ArtableStatuses.LookingUgly)
                    return db.ArtableStatuses.LookingOkay;
                if (current == db.ArtableStatuses.LookingOkay)
                    return db.ArtableStatuses.LookingGreat;

                return current; // should never reach this line
            }
        }
    }
}
