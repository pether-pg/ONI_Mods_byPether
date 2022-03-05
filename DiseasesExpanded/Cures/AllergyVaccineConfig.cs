using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiseasesExpanded
{
    class AllergyVaccineConfig : IEntityConfig
    {
        public const string ID = "AllergyVaccine";
        public const string EffectID = "AllergyVaccineEffect";
        public static ComplexRecipe recipe;

        public static string Name { get => string.Format(STRINGS.CURES.VACCINE.NAME, GermIdx.GetGermName(GermIdx.PollenGermsIdx)); }
        public static string Desc { get => string.Format(STRINGS.CURES.VACCINE.DESC, GermIdx.GetGermName(GermIdx.PollenGermsIdx)); }

        public string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public void OnPrefabInit(GameObject inst)
        {
            KAnimControllerBase kAnimBase = inst.GetComponent<KAnimControllerBase>();
            if (kAnimBase != null)
                kAnimBase.TintColour = ColorPalette.FlowerPink;
        }

        public void OnSpawn(GameObject inst)
        {
        }

        public GameObject CreatePrefab()
        {
            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                new ComplexRecipe.RecipeElement((Tag)PollenFlask.ID, 1f),
                new ComplexRecipe.RecipeElement(SimHashes.UraniumOre.CreateTag(), VaccineApothecaryConfig.UraniumOreCost)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement((Tag) ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            AllergyVaccineConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(VaccineApothecaryConfig.ID, ingredients, results), ingredients, results)
            {
                time = VaccineApothecaryConfig.RecipeTime,
                description = Desc,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { (Tag)VaccineApothecaryConfig.ID },
                sortOrder = 1
            };

            MedicineInfo info = new MedicineInfo(ID, EffectID, MedicineInfo.MedicineType.Booster, null, null);

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, Name, Desc, 1f, true, Assets.GetAnim(Kanims.VaccineCKanim), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToMedicine(looseEntity, info);
        }
    }
}
