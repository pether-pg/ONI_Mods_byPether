using HarmonyLib;
using UnityEngine;
using Database;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_Aquarium
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (!Settings.Instance.Aquarium.IncludeRoom)
                return;

            __instance.Add(RoomTypes_AllModded.Aquarium);

            RoomConstraintTags.AddStompInConflict(__instance.Farm, RoomTypes_AllModded.Aquarium);
            RoomConstraintTags.AddStompInConflict(__instance.CreaturePen, RoomTypes_AllModded.Aquarium);
        }

        [HarmonyPatch(typeof(FishFeederConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class FishFeederConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Aquarium.IncludeRoom) return;
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.AquariumFeederTag);
            }
        }

        [HarmonyPatch(typeof(FishDeliveryPointConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class FishDeliveryPointConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Aquarium.IncludeRoom) return;
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.AquariumReleaseTag);
            }
        }

        [HarmonyPatch(typeof(PacuConfig))]
        [HarmonyPatch("CreatePacu")]
        public static class PacuConfig_CreatePacu_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
                if (!Settings.Instance.Aquarium.IncludeRoom) return;
                DecorProviderModifier decor = __result.AddOrGet<DecorProviderModifier>();
                decor.RequiredRoomId = RoomTypeAquariumData.RoomId;
                if(Settings.Instance.Aquarium.Bonus.HasValue)
                    decor.BonusScale = Settings.Instance.Aquarium.Bonus.Value;
            }
        }

        [HarmonyPatch(typeof(PacuTropicalConfig))]
        [HarmonyPatch("CreatePacu")]
        public static class PacuTropicalConfig_CreatePacu_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
                if (!Settings.Instance.Aquarium.IncludeRoom) return;
                DecorProviderModifier decor = __result.AddOrGet<DecorProviderModifier>();
                if (Settings.Instance.Aquarium.Bonus.HasValue)
                    decor.BonusScale = Settings.Instance.Aquarium.Bonus.Value;
            }
        }

        [HarmonyPatch(typeof(PacuCleanerConfig))]
        [HarmonyPatch("CreatePacu")]
        public static class PacuCleanerConfig_CreatePacu_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
                if (!Settings.Instance.Aquarium.IncludeRoom) return;
                DecorProviderModifier decor = __result.AddOrGet<DecorProviderModifier>();
                if (Settings.Instance.Aquarium.Bonus.HasValue)
                    decor.BonusScale = Settings.Instance.Aquarium.Bonus.Value;
            }
        }
    }
}
