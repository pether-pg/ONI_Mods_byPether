using HarmonyLib;
using UnityEngine;
using Database;
using System.Collections.Generic;
using Klei.AI;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_NurseryGenetic
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (Settings.Instance.NurseryGenetic.IncludeRoom && DlcManager.IsExpansion1Active())
            {
                __instance.Add(RoomTypes_AllModded.GeneticNursery);

                RoomConstraintTags.AddStompInConflict(__instance.Farm, RoomTypes_AllModded.GeneticNursery);
                RoomConstraintTags.AddStompInConflict(__instance.CreaturePen, RoomTypes_AllModded.GeneticNursery);

                if (Settings.Instance.Botanical.IncludeRoom)
                    RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.GeneticNursery, RoomTypes_AllModded.Botanical);
                if (Settings.Instance.Nursery.IncludeRoom)
                    RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.GeneticNursery, RoomTypes_AllModded.Nursery);
            }
        }

        [HarmonyPatch(typeof(SeedProducer))]
        [HarmonyPatch("RollForMutation")]
        public static class SeedProducer_RollForMutation_Patch
        {
            public static void Postfix(SeedProducer __instance, ref bool __result)
            {
                if (!Settings.Instance.NurseryGenetic.Bonus.HasValue) return;
                if (!RoomTypes_AllModded.IsInTheRoom(__instance, RoomTypeNurseryGeneticData.RoomId)) return;

                AttributeInstance attributeInstance = Db.Get().PlantAttributes.MaxRadiationThreshold.Lookup((Component)__instance);
                int cell = Grid.PosToCell(__instance.gameObject);

                double roll = (double)UnityEngine.Random.value;
                double chance = (double)Mathf.Clamp(Grid.IsValidCell(cell) ? Grid.Radiation[cell] : 0.0f, 0.0f, attributeInstance.GetTotalValue()) / (double)attributeInstance.GetTotalValue() * 0.800000011920929;
                __result = roll < (1 + Settings.Instance.NurseryGenetic.Bonus.Value) * chance;
            }
        }
    }
}
