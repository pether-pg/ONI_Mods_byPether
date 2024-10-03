using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class NanobotUpgrade_StressReliefConfig : IEntityConfig
    {
        public const string ID = "NanobotUpgrade_StressRelief";
        public const MutationVectors.Vectors VECTOR = MutationVectors.Vectors.Att_Stress;

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
            if (MedicalNanobotsData.IsReadyToUse())
                MedicalNanobotsData.Instance.IncreaseDevelopment(VECTOR);
            PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, STRINGS.NANOBOTDEVELOPMENT.STRESS.NAME, inst.transform);
            Util.KDestroyGameObject(inst);
        }

        public GameObject CreatePrefab()
        {
            DefineRecipe();

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(
                ID,
                STRINGS.NANOBOTDEVELOPMENT.STRESS.NAME,
                STRINGS.NANOBOTDEVELOPMENT.STRESS.DESC,
                1f,
                true,
                Assets.GetAnim(Kanims.MedicalNanobotsUpgrade),
                "object",
                Grid.SceneLayer.Front,
                EntityTemplates.CollisionShape.RECTANGLE,
                0.8f,
                0.4f,
                true,
                additionalTags: new List<Tag>() { GameTags.IndustrialIngredient });

            return looseEntity;
        }

        private static void DefineRecipe()
        {
            if (!Settings.Instance.MedicalNanobots.IncludeDisease)
                return;

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                RecipeUpdater.MainIngridient,
                RecipeUpdater.GetSecondaryIngridient(VECTOR)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(ID, 1, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            ComplexRecipe recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(RecipeUpdater.FABRICATOR_ID, ingredients, results), ingredients, results)
            {
                time = RecipeUpdater.RECIPE_TIME,
                description = STRINGS.NANOBOTDEVELOPMENT.STRESS.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { RecipeUpdater.FABRICATOR_ID },
                sortOrder = 21
            };
        }
    }
}
