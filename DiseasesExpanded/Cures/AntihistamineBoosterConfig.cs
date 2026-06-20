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

        public string[] GetDlcIds() => null;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }

        public GameObject CreatePrefab()
        {
            var tags = new List<Tag>() { "PrickleFlowerSeed", KelpConfig.ID };
            var amounts = new List<float>() { 1f, 10f };

            if (ModInfo.IsFragrantFlowersEnabled)
            {
                tags.Add("Duskbloom");
                amounts.Add(1f);
            }

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                new ComplexRecipe.RecipeElement(tags.ToArray(), amounts.ToArray()),
                new ComplexRecipe.RecipeElement(SimHashes.Dirt.CreateTag(), 1f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement((Tag) ID, 10f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            AntihistamineBoosterConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(ApothecaryConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = 100f,
                description = STRINGS.CURES.ANTIHISTAMINEBOOSTER.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { (Tag)ApothecaryConfig.ID },
                sortOrder = 11
            };

            MedicineInfo info = new MedicineInfo(ID, EffectID, MedicineInfo.MedicineType.Booster, (string)null, new string[] { "Allergies" }, new string[] { "DupeMosquitoBite" });

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, STRINGS.CURES.ANTIHISTAMINEBOOSTER.NAME, STRINGS.CURES.ANTIHISTAMINEBOOSTER.DESC, 1f, true, Assets.GetAnim(Kanims.AntihistamineBoosterKanim), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToMedicine(looseEntity, info);
        }
    }
}
