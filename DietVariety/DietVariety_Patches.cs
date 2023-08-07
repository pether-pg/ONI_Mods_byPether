using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Klei.AI;

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

        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch(nameof(Db.Initialize))]
        public class Db_Initialize_Patch
        {
            public static void Postfix()
            {
                Effect aNewHope = Db.Get().effects.Get("AnewHope");
                if (aNewHope == null || aNewHope.SelfModifiers == null)
                    return;

                string id = Db.Get().Attributes.QualityOfLife.Id;
                float value = Settings.Instance.MinFoodTypesRequired * Settings.Instance.MoralePerFoodType;
                aNewHope.SelfModifiers.Add(new AttributeModifier(id, value, STRINGS.EFFECTS.A_NEW_HOPE_BOOST.NAME));
            }
        }

        [HarmonyPatch(typeof(Game))]
        [HarmonyPatch("DestroyInstances")]
        public class Game_DestroyInstances_Patch
        {
            public static void Prefix()
            {
                PastMealsEaten.Clear();
            }
        }

        [HarmonyPatch(typeof(SaveGame))]
        [HarmonyPatch("OnPrefabInit")]
        public class SaveGame_OnPrefabInit_Patch
        {
            public static void Postfix(SaveGame __instance)
            {
                __instance.gameObject.AddComponent<PastMealsEaten>();
            }
        }

        [HarmonyPatch(typeof(FetchManager))]
        [HarmonyPatch("FindEdibleFetchTarget")]
        public class FetchManager_FindEdibleFetchTarget_Patch
        {
            static FieldInfo pickupPathCostFieldInfo = AccessTools.Field(typeof(FetchManager.Pickup), nameof(FetchManager.Pickup.PathCost));
            static MethodInfo myExtraCodeMethodInfo = AccessTools.Method(typeof(FetchManager_FindEdibleFetchTarget_Patch), nameof(FetchManager_FindEdibleFetchTarget_Patch.AddPathCost));

            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                if (pickupPathCostFieldInfo == null || myExtraCodeMethodInfo == null)
                    Debug.Log("DietVariety: unable to correctly transpile FetchManager.FindEdibleFetchTarget()");

                foreach (var instruction in instructions)
                {
                    yield return instruction;

                    if (pickupPathCostFieldInfo == null || myExtraCodeMethodInfo == null)
                        continue;

                    if (instruction.LoadsField(pickupPathCostFieldInfo))
                    {
                        // Load on the stack 1st argument of FindEdibleFetchTarget method - Storage destination
                        yield return new CodeInstruction(OpCodes.Ldarg_1);

                        // Load on the stack 5th local variable - Pickupable pickupable = pickup2.pickupable;
                        yield return new CodeInstruction(OpCodes.Ldloc_S, 5);

                        yield return new CodeInstruction(OpCodes.Call, myExtraCodeMethodInfo);
                    }
                }
            }

            public static ushort AddPathCost(ushort currentCost, Storage destination, Pickupable pickupable)
            {
                string foodID = pickupable.name;
                int penalty = PastMealsEaten.Instance.GetVarietyCost(destination.gameObject, foodID);
                int totalCost = currentCost + penalty * Settings.Instance.PreferencePenaltyForEatenTypes;
                return (ushort)totalCost;
            }
        }
    }
}
