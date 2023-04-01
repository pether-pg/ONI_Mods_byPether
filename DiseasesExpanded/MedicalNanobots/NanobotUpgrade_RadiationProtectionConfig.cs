using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class NanobotUpgrade_RadiationProtectionConfig : IEntityConfig
    {
        public const string ID = "NanobotUpgrade_RadiationProtection";

        public string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
            if (MedicalNanobotsData.IsReadyToUse())
                MedicalNanobotsData.Instance.IncreaseDevelopment(MutationVectors.Vectors.Res_RadiationResistance);
            PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, STRINGS.NANOBOTDEVELOPMENT.RADIATION.NAME, inst.transform);
            Util.KDestroyGameObject(inst);
        }

        public GameObject CreatePrefab()
        {
            DefineRecipe();

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(
                ID,
                STRINGS.NANOBOTDEVELOPMENT.RADIATION.NAME,
                STRINGS.NANOBOTDEVELOPMENT.RADIATION.DESC,
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
                MedicalNanobotsData.MainIngridient,
                new ComplexRecipe.RecipeElement(SimHashes.Lead.CreateTag(), MedicalNanobotsData.RECIPE_MASS_LARGE)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(ID, 1, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            ComplexRecipe recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(MedicalNanobotsData.FABRICATOR_ID, ingredients, results), ingredients, results)
            {
                time = MedicalNanobotsData.RECIPE_TIME,
                description = STRINGS.NANOBOTDEVELOPMENT.RADIATION.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { MedicalNanobotsData.FABRICATOR_ID },
                sortOrder = 31
            };
        }
    }
}