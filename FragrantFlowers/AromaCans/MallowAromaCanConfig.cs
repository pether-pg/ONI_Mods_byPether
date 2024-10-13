using UnityEngine;
using System.Collections.Generic;

namespace FragrantFlowers
{
    class MallowAromaCanConfig : IEntityConfig
    {
        public const string ID = "MallowAromaCan";
        public static ComplexRecipe recipe;

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

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
                new ComplexRecipe.RecipeElement(AromaticsFabricator.BasicCanIngridientTag, AromaticsFabricator.BasicCanIngridientMass),
                new ComplexRecipe.RecipeElement(Crop_CottonBollConfig.ID, 1f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement((Tag) ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(CraftingTableConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = 100f,
                description = STRINGS.AROMACANS.MALLOW.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { (Tag)CraftingTableConfig.ID },
                sortOrder = 11
            };

            AromaticsFabricator.RegisterAromaticsRecipe(new ComplexRecipe.RecipeElement[1] { new ComplexRecipe.RecipeElement(ID, 1f) }, MallowScent.ID, "MallowScent recipe");

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, STRINGS.AROMACANS.MALLOW.NAME, STRINGS.AROMACANS.MALLOW.DESC, 1f, true, Assets.GetAnim("aromatic_mallowscent_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            looseEntity.AddTag(GameTags.IndustrialProduct);
            return looseEntity;
        }
    }
}