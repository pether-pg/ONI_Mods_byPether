using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class MudMaskConfig : IEntityConfig
    {
        public const string ID = "MudMask";
        public const string EffectID = "MudMaskEffect";
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
                new ComplexRecipe.RecipeElement(SimHashes.Dirt.CreateTag(), 100f),
                new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(), 100f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement((Tag) ID, 10f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            MudMaskConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(ApothecaryConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = 100f,
                description = STRINGS.CURES.MUDMASK.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { (Tag)ApothecaryConfig.ID },
                sortOrder = 12
            };

            MedicineInfo info = new MedicineInfo(ID, EffectID, MedicineInfo.MedicineType.Booster, (string)null);

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, STRINGS.CURES.MUDMASK.NAME, STRINGS.CURES.MUDMASK.DESC, 1f, true, Assets.GetAnim(Kanims.MudMaskKanim), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToMedicine(looseEntity, info);
        }
    }
}
