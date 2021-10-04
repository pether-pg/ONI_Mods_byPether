using UnityEngine;
using System.Collections.Generic;
using TUNING;

namespace DiseasesExpanded
{
    class MegaFeastConfig : IEntityConfig
    {
        public const string ID = "MegaFeast";
        public const string EffectID = "MegaFeastEffect";
        public static ComplexRecipe recipe;

        public string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public GameObject CreatePrefab()
        {
            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[4]
            {
                new ComplexRecipe.RecipeElement((Tag)MushroomWrapConfig.ID, 1f),
                new ComplexRecipe.RecipeElement((Tag)SurfAndTurfConfig.ID, 1f),
                new ComplexRecipe.RecipeElement((Tag)BurgerConfig.ID, 1f),
                new ComplexRecipe.RecipeElement((Tag)BerryPieConfig.ID, 1f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement((Tag) ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            MegaFeastConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(GourmetCookingStationConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = 100f,
                description = STRINGS.CURES.MEGAFEAST.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { (Tag)GourmetCookingStationConfig.ID },
                sortOrder = 12
            };

            float totalCalories = 0f;
            totalCalories += FOOD.FOOD_TYPES.MUSHROOM_WRAP.CaloriesPerUnit;
            totalCalories += FOOD.FOOD_TYPES.SURF_AND_TURF.CaloriesPerUnit;
            totalCalories += FOOD.FOOD_TYPES.BURGER.CaloriesPerUnit;
            totalCalories += FOOD.FOOD_TYPES.BERRY_PIE.CaloriesPerUnit;

            EdiblesManager.FoodInfo foodInfo = new EdiblesManager.FoodInfo(ID, DlcManager.EXPANSION1_ID, totalCalories, 6, 255.15f, 277.15f, 600f, true);
            foodInfo.AddEffects(new List<string>() { "GoodEats" }, DlcManager.AVAILABLE_ALL_VERSIONS);
            MedicineInfo medInfo = new MedicineInfo(ID, EffectID, MedicineInfo.MedicineType.CureSpecific, (string)null, new string[] { HungerSickness.ID });

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, STRINGS.CURES.MEGAFEAST.NAME, STRINGS.CURES.MEGAFEAST.NAME, 1f, false, Assets.GetAnim(Kanims.MegaFeastKanim), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            GameObject foodEntity = EntityTemplates.ExtendEntityToFood(looseEntity, foodInfo);
            GameObject medfoodEntity = EntityTemplates.ExtendEntityToMedicine(foodEntity, medInfo);
            return medfoodEntity;
        }

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }
    }
}
