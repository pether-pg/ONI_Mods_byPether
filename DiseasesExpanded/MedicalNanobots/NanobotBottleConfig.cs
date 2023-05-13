using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class NanobotBottleConfig : IEntityConfig
    {
        public const string ID = "NanobotBottle";
        public const int SPAWNED_BOTS_COUNT = 1000 * 1000;
        public const float OXYGEN_MASS = 1.0f;
        public static Tag BOTTLED_GERM_TAG = TagManager.Create("BottledNanobots", STRINGS.TAGS.DISPOSABLE_GERMS.PROPER_NAME);

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public void OnPrefabInit(GameObject inst)
        {
            PrimaryElement prime = inst.AddOrGet<PrimaryElement>();
            prime.ElementID = SimHashes.Oxygen;
            prime.Mass = OXYGEN_MASS;

            if (!Settings.Instance.MedicalNanobots.IncludeDisease)
                return;

            prime.AddDisease(GermIdx.MedicalNanobotsIdx, SPAWNED_BOTS_COUNT, "Spawned");
        }

        public void OnSpawn(GameObject inst)
        {
        }

        public GameObject CreatePrefab()
        {
            DefineRecipe();

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(
                ID,
                STRINGS.NANOBOTDEVELOPMENT.MORENANOBOTSBOTTLED.NAME,
                STRINGS.NANOBOTDEVELOPMENT.MORENANOBOTSBOTTLED.DESC,
                1f,
                true,
                Assets.GetAnim(Kanims.Microchip),
                "object",
                Grid.SceneLayer.Front,
                EntityTemplates.CollisionShape.RECTANGLE,
                0.9f,
                0.6f,
                true,
                additionalTags: new List<Tag>() { BOTTLED_GERM_TAG, GameTags.IndustrialIngredient });

            return looseEntity;
        }

        private static void DefineRecipe()
        {
            if (!Settings.Instance.MedicalNanobots.IncludeDisease)
                return;

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                MedicalNanobotsData.MainIngridient,
                new ComplexRecipe.RecipeElement(SimHashes.Oxygen.CreateTag(), OXYGEN_MASS)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(ID, 1, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            ComplexRecipe recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(MedicalNanobotsData.FABRICATOR_ID, ingredients, results), ingredients, results)
            {
                time = MedicalNanobotsData.RECIPE_TIME,
                description = STRINGS.NANOBOTDEVELOPMENT.MORENANOBOTS.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { MedicalNanobotsData.FABRICATOR_ID },
                sortOrder = 11
            };
        }
    }
}
