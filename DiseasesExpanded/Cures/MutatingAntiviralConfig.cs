using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class MutatingAntiviralConfig : IEntityConfig
    {
        public const string ID = "MutatingAntiviral";
        public const string EffectID = "MutatingAntiviralEffect";
        public static ComplexRecipe recipe;

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public GameObject CreatePrefab()
        {
            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[3]
            {
                new ComplexRecipe.RecipeElement((Tag) BasicForagePlantConfig.ID, 1f),
                new ComplexRecipe.RecipeElement((Tag) SwampLilyFlowerConfig.ID, 1f),
                new ComplexRecipe.RecipeElement((Tag) SpiceNutConfig.ID, 1f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement((Tag) ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            SapShotConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(ApothecaryConfig.ID, ingredients, results), ingredients, results)
            {
                time = VaccineApothecaryConfig.RecipeTime,
                description = STRINGS.CURES.MUTATINGANTIVIRAL.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { (Tag)ApothecaryConfig.ID },
                sortOrder = 13
            };

            MedicineInfo medInfo = new MedicineInfo(ID, EffectID, MedicineInfo.MedicineType.CureSpecific, null, new string[] { MutatingSickness.ID });

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, 
                STRINGS.CURES.MUTATINGANTIVIRAL.NAME, 
                STRINGS.CURES.MUTATINGANTIVIRAL.NAME, 
                1f,
                false, 
                Assets.GetAnim(Kanims.UnstableAntiviralKanim), 
                "object", 
                Grid.SceneLayer.Front, 
                EntityTemplates.CollisionShape.RECTANGLE, 
                0.8f, 
                0.4f, 
                true);

            GameObject medicineEntity = EntityTemplates.ExtendEntityToMedicine(looseEntity, medInfo);
            return medicineEntity;
        }

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }
    }
}
