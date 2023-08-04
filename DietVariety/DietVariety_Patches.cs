using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace DietVariety
{
    public class DietVariety_Patches
    {
        [HarmonyPatch(typeof(MinionConfig))]
        [HarmonyPatch("CreatePrefab")]
        public class MinionConfig_CreatePrefab_Patch
        {
            public static void Postfix(GameObject __result)
            {
                __result.AddOrGet<VarietyMonitor>();
            }
        }

        [HarmonyPatch(typeof(FetchManager))]
        [HarmonyPatch("FindEdibleFetchTarget")]
        public class FetchManager_FindEdibleFetchTarget_Patch
        {
            static MethodInfo myExtraCodeMethodInfo = AccessTools.Method(typeof(FetchManager_FindEdibleFetchTarget_Patch), nameof(FetchManager_FindEdibleFetchTarget_Patch.AddPathCost));

            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                foreach (var instruction in instructions)
                {
                    yield return instruction;

                    if (instruction.opcode == OpCodes.Add)
                    {
                        // Load on the stack 2nd argument of CountRations method - Storage destination
                        yield return new CodeInstruction(OpCodes.Ldarg_1);

                        // Load on the stack 5th local variable - Pickupable pickupable = pickup2.pickupable;
                        yield return new CodeInstruction(OpCodes.Ldloc_S, 5);

                        yield return new CodeInstruction(OpCodes.Call, myExtraCodeMethodInfo);
                    }
                }
            }

            public static int AddPathCost(int currentCost, Storage destination, Pickupable pickupable)
            {
                VarietyMonitor vm = destination.gameObject.GetComponent<VarietyMonitor>();
                if(vm == null)
                {
                    Debug.Log($"VarietyMonitor == null for {destination.gameObject.name}");
                    return currentCost;
                }
                string foodID = pickupable.name;
                int penalty = vm.GetVarietyCost(foodID);
                int totalCost = currentCost + penalty * Settings.Instance.PreferencePenaltyForEatenTypes;
                return totalCost;
            }
        }
    }
}
