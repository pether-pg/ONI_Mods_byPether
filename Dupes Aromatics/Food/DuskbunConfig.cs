using UnityEngine;
using System.Collections.Generic;

namespace Dupes_Aromatics
{
    class DuskbunConfig : IEntityConfig
    {
        public const string ID = "Duskbun";
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

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                new ComplexRecipe.RecipeElement(ColdWheatBreadConfig.ID, 1f),
                new ComplexRecipe.RecipeElement(DuskjamConfig.ID, 1f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
            };
            recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(GourmetCookingStationConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = TUNING.FOOD.RECIPES.STANDARD_COOK_TIME,
                description = STRINGS.FOOD.DUSKBUN.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { GourmetCookingStationConfig.ID },
                sortOrder = 1
            };

            EdiblesManager.FoodInfo info = new EdiblesManager.FoodInfo(ID, "EXPANSION1_ID", 4000000f, 5, 255.15f, 277.15f, 4800f, true); // see TUNING.FOOD.FOOD_TYPES.SPICEBREAD
            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, STRINGS.FOOD.DUSKBUN.NAME, STRINGS.FOOD.DUSKBUN.DESC, 1f, true, Assets.GetAnim("food_duskbun_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToFood(looseEntity, info);
        }
    }
}