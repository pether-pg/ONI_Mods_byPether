using HarmonyLib;
using UnityEngine;
using Klei.AI;
using System;
using System.Linq;
using System.Collections.Generic;
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

        public static Effect CalculateEffectBonus(MinionModifiers modifiers)
        {
            AttributeInstance creativityAttrInstance = modifiers.attributes.AttributeTable.Where(p => p.Name == "Creativity").FirstOrDefault();
            if (creativityAttrInstance == null)
                return null;

            if (!Settings.Instance.Museum.Bonus.HasValue)
                return null;

            float creativity = creativityAttrInstance.GetTotalValue();
            float bonus = Settings.Instance.Museum.Bonus.Value;
            int moraleBonus = Mathf.Clamp((int)Math.Ceiling(creativity * bonus), 1, 10);

            Effect effect = new Effect(RoomTypeMuseumData.EffectId, STRINGS.ROOMS.EFFECTS.MUSEUM.NAME, STRINGS.ROOMS.EFFECTS.MUSEUM.DESCRIPTION, 240, false, true, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier("QualityOfLife", moraleBonus, description: STRINGS.ROOMS.EFFECTS.MUSEUM.NAME));
            return effect;
        }

        [HarmonyPatch(typeof(ItemPedestalConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class ItemPedestalConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(ref GameObject go)
            {
                if (!Settings.Instance.Museum.IncludeRoom) return;

                go.AddOrGet<MuseumEffectTrigger>();
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.ItemPedestalTag);
            }
        }

        [HarmonyPatch(typeof(GravitasPedestalConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class GravitasPedestalConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(ref GameObject go)
            {
                if (!Settings.Instance.Museum.IncludeRoom) return;

                go.AddOrGet<MuseumEffectTrigger>();
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.ItemPedestalTag);
            }
        }
    }
}
