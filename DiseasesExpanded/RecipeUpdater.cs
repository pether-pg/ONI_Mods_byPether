using System;
using System.Collections.Generic;
using HarmonyLib;

namespace DiseasesExpanded
{
    class RecipeUpdater
    {
        public const int INVALID_IDX = -1;

        public const string FABRICATOR_ID = SupermaterialRefineryConfig.ID;
        public const float RECIPE_TIME = 600;
        public const float RECIPE_TIME_SWARM = 200;
        public const float RECIPE_MASS_BOT_SWARM = 1000;
        public const float RECIPE_MASS_LARGE = 1000;
        public const float RECIPE_MASS_SMALL = 100;

        public static readonly ComplexRecipe.RecipeElement MainIngridient = new ComplexRecipe.RecipeElement(SimHashes.Steel.CreateTag(), RECIPE_MASS_LARGE);
        public static readonly ComplexRecipe.RecipeElement MainIngridient_Swarm = new ComplexRecipe.RecipeElement(SimHashes.Steel.CreateTag(), RECIPE_MASS_BOT_SWARM);

        public static readonly Dictionary<MutationVectors.Vectors, ComplexRecipe.RecipeElement> SecondaryUpgradeIngridiends = new Dictionary<MutationVectors.Vectors, ComplexRecipe.RecipeElement>
        {
            { MutationVectors.Vectors.Att_Attributes,               new ComplexRecipe.RecipeElement(SimHashes.TempConductorSolid.CreateTag(), RECIPE_MASS_SMALL) },
            { MutationVectors.Vectors.Att_Breathing,                new ComplexRecipe.RecipeElement(SimHashes.OxyRock.CreateTag(), RECIPE_MASS_LARGE) },
            { MutationVectors.Vectors.Att_Calories,                 new ComplexRecipe.RecipeElement(SimHashes.RefinedCarbon.CreateTag(), RECIPE_MASS_LARGE) },
            { MutationVectors.Vectors.Att_Health,                   new ComplexRecipe.RecipeElement(SimHashes.TempConductorSolid.CreateTag(), RECIPE_MASS_SMALL) },
            { MutationVectors.Vectors.Att_Stamina,                  new ComplexRecipe.RecipeElement(SimHashes.Petroleum.CreateTag(), RECIPE_MASS_LARGE) },
            { MutationVectors.Vectors.Att_Stress,                   new ComplexRecipe.RecipeElement(SimHashes.SuperCoolant.CreateTag(), RECIPE_MASS_SMALL) },
            { MutationVectors.Vectors.Res_BaseInfectionResistance,  new ComplexRecipe.RecipeElement(SimHashes.BleachStone.CreateTag(), RECIPE_MASS_LARGE) },
            { MutationVectors.Vectors.Res_EffectDuration,           new ComplexRecipe.RecipeElement(SimHashes.Polypropylene.CreateTag(), RECIPE_MASS_LARGE) },
            { MutationVectors.Vectors.Res_ExposureThreshold,        new ComplexRecipe.RecipeElement(SimHashes.Diamond.CreateTag(), RECIPE_MASS_LARGE) },
            { MutationVectors.Vectors.Res_RadiationResistance,      new ComplexRecipe.RecipeElement(SimHashes.Lead.CreateTag(), RECIPE_MASS_LARGE) },
            { MutationVectors.Vectors.Res_Replication,              new ComplexRecipe.RecipeElement(SimHashes.ViscoGel.CreateTag(), RECIPE_MASS_SMALL) },
            { MutationVectors.Vectors.Res_TemperatureResistance,    new ComplexRecipe.RecipeElement(SimHashes.SuperInsulator.CreateTag(), RECIPE_MASS_SMALL) },
        };

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
            {
                if (cf == null || recipe.fabricators == null) continue;

                if (recipe.fabricators.Contains(cf.gameObject.PrefabID()))
                    UpdateRecipesForFabricator(cf, recipe.id);
            }
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

        public static void SetNewRecipeCost(MutationVectors.Vectors vector)
        {
            ComplexRecipe recipe = GetNanobotUpgradeRecipe(vector);
            if (recipe == null)
                return;

            int level = MedicalNanobotsData.Instance.GetDevelopmentLevel(vector);
            int multiplier = level + 1;
            ComplexRecipe.RecipeElement SecondaryIngridient = GetSecondaryIngridient(vector);

            recipe.ingredients[0] = new ComplexRecipe.RecipeElement(MainIngridient.material, MainIngridient.amount * multiplier);
            recipe.ingredients[1] = new ComplexRecipe.RecipeElement(SecondaryIngridient.material, SecondaryIngridient.amount * multiplier);
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

        public static void UpdateRecipesForFabricator(ComplexFabricator fabricator, string recipeId)
        {
            int recipeIdx = GetRecipeIndexInFabricator(recipeId, fabricator);
            if (recipeIdx != INVALID_IDX)
            {
                Traverse openOrderCountsTraverse = Traverse.Create(fabricator).Field("openOrderCounts");
                List<int> openOrderCounts = openOrderCountsTraverse.GetValue<List<int>>();
                if (openOrderCounts.Count > recipeIdx)
                {
                    openOrderCounts.RemoveAt(recipeIdx);
                    openOrderCountsTraverse.SetValue(openOrderCounts);
                }
            }
            Traverse.Create(fabricator).Field("recipe_list").SetValue(null);
            Traverse.Create(fabricator).Field("workingOrderIdx").SetValue(-1);

            fabricator.GetRecipes();
        }

        public static int GetRecipeIndexInFabricator(string recipeId, ComplexFabricator fabricator)
        {
            ComplexRecipe[] recipe_list = Traverse.Create(fabricator).Field("recipe_list").GetValue<ComplexRecipe[]>();
            for (int idx = 0; idx < recipe_list.Length; idx++)
                if (recipe_list[idx].id == recipeId)
                    return idx;
            return INVALID_IDX;
        }

        public static ComplexRecipe.RecipeElement GetSecondaryIngridient(MutationVectors.Vectors vector)
        {
            if (SecondaryUpgradeIngridiends.ContainsKey(vector))
                return SecondaryUpgradeIngridiends[vector];
            Debug.Log($"{ModInfo.Namespace}: Could not get secondary ingridient for {vector}. Using default one...");
            return MainIngridient;
        }
    }
}
