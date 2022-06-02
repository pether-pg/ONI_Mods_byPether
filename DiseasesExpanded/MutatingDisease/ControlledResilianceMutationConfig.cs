using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded.MutatingDisease
{
    class ControlledResilianceMutationConfig : IEntityConfig
    {
        public const string ID = "ControlledResilianceMutation";

        public string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
            MutationData.Instance.BulkModifyMutation(MutationVectors.GetResilianceVectors(), -1);
            MutationData.Instance.Mutate();
            Util.KDestroyGameObject(inst);
        }

        public GameObject CreatePrefab()
        {
            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                new ComplexRecipe.RecipeElement((Tag)MutatingGermFlask.ID, 1f),
                new ComplexRecipe.RecipeElement(SimHashes.UraniumOre.CreateTag(), VaccineApothecaryConfig.UraniumOreCost)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement((Tag) ID, 1, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            ComplexRecipe recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(VaccineApothecaryConfig.ID, ingredients, results), ingredients, results)
            {
                time = VaccineApothecaryConfig.MutationRecipeTime,
                description = STRINGS.CONTROLLEDMUTATION.RESILIANCE.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                fabricators = new List<Tag>() { (Tag)VaccineApothecaryConfig.ID },
                sortOrder = 15
            };

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(
                ID,
                STRINGS.CONTROLLEDMUTATION.RESILIANCE.NAME,
                STRINGS.CONTROLLEDMUTATION.RESILIANCE.DESC,
                1f,
                true,
                Assets.GetAnim(Kanims.ControlledMutation),
                "object",
                Grid.SceneLayer.Front,
                EntityTemplates.CollisionShape.RECTANGLE,
                0.8f,
                0.4f,
                true);

            return looseEntity;
        }
    }
}
