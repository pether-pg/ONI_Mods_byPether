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

        private void DefineRecipe(Tag mainIngridient, float amount = 1)
        {
            if (!Settings.Instance.MutatingVirus.IncludeDisease)
                return;

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[3]
            {
                new ComplexRecipe.RecipeElement(mainIngridient, amount),
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 100f),
                new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(), 100f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            SapShotConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(ApothecaryConfig.ID, ingredients, results), ingredients, results)
            {
                time = VaccineApothecaryConfig.RecipeTime,
                description = STRINGS.CURES.MUTATINGANTIVIRAL.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { ApothecaryConfig.ID },
                sortOrder = 13
            };
        }

        public GameObject CreatePrefab()
        {
            DefineRecipe(BasicForagePlantConfig.ID, 2);
            DefineRecipe(ForestForagePlantConfig.ID, 0.25f);
            if (DlcManager.IsExpansion1Active())
                DefineRecipe(SwampForagePlantConfig.ID, 2 / 3.0f);

            MedicineInfo medInfo = new MedicineInfo(ID, EffectID, MedicineInfo.MedicineType.CureSpecific, null, new string[] { MutatingSickness.ID });

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID,
                STRINGS.CURES.MUTATINGANTIVIRAL.NAME,
                STRINGS.CURES.MUTATINGANTIVIRAL.DESC,
                1f,
                true,
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
