using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiseasesExpanded
{
    class AntihistamineBoosterConfig : IEntityConfig
    {
        public const string ID = "AntihistamineBooster";
        public const string EffectID = "HistamineSuppression"; // from vanilla ONI
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
                new ComplexRecipe.RecipeElement((Tag) "PrickleFlowerSeed", 1f),
                new ComplexRecipe.RecipeElement(SimHashes.Dirt.CreateTag(), 1f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement((Tag) ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            GasCureConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(ApothecaryConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = 100f,
                description = STRINGS.CURES.GASCURE.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { (Tag)ApothecaryConfig.ID },
                sortOrder = 11
            };

            MedicineInfo info = new MedicineInfo(ID, EffectID, MedicineInfo.MedicineType.Booster, (string)null, new string[] { "Allergies" });

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, STRINGS.CURES.ANTIHISTAMINEBOOSTER.NAME, STRINGS.CURES.ANTIHISTAMINEBOOSTER.DESC, 1f, true, Assets.GetAnim(Kanims.AntihistamineBoosterKanim), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToMedicine(looseEntity, info);
        }
    }
}
