using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace RealCaloriesInfo
{
    public class RealCaloriesInfo_Patches_Electrobanks
    {

        [HarmonyPatch(typeof(MeterScreen_Electrobanks))]
        [HarmonyPatch("InternalRefresh")]
        public class MeterScreen_Electrobanks_InternalRefresh_Patch
        {
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                return SupportMethods.SharedTranspilerElectrobank(instructions);
            }
        }

        [HarmonyPatch(typeof(MeterScreen_Electrobanks))]
        [HarmonyPatch("OnTooltip")]
        public class MeterScreen_Electrobanks_OnTooltip_Patch
        {
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                return SupportMethods.SharedTranspilerElectrobank(instructions);
            }
        }

        public class SupportMethods
        {
            static MethodInfo countElectrobankAmountMethodInfo = AccessTools.Method(
                typeof(WorldResourceAmountTracker<ElectrobankTracker>),
                nameof(WorldResourceAmountTracker<ElectrobankTracker>.CountAmount),
                new System.Type[] { typeof(Dictionary<string, float>), typeof(float).MakeByRefType(), typeof(WorldInventory), typeof(bool) });

            static MethodInfo myExtraCodeMethodInfo_Electrobank = AccessTools.Method(typeof(SupportMethods), nameof(SupportMethods.CountElectrobankAmountAlternative));

            public static IEnumerable<CodeInstruction> SharedTranspilerElectrobank(IEnumerable<CodeInstruction> instructions)
            {
                foreach (var instruction in instructions)
                {
                    // if CountAmount would be called, instead call my method that will do all the calculations
                    if (instruction.operand is MethodInfo m && m == countElectrobankAmountMethodInfo)
                    {
                        yield return new CodeInstruction(OpCodes.Call, myExtraCodeMethodInfo_Electrobank);
                    }
                    else
                        yield return instruction;
                }
            }

            // 1st argument is required to satisfy callvirt - it requires instance of an object
            // following arguments are the same as in WorldResourceAmountTracker<ElectrobankTracker>.CountAmount() to use the same stack as the original method
            public static float CountElectrobankAmountAlternative(object justForCallvirt, Dictionary<string, float> unitCountByID, out float totalUnitsFound, WorldInventory inventory, bool excludeUnreachable)
            {
                float num = 0.0f;
                totalUnitsFound = 0.0f;
                ICollection<Pickupable> pickupables = inventory.GetPickupables(GameTags.ChargedPortableBattery);
                if (pickupables != null)
                {
                    foreach (Pickupable pickupable in (IEnumerable<Pickupable>)pickupables)
                    {
                        if (!pickupable.KPrefabID.HasTag(GameTags.StoredPrivate))
                        {
                            Electrobank elektrobank = pickupable.GetComponent<Electrobank>();
                            if (elektrobank == null) continue;

                            num += pickupable.PrimaryElement.Units * GetPermittedCharge(elektrobank, inventory);
                            totalUnitsFound += pickupable.PrimaryElement.Units;

                            if (unitCountByID != null)
                            {
                                if (!unitCountByID.ContainsKey(elektrobank.ID))
                                    unitCountByID[elektrobank.ID] = 0.0f;
                                unitCountByID[elektrobank.ID] += pickupable.PrimaryElement.Units;
                            }
                        }
                    }
                }
                return num;
            }

            public static float GetPermittedCharge(Electrobank electrobank, WorldInventory inventory)
            {
                if (IsPermitedAtLeastOnce(electrobank, GetWorldId(inventory)))
                    return electrobank.Charge;
                return 0;
            }

            public static int GetWorldId(WorldInventory worldInventory)
            {
                WorldContainer component = worldInventory.GetComponent<WorldContainer>();
                return !((UnityEngine.Object)component != (UnityEngine.Object)null) ? -1 : component.id;
            }

            public static bool IsPermitedAtLeastOnce(Electrobank electrobank, int worldId)
            {
                foreach (MinionIdentity mi in Components.MinionIdentities)
                {
                    if (!(mi.GetMyWorldId() == worldId))
                        continue;

                    ConsumableConsumer consumer = mi.gameObject.GetComponent<ConsumableConsumer>();
                    if (consumer == null)
                        continue;

                    if (consumer.IsPermitted(electrobank.ID))
                        return true;
                }
                return false;
            }
        }
    }
}
