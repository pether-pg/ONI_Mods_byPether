using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace RealCaloriesInfo
{
    public class RealCaloriesInfo_Patches
    {
        [HarmonyPatch(typeof(MeterScreen_Rations))]
        [HarmonyPatch("InternalRefresh")]
        public class MeterScreen_Rations_InternalRefresh_Patch
        {
            // Throws System.FormatException:
            // Method virtual System.Void MeterScreen_Rations::InternalRefresh() cannot be patched.
            // Reason: Invalid IL code in (wrapper dynamic-method) MeterScreen_Rations:MeterScreen_Rations.InternalRefresh_Patch0 
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                return SupportMethods.SharedTranspiler(instructions);
            }
        }

        [HarmonyPatch(typeof(MeterScreen_Rations))]
        [HarmonyPatch("OnTooltip")]
        public class MeterScreen_Rations_OnTooltip_Patch
        {
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                return SupportMethods.SharedTranspiler(instructions);
            }
        }

        public class SupportMethods
        {
            static MethodInfo countAmountMethodInfo = AccessTools.Method(typeof(WorldResourceAmountTracker<RationTracker>), nameof(WorldResourceAmountTracker<RationTracker>.CountAmount));
            static MethodInfo myExtraCodeMethodInfo = AccessTools.Method(typeof(SupportMethods), nameof(SupportMethods.CountAmountAlternative));

            public static IEnumerable<CodeInstruction> SharedTranspiler(IEnumerable<CodeInstruction> instructions)
            {
                foreach (var instruction in instructions)
                {
                    if (instruction.operand is MethodInfo m && m == countAmountMethodInfo)
                    {
                        yield return new CodeInstruction(OpCodes.Call, myExtraCodeMethodInfo);
                    }
                    else
                        yield return instruction;
                }
            }
            public static float CountAmountAlternative(Dictionary<string, float> unitCountByID, WorldInventory inventory, bool excludeUnreachable)
            {
                float num = 0.0f;
                ICollection<Pickupable> pickupables = inventory.GetPickupables(GameTags.Edible);
                if (pickupables != null)
                {
                    foreach (Pickupable pickupable in (IEnumerable<Pickupable>)pickupables)
                    {
                        if (!pickupable.KPrefabID.HasTag(GameTags.StoredPrivate))
                        {
                            Edible edible = pickupable.GetComponent<Edible>();
                            if (edible == null) continue;

                            num += GetPermittedCalories(edible, inventory);

                            if (unitCountByID != null)
                            {
                                if (!unitCountByID.ContainsKey(edible.FoodID))
                                    unitCountByID[edible.FoodID] = 0.0f;
                                unitCountByID[edible.FoodID] += edible.Units;
                            }
                        }
                    }
                }
                return 69_420_000; // meme debug value
                return num;
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
