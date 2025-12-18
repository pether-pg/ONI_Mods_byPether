using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class GasCureConfig : IEntityConfig
    {
        public const string ID = "GasCure";
        public const string EffectID = "GasCureEffect";
        public static ComplexRecipe recipe;
        public const int FlowerGermsSpawned = 10000;

        public string[] GetDlcIds() => null;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }

        public GameObject CreatePrefab()
        {
            DefineRecipe();

            MedicineInfo info = new MedicineInfo(ID, EffectID, MedicineInfo.MedicineType.Booster, (string)null);

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, STRINGS.CURES.GASCURE.NAME, STRINGS.CURES.GASCURE.DESC, 1f, true, Assets.GetAnim(Kanims.GasCureKanim), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToMedicine(looseEntity, info);
        }

        private void DefineRecipe()
        {
            if (!Settings.Instance.MooFlu.IncludeDisease)
                return;

            var tags = new List<Tag>() { PrickleFlowerConfig.SEED_ID };

            if (ModInfo.IsFragrantFlowersEnabled)
            {
                tags.Add("SpinosaSeed");
                tags.Add("LavenderSeed");
                tags.Add("IceMallowSeed");
            }

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                new ComplexRecipe.RecipeElement(tags.ToArray(), 1f),
                new ComplexRecipe.RecipeElement(SimHashes.Fertilizer.CreateTag(), 1f),
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
                sortOrder = 12
            };
        }
    }
}
