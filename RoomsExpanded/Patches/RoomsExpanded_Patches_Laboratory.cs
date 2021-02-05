using Harmony;
using Database;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_Laboratory
    {
        // All 3 research station are using the same Research Center script

        [HarmonyPatch(typeof(ResearchCenter))]
        [HarmonyPatch("ConvertMassToResearchPoints")]
        public static class ResearchCenter_ConvertMassToResearchPoints_Patch
        {
            public static void Prefix(ref float mass_consumed, ref ResearchCenter __instance)
            {
                if (!Settings.Instance.Laboratory.IncludeRoom) return;
                if (__instance == null) return;

                if (RoomTypes_AllModded.IsInTheRoom(__instance, RoomTypeLaboratoryData.RoomId)
                    && Settings.Instance.Laboratory.Bonus.HasValue)
                    mass_consumed *= (1 + Settings.Instance.Laboratory.Bonus.Value);

            }
        }

        [HarmonyPatch(typeof(CosmicResearchCenterConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class CosmicResearchCenterConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Laboratory.IncludeRoom) return;
                go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.ResearchStation);
            }
        }

        [HarmonyPatch(typeof(AdvancedResearchCenterConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class AdvancedResearchCenterConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Laboratory.IncludeRoom) return;
                go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.ResearchStation);
            }
        }

        [HarmonyPatch(typeof(ResearchCenterConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class ResearchCenterConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Laboratory.IncludeRoom) return;
                go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.ResearchStation);
            }
        }
    }
}
