using System;
using System.Collections.Generic;
using HarmonyLib;

namespace DiseasesExpanded
{
    class RecipeUpdater
    {
        public static void MultiplyNanobotUpgradeCost(MutationVectors.Vectors vector, float multiplier)
        {
            ComplexRecipe recipe = GetNanobotUpgradeRecipe(vector);
            MultiplyRecipeCost(recipe, multiplier);
        }

        public static void DeleteNanobotUpgradeRecipe(MutationVectors.Vectors vector)
        {
            ComplexRecipe recipe = GetNanobotUpgradeRecipe(vector);
            if (recipe == null)
                return;
            
            ComplexRecipeManager.Get().recipes.Remove(recipe);

            foreach (ComplexFabricator cf in Components.ComplexFabricators)
                if(cf != null && recipe.fabricators != null && recipe.fabricators.Contains(cf.gameObject.PrefabID()))
                    UpdateRecipesForFabricator(cf);
        }

        public static ComplexRecipe GetNanobotUpgradeRecipe(MutationVectors.Vectors vector)
        {
            Dictionary<MutationVectors.Vectors, string> EntityIDs = new Dictionary<MutationVectors.Vectors, string>()
            {
                { MutationVectors.Vectors.Att_Attributes, NanobotUpgrade_AttributeBoostConfig.ID },
                { MutationVectors.Vectors.Att_Breathing, NanobotUpgrade_BreathingConfig.ID },
                { MutationVectors.Vectors.Att_Calories, NanobotUpgrade_MetabolismBoostConfig.ID },
                { MutationVectors.Vectors.Att_Health, NanobotUpgrade_HealthRegenConfig.ID },
                { MutationVectors.Vectors.Att_Stamina, NanobotUpgrade_AntiExhaustionConfig.ID },
                { MutationVectors.Vectors.Att_Stress, NanobotUpgrade_StressReliefConfig.ID },
                { MutationVectors.Vectors.Res_BaseInfectionResistance, NanobotUpgrade_ResistanceBoostConfig.ID },
                { MutationVectors.Vectors.Res_EffectDuration, NanobotUpgrade_DurationBoostConfig.ID },
                { MutationVectors.Vectors.Res_ExposureThreshold, NanobotUpgrade_ThresholdConfig.ID },
                { MutationVectors.Vectors.Res_RadiationResistance, NanobotUpgrade_RadiationProtectionConfig.ID },
                { MutationVectors.Vectors.Res_Replication, NanobotUpgrade_SpawnIncreaseConfig.ID },
                { MutationVectors.Vectors.Res_TemperatureResistance, NanobotUpgrade_ThermalProtectionConfig.ID },
            };

            for (int i = 0; i < ComplexRecipeManager.Get().recipes.Count; i++)
                if (ComplexRecipeManager.Get().recipes[i].results[0].material == EntityIDs[vector])
                    return ComplexRecipeManager.Get().recipes[i];

            return null;
        }

        public static void MultiplyRecipeCost(ComplexRecipe recipe, float multiplier)
        {
            if (recipe == null)
                return;

            for (int i = 0; i < recipe.ingredients.Length; i++)
                recipe.ingredients[i] = new ComplexRecipe.RecipeElement(
                    recipe.ingredients[i].material,
                    recipe.ingredients[i].amount * multiplier
                    ); ;
        }

        public static void UpdateRecipesForFabricator(ComplexFabricator fabricator)
        {
            Traverse.Create(fabricator).Field("recipe_list").SetValue(null);
            Traverse.Create(fabricator).Field("workingOrderIdx").SetValue(-1);
            fabricator.GetRecipes();
        }
    }
}
