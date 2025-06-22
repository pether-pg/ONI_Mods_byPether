using HarmonyLib;
using UnityEngine;
using Klei.AI;
using System;
using System.Linq;
using System.Collections.Generic;
using Database;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_MuseumHistory
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (!Settings.Instance.MuseumHistory.IncludeRoom)
                return;

            __instance.Add(RoomTypes_AllModded.HistoryMuseum);
        }

        public static Effect CalculateEffectBonus(MinionModifiers modifiers)
        {
            AttributeInstance scienceAttrInstance = modifiers.attributes.AttributeTable.Where(p => p.Id == "Learning").FirstOrDefault();
            if (scienceAttrInstance == null)
                return null;

            float science = scienceAttrInstance.GetTotalValue();
            float bonus = Settings.Instance.MuseumHistory.Bonus;
            int moraleBonus = Mathf.Clamp((int)Math.Ceiling(science * bonus), 1, 10);

            Effect effect = new Effect(RoomTypeMuseumHistoryData.EffectId, STRINGS.ROOMS.EFFECTS.MUSEUMHISTORY.NAME, STRINGS.ROOMS.EFFECTS.MUSEUMHISTORY.DESCRIPTION, 240, false, true, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier("QualityOfLife", moraleBonus, description: STRINGS.ROOMS.EFFECTS.MUSEUMHISTORY.NAME));
            return effect;
        }

        [HarmonyPatch(typeof(FossilSculptureConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class FossilSculptureConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(ref GameObject go)
            {
                if (!Settings.Instance.MuseumHistory.IncludeRoom)
                    return;

                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.FossilBuilding);
            }
        }

        [HarmonyPatch(typeof(CeilingFossilSculptureConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class CeilingFossilSculptureConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(ref GameObject go)
            {
                if (!Settings.Instance.MuseumHistory.IncludeRoom)
                    return;

                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.FossilBuilding);
            }
        }
    }
}
