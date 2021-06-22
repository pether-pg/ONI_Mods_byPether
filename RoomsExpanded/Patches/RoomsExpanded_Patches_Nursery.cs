using HarmonyLib;
using UnityEngine;
using Database;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_Nursery
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (Settings.Instance.Nursery.IncludeRoom)
            {
                __instance.Add(RoomTypes_AllModded.Nursery);

                RoomConstraintTags.AddStompInConflict(__instance.Farm, RoomTypes_AllModded.Nursery);
                RoomConstraintTags.AddStompInConflict(__instance.CreaturePen, RoomTypes_AllModded.Nursery);

                if (Settings.Instance.Botanical.IncludeRoom)
                    RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.Botanical, RoomTypes_AllModded.Nursery);
            }
        }

        [HarmonyPatch(typeof(PlanterBoxConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class PlanterBoxConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Nursery.IncludeRoom) return; 
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.NurseryPlanterBoxTag);
            }
        }
        
        [HarmonyPatch(typeof(SeedProducer))]
        [HarmonyPatch("CropPicked")]
        public static class SeedProducer_CropPicked_Patch
        {
            public static void Postfix(SeedProducer __instance)
            {
                if (!Settings.Instance.Nursery.IncludeRoom) return;
                if (!RoomTypes_AllModded.IsInTheRoom(__instance, RoomTypeNurseryData.RoomId)) return;

                if (__instance.seedInfo.productionType != SeedProducer.ProductionType.Harvest
                    || !Settings.Instance.Nursery.Bonus.HasValue)
                    return;

                double chance = Settings.Instance.Nursery.Bonus.Value;
                if ((double)UnityEngine.Random.Range(0.0f, 1f) <= chance)
                {
                    if(ModInfo.Instance.ExecutedWithDLC())
                        Traverse.Create(__instance).Method("ProduceSeed", new object[] { __instance.seedInfo.seedId, 1, true}).GetValue<GameObject>();
                    else
                        Traverse.Create(__instance).Method("ProduceSeed", new object[] { __instance.seedInfo.seedId, 1 }).GetValue<GameObject>();
                        //__instance.ProduceSeed(__instance.seedInfo.seedId, 1);

                }
            }
        }
    }
}
