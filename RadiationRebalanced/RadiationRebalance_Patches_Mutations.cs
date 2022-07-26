using HarmonyLib;
using UnityEngine;
using Klei.AI;
using System.Text;

namespace RadiationRebalanced
{
    class RadiationRebalance_Patches_Mutations
    {
        [HarmonyPatch(typeof(SeedProducer))]
        [HarmonyPatch("RollForMutation")]
        public static class SeedProducer_RollForMutation_Patch
        {
            public static void Postfix(SeedProducer __instance, ref bool __result)
            {
                if(Settings.Instance.PlantMutations.MutationChanceMultiplier != 1)
                    __result = RollForMutationOverride(__instance);
            }

            private static bool RollForMutationOverride(SeedProducer producer)
            {
                // Klei's function is simple enough to justify full rewrite instead of using transplier

                AttributeInstance attributeInstance = Db.Get().PlantAttributes.MaxRadiationThreshold.Lookup((Component)producer);
                float maxPlantRadiation = attributeInstance.GetTotalValue();
                int cell = Grid.PosToCell(producer.gameObject);
                float radiation = Grid.IsValidCell(cell) ? Grid.Radiation[cell] : 0.0f;
                float effectiveRadiation = Mathf.Clamp(radiation, 0.0f, maxPlantRadiation);
                double mutationChance = (double)effectiveRadiation / (double)maxPlantRadiation * 0.800000011920929;
                mutationChance *= Settings.Instance.PlantMutations.MutationChanceMultiplier;

                double roll = UnityEngine.Random.value;
                return roll < mutationChance;
            }
        }

        [HarmonyPatch(typeof(PlantMutation))]
        [HarmonyPatch("AttributeModifier")]
        public static class PlantMutation_AttributeModifier_Patch
        {
            private static string RadiationId = string.Empty;

            public static void Prefix(Attribute attribute, ref float amount)
            {
                if (RadiationId == string.Empty)
                    RadiationId = Db.Get().PlantAttributes.MinRadiationThreshold.Id;

                if (attribute.Id == RadiationId)
                    amount = Settings.Instance.PlantMutations.AmbientRadiationRequirement;
            }
        }

        [HarmonyPatch(typeof(PlantMutation))]
        [HarmonyPatch("GetTooltip")]
        public static class PlantMutation_GetTooltip_Patch
        {
            public static void Postfix(ref string __result)
            {
                if (!Settings.Instance.PlantMutations.MutatedPlantsDropSeed)
                    return;

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append((string)STRINGS.DUPLICANTS.TRAITS.TRAIT_DESCRIPTION_LIST_ENTRY);
                stringBuilder.Append((string)STRINGS.UI.GAMEOBJECTEFFECTS.MUTANT_STERILE);
                string toRemove = stringBuilder.ToString();

                __result = __result.Replace(toRemove, "");
            }
        }

        [HarmonyPatch(typeof(SeedProducer))]
        [HarmonyPatch("Configure")]
        public static class SeedProducer_Configure_Patch
        {
            public static void Prefix(ref SeedProducer.ProductionType productionType)
            {
                if (!Settings.Instance.PlantMutations.MutatedPlantsDropSeed)
                    return;

                // Sterile is used only in mutations and only instead of Harvest type
                if (productionType == SeedProducer.ProductionType.Sterile)
                    productionType = SeedProducer.ProductionType.Harvest;
            }
        }
    }
}
