using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class RustSickness2CureConfig : IEntityConfig
    {
        public const string ID = "RustSickness2Cure";
        public const string EFFECT_ID = "RustSickness2CureEffect";
        public static ComplexRecipe recipe;

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public GameObject CreatePrefab()
        {
            DefineRecipe();

            MedicineInfo medInfo = new MedicineInfo(ID, EFFECT_ID, MedicineInfo.MedicineType.CureSpecific, DoctorStationConfig.ID, new string[] { RustSickness_2.ID });

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, STRINGS.CURES.RUST_2_CURE.NAME, STRINGS.CURES.RUST_2_CURE.DESC, 1f, true, Assets.GetAnim(Kanims.Rust2Cure), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            GameObject medicineEntity = EntityTemplates.ExtendEntityToMedicine(looseEntity, medInfo);
            return medicineEntity;
        }

        private void DefineRecipe()
        {
            if (!Settings.Instance.RustDust.IncludeDisease)
                return;

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                new ComplexRecipe.RecipeElement(new Tag[2]{ SimHashes.CrudeOil.CreateTag(), SimHashes.PhytoOil.CreateTag() }, new float[2]{ 1, 1 }),
                new ComplexRecipe.RecipeElement(PowerStationToolsConfig.tag, 1f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(ApothecaryConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = 200f,
                description = ID,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { ApothecaryConfig.ID },
                sortOrder = 20,
                requiredTech = "MedicineII"
            };
        }

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }
    }
}
