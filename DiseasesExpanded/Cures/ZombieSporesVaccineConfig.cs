using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiseasesExpanded
{
    class ZombieSporesVaccineConfig : IEntityConfig
    {
        public const string ID = "ZombieSporesVaccine";
        public const string EffectID = "ZombieSporesVaccineEffect";
        public static ComplexRecipe recipe;

        public static string Name { get => string.Format(STRINGS.CURES.VACCINE.NAME, GermIdx.GetGermName(GermIdx.ZombieSporesIdx)); }
        public static string Desc { get => string.Format(STRINGS.CURES.VACCINE.DESC, GermIdx.GetGermName(GermIdx.ZombieSporesIdx)); }

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
                new ComplexRecipe.RecipeElement((Tag)ZombieSporesFlask.ID, 1f),
                new ComplexRecipe.RecipeElement(SimHashes.UraniumOre.CreateTag(), 200f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement((Tag) ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            ZombieSporesVaccineConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(VaccineApothecaryConfig.ID, ingredients, results), ingredients, results)
            {
                time = VaccineApothecaryConfig.RecipeTime,
                description = Desc,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { (Tag)VaccineApothecaryConfig.ID },
                sortOrder = 1
            };

            MedicineInfo info = new MedicineInfo(ID, EffectID, MedicineInfo.MedicineType.Booster, null, null);

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, Name, Desc, 1f, true, Assets.GetAnim(Kanims.SapShotKanim), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToMedicine(looseEntity, info);
        }
    }
}
