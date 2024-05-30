using UnityEngine;
using System.Collections.Generic;

namespace FragrantFlowers
{
    class SpinosaCakeConfig : IEntityConfig
    {
        public const string ID = "SpinosaCake";
        public static ComplexRecipe recipe;

        public string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }

        public GameObject CreatePrefab()
        {

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[3]
            {
              new ComplexRecipe.RecipeElement(SpinosaSyrupConfig.ID, 1f),
              new ComplexRecipe.RecipeElement(ColdWheatConfig.SEED_ID, 3f),
              new ComplexRecipe.RecipeElement(RawEggConfig.ID, 1f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
            };
            recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(GourmetCookingStationConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = TUNING.FOOD.RECIPES.STANDARD_COOK_TIME,
                description = STRINGS.FOOD.SPINOSACAKE.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { GourmetCookingStationConfig.ID },
                sortOrder = 1
            };

            EdiblesManager.FoodInfo info = new EdiblesManager.FoodInfo(ID, "EXPANSION1_ID", 6400000f, 5, 255.15f, 277.15f, 2400f, true); // see TUNING.FOOD.FOOD_TYPES.BERRY_PIE
            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, STRINGS.FOOD.SPINOSACAKE.NAME, STRINGS.FOOD.SPINOSACAKE.DESC, 1f, true, Assets.GetAnim("food_rosecake_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToFood(looseEntity, info);
        }
    }
}