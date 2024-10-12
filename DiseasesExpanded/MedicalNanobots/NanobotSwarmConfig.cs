using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class NanobotSwarmConfig : IEntityConfig
    {
        public const string ID = "NanobotSwarm";
        public const int SPAWNED_BOTS_COUNT = 1000 * 1000;
        public const int SPAWNED_BOTS_COUNT_PER_TILE = SPAWNED_BOTS_COUNT / 6;

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
            Vector3 position = inst.transform.position;
            if (Settings.Instance.MedicalNanobots.IncludeDisease)
                Game.Instance.StartCoroutine(StartSpawningBots(position));
            PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, STRINGS.GERMS.MEDICALNANOBOTS.NAME, inst.transform);
            Util.KDestroyGameObject(inst);
        }

        private IEnumerator StartSpawningBots(Vector3 position)
        {
            int repeats = 5;
            ClearArea(position);
            for (int i=0; i<repeats; i++)
            {
                SpawnGerms(position, SPAWNED_BOTS_COUNT_PER_TILE);
                yield return new WaitForSeconds(0.2f);
            }
        }

        private void ClearArea(Vector3 position)
        {
            foreach (int cell in GetAffectedCells(position))
                if(Grid.DiseaseIdx[cell] != GermIdx.MedicalNanobotsIdx)
                    SimMessages.ConsumeDisease(cell, 1.0f, int.MaxValue, 0);
        }

        private void SpawnGerms(Vector3 position, int count)
        {
            foreach (int cell in GetAffectedCells(position))
                SimMessages.ModifyDiseaseOnCell(cell, GermIdx.MedicalNanobotsIdx, count);
        }

        private List<int> GetAffectedCells(Vector3 position)
        {
            int cell = Grid.PosToCell(position);
            List<int> result = new List<int>() { cell, Grid.CellAbove(cell), Grid.CellLeft(cell), Grid.CellRight(cell), Grid.CellUpLeft(cell), Grid.CellUpRight(cell) };
            return result;
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
                0.9f,
                0.75f,
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
                RecipeUpdater.MainIngridient_Swarm
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(ID, 1, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            ComplexRecipe recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(RecipeUpdater.FABRICATOR_ID, ingredients, results), ingredients, results)
            {
                time = RecipeUpdater.RECIPE_TIME_SWARM,
                description = STRINGS.NANOBOTDEVELOPMENT.MORENANOBOTS.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { RecipeUpdater.FABRICATOR_ID },
                sortOrder = 11
            };
        }
    }
}
