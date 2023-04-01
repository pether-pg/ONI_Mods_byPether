using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class NanobotPackConfig : IEntityConfig
    {
        public const string ID = "NanobotPack";
        public const int SPAWNED_BOTS_COUNT = 1000 * 1000;

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
            int cell = Grid.PosToCell(inst.transform.position);
            SimMessages.ModifyDiseaseOnCell(cell, GermIdx.MedicalNanobotsIdx, SPAWNED_BOTS_COUNT);
            SimMessages.ModifyDiseaseOnCell(Grid.CellAbove(cell), GermIdx.MedicalNanobotsIdx, SPAWNED_BOTS_COUNT);
            SimMessages.ModifyDiseaseOnCell(Grid.CellLeft(cell), GermIdx.MedicalNanobotsIdx, SPAWNED_BOTS_COUNT);
            SimMessages.ModifyDiseaseOnCell(Grid.CellRight(cell), GermIdx.MedicalNanobotsIdx, SPAWNED_BOTS_COUNT);
            SimMessages.ModifyDiseaseOnCell(Grid.CellUpLeft(cell), GermIdx.MedicalNanobotsIdx, SPAWNED_BOTS_COUNT);
            SimMessages.ModifyDiseaseOnCell(Grid.CellUpRight(cell), GermIdx.MedicalNanobotsIdx, SPAWNED_BOTS_COUNT);
            PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, STRINGS.GERMS.MEDICALNANOBOTS.NAME, inst.transform);
            Util.KDestroyGameObject(inst);
        }

        public GameObject CreatePrefab()
        {
            DefineRecipe();

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(
                ID,
                STRINGS.NANOBOTDEVELOPMENT.MORENANOBOTS.NAME,
                STRINGS.NANOBOTDEVELOPMENT.MORENANOBOTS.DESC,
                1f,
                true,
                Assets.GetAnim(Kanims.MedicalNanobots),
                "object",
                Grid.SceneLayer.Front,
                EntityTemplates.CollisionShape.RECTANGLE,
                0.8f,
                0.4f,
                true,
                additionalTags: new List<Tag>() { GameTags.IndustrialIngredient });

            return looseEntity;
        }

        private static void DefineRecipe()
        {
            if (!Settings.Instance.MedicalNanobots.IncludeDisease)
                return;

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[1]
            {
                MedicalNanobotsData.MainIngridient
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(ID, 1, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            ComplexRecipe recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(MedicalNanobotsData.FABRICATOR_ID, ingredients, results), ingredients, results)
            {
                time = MedicalNanobotsData.RECIPE_TIME,
                description = STRINGS.NANOBOTDEVELOPMENT.MORENANOBOTS.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { MedicalNanobotsData.FABRICATOR_ID },
                sortOrder = 11
            };
        }
    }
}
