using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiseasesExpanded
{
    class HungermsVaccineConfig : IEntityConfig
    {
        public const string ID = "HungermsVaccine";
        public const string EffectID = "HungermsVaccineEffect";
        //public static ComplexRecipe recipe;

        public static string Name { get => string.Format(STRINGS.CURES.VACCINE.NAME, GermIdx.GetGermName(GermIdx.HungerGermsIdx)); }
        public static string Desc { get => string.Format(STRINGS.CURES.VACCINE.DESC, GermIdx.GetGermName(GermIdx.HungerGermsIdx)); }

        public string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public void OnPrefabInit(GameObject inst)
        {
            KAnimControllerBase kAnimBase = inst.GetComponent<KAnimControllerBase>();
            if (kAnimBase != null)
                kAnimBase.TintColour = ColorPalette.HungryBrown;
        }

        public void OnSpawn(GameObject inst)
        {
        }

        public GameObject CreatePrefab()
        {
            /*
            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                new ComplexRecipe.RecipeElement((Tag)HungermsFlask.ID, 1f),
                VaccineApothecaryConfig.GetMainIngridient()
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement((Tag) ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            HungermsVaccine.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(VaccineApothecaryConfig.GetAdvancedApothecaryId(), ingredients, results), ingredients, results)
            {
                time = VaccineApothecaryConfig.RecipeTime,
                description = Desc,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { (Tag)VaccineApothecaryConfig.GetAdvancedApothecaryId() },
                sortOrder = 1
            };
            */

            MedicineInfo info = new MedicineInfo(ID, EffectID, MedicineInfo.MedicineType.Booster, null, null);

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, Name, Desc, 1f, true, Assets.GetAnim(Kanims.VaccineCKanim), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToMedicine(looseEntity, info);
        }
    }
}