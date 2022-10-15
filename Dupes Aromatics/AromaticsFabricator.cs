using Database;
using Klei.AI;
using UnityEngine;
using System.Collections.Generic;

namespace Dupes_Aromatics
{
    class AromaticsFabricator : ComplexFabricator, ISim200ms
    {
        public const string FabricatorId = AirFilterConfig.ID;
        public const float RecipeTime = 600;

        public static Dictionary<ComplexRecipe, string> RecipesScents = new Dictionary<ComplexRecipe, string>();

        public static void RegisterAromaticsRecipe(ComplexRecipe.RecipeElement[] ingredients, string germId, string Description)
        {
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(SimHashes.CarbonDioxide.CreateTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };

            ComplexRecipe recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(FabricatorId, ingredients, results), ingredients, results)
            {
                time = RecipeTime,
                description = Description,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Ingredient,
                fabricators = new List<Tag>() { (Tag)FabricatorId },
                sortOrder = 1
            };

            if (RecipesScents == null)
                RecipesScents = new Dictionary<ComplexRecipe, string>();
            RecipesScents.Add(recipe, germId);
        }

        public static void SpawnGerms(GameObject go, string germId, float dt, int amountPerSecond = 1000)
        {
            Diseases diseases = Db.Get().Diseases;
            Disease germ = diseases.TryGet(germId);
            if (germ == null)
            {
                UpdateSourceVisibility(go, string.Empty);
                return;
            }

            SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(go.transform.position), diseases.GetIndex(germId), (int)(amountPerSecond * dt));
            UpdateSourceVisibility(go, germId);
        }

        public static void UpdateSourceVisibility(GameObject go, string germId)
        {
            DiseaseSourceVisualizer source = go.AddOrGet<DiseaseSourceVisualizer>();
            source.alwaysShowDisease = germId;
            source.UpdateVisibility();
        }

        public void Sim200ms(float dt)
        {
            base.Sim200ms(dt);

            ComplexRecipe recipe = CurrentWorkingOrder;
            if (recipe == null || !operational.IsOperational)
            {
                UpdateSourceVisibility(gameObject, string.Empty);
                return;
            }

            if (RecipesScents.ContainsKey(recipe))
                SpawnGerms(gameObject, RecipesScents[recipe], dt);
        }
    }
}
