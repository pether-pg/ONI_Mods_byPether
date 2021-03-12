using Harmony;
using UnityEngine;
using Klei.AI;
using System;
using Database;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_Museum
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (!Settings.Instance.Museum.IncludeRoom)
                return;

            __instance.Add(RoomTypes_AllModded.Museum);

            RoomConstraintTags.AddStompInConflict(__instance.Barracks, RoomTypes_AllModded.Museum);
            RoomConstraintTags.AddStompInConflict(__instance.Bedroom, RoomTypes_AllModded.Museum);
            RoomConstraintTags.AddStompInConflict(__instance.MessHall, RoomTypes_AllModded.Museum);
            RoomConstraintTags.AddStompInConflict(__instance.GreatHall, RoomTypes_AllModded.Museum);
            RoomConstraintTags.AddStompInConflict(__instance.Hospital, RoomTypes_AllModded.Museum);
            RoomConstraintTags.AddStompInConflict(__instance.MassageClinic, RoomTypes_AllModded.Museum);
            RoomConstraintTags.AddStompInConflict(__instance.RecRoom, RoomTypes_AllModded.Museum);
            RoomConstraintTags.AddStompInConflict(__instance.Farm, RoomTypes_AllModded.Museum);
            RoomConstraintTags.AddStompInConflict(__instance.CreaturePen, RoomTypes_AllModded.Museum);

            if(Settings.Instance.Aquarium.IncludeRoom)
                RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.Aquarium, RoomTypes_AllModded.Museum);
            if(Settings.Instance.Bathroom.IncludeRoom)
                RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.BathroomRoom, RoomTypes_AllModded.Museum);
            if(Settings.Instance.Gym.IncludeRoom)
                RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.GymRoom, RoomTypes_AllModded.Museum);
            if(Settings.Instance.Kitchen.IncludeRoom)
                RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.KitchenRoom, RoomTypes_AllModded.Museum);
            if(Settings.Instance.Laboratory.IncludeRoom)
                RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.LaboratoryRoom, RoomTypes_AllModded.Museum);
        }

        [HarmonyPatch(typeof(ItemPedestalConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class ItemPedestalConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(ref GameObject go)
            {
                if (!Settings.Instance.Museum.IncludeRoom) return;

                GVD.VersionAlert(expectDLC: false, "Museum Effect tirgger script"); // Effect constructors differ in vanilla and DLC
                go.AddOrGet<MuseumEffectTrigger>();
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.ItemPedestalTag);
            }
        }
    }
}
