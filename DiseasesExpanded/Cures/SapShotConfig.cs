using UnityEngine;
using System.Collections.Generic;
using TUNING;

namespace DiseasesExpanded
{
    class SapShotConfig : IEntityConfig
    {
        public const string ID = "SapShot";
        public const string EFFECT_ID = "SapShotEffect";
        public static ComplexRecipe recipe;

        public string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public GameObject CreatePrefab()
        {
            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[3]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Resin.CreateTag(), 10f),
                new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(), 100f),
                new ComplexRecipe.RecipeElement(SimHashes.Sucrose.CreateTag(), 100f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(ApothecaryConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = 100f,
                description = STRINGS.CURES.SAPSHOT.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { ApothecaryConfig.ID },
                sortOrder = 12
            };

            MedicineInfo medInfo = new MedicineInfo(ID, EFFECT_ID, MedicineInfo.MedicineType.CureSpecific, AdvancedDoctorStationConfig.ID, new string[] { HungerSickness.ID });

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, STRINGS.CURES.SAPSHOT.NAME, STRINGS.CURES.SAPSHOT.NAME, 1f, false, Assets.GetAnim(Kanims.SapShotKanim), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
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
