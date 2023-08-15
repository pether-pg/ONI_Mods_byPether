using UnityEngine;
using System.Collections.Generic;

namespace ExampleFoodMod
{
    // TODO: Rename the class to something meaningful. Leave "Config" suffix and " : IEntityConfig" inheritance
    class ModdedFoodItemConfig : IEntityConfig
    {
        // TODO: Modify the ID. Suggested to make it similar to the class name, just without "Config" suffix
        public const string ID = "ModdedFoodItem";

        // TODO: Modify information about your food
        private const string kanimName = "muckrootvegetable_kanim";
        private const float calories = 4000000f;
        private const int caloriesPerUnit = 5;
        private const float preserveTemp = 255.15f;
        private const float rottingTemp = 277.15f;
        private const float spoilTime = 4800f;
        private const bool canRot = true;
        private const string cookingStationId = GourmetCookingStationConfig.ID;
        private const string expansionID = "EXPANSION1_ID";

        public static ComplexRecipe recipe;

        // TODO: Adjust ingridients to your needs
        private void DefineRecipe()
        {
            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Milk.CreateTag(), 1f),
                new ComplexRecipe.RecipeElement(SwampFruitConfig.ID, 1f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
            };
            recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(cookingStationId, ingredients, results), ingredients, results)
            {
                time = TUNING.FOOD.RECIPES.STANDARD_COOK_TIME,
                description = STRINGS.FOOD.SWAMP_MOUSSE.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { cookingStationId },
                sortOrder = 1
            };
        }

        // TODO: Set to desired value. Make sure recipes using DLC items are not allowed outside the DLC
        public string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }

        public GameObject CreatePrefab()
        {
            DefineRecipe();

            EdiblesManager.FoodInfo info = new EdiblesManager.FoodInfo(ID, expansionID, calories, caloriesPerUnit, preserveTemp, rottingTemp, spoilTime, canRot);
            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, STRINGS.FOOD.SWAMP_MOUSSE.NAME, STRINGS.FOOD.SWAMP_MOUSSE.DESC, 1f, true, Assets.GetAnim(kanimName), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToFood(looseEntity, info);
        }
    }
}
