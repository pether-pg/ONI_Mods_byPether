using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace RealCaloriesInfo
{
    class RealCaloriesInfo_Patches_Obsolete
    {
        // This no longer works, leaving for reference
        // [HarmonyPatch(typeof(RationTracker))]
        // [HarmonyPatch("CountRations")]
        public class RationTracker_CountRations_Patch
        {
            public static int WorldId = 0;
            static MethodInfo getCaloriesMethodInfo = AccessTools.Property(typeof(Edible), nameof(Edible.Calories)).GetMethod;
            static MethodInfo myExtraCodeMethodInfo = AccessTools.Method(typeof(RationTracker_CountRations_Patch), nameof(RationTracker_CountRations_Patch.GetPermittedCalories));

            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                foreach (var instruction in instructions)
                {
                    if (instruction.operand is MethodInfo m && m == getCaloriesMethodInfo)
                    {
                        // Load on the stack 2nd argument of CountRations method - WorldInventory inventory
                        yield return new CodeInstruction(OpCodes.Ldarg_2);

                        // Call GetPermittedCalories(Edible edible, WorldInventory inventory)
                        // Edible object was already on the stack to call Edible.GetCalories - instead my method is called
                        yield return new CodeInstruction(OpCodes.Call, myExtraCodeMethodInfo);
                    }
                    else
                        yield return instruction;
                }
            }

            public static float GetPermittedCalories(Edible edible, WorldInventory inventory)
            {
                if (IsAlwaysPermitted(edible, GetWorldId(inventory)))
                    return edible.Calories;
                return 0;
            }

            public static int GetWorldId(WorldInventory worldInventory)
            {
                WorldContainer component = worldInventory.GetComponent<WorldContainer>();
                return !((UnityEngine.Object)component != (UnityEngine.Object)null) ? -1 : component.id;
            }

            public static bool IsAlwaysPermitted(Edible edible, int worldId)
            {
                foreach (MinionIdentity mi in Components.MinionIdentities)
                {
                    if (!(mi.GetMyWorldId() == worldId))
                        continue;
                    ConsumableConsumer consumer = mi.gameObject.GetComponent<ConsumableConsumer>();
                    if (consumer == null)
                    {
                        Debug.Log($"{ModInfo.Namespace}: Could not get ConsumableConsumer for minion {mi.name}");
                        continue;
                    }

                    if (!consumer.IsPermitted(edible.FoodID))
                        return false;
                }
                return true;
            }
        }
    }
}
