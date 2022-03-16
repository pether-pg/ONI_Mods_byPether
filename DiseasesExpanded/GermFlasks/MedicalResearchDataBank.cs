using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class MedicalResearchDataBank : IEntityConfig
    {
        public const string ID = nameof(MedicalResearchDataBank);
        public const string MedicalResearchTypeId = "medical";

        public static void GrantResearchPoints(GameObject go, float amount = 1)
        {
            if (!CheckTechRequireMedicalPoints())
                return;
            Research.Instance.AddResearchPoints(MedicalResearchTypeId, amount);
            PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Research, STRINGS.MEDICALRESEARCH.NAME, go.transform);
        }

        private static bool CheckTechRequireMedicalPoints()
        {
            bool useGlobal = Research.Instance.UseGlobalPointInventory;
            TechInstance activeResearch = Research.Instance.GetActiveResearch();
            if (!useGlobal && activeResearch == null)
                return false;
            ResearchPointInventory inventory = useGlobal ? Research.Instance.globalPointInventory : activeResearch.progressInventory;
            if (!inventory.PointsByTypeID.ContainsKey(MedicalResearchTypeId))
                return false;
            if (useGlobal)
                return true;
            if (activeResearch.tech.costsByResearchTypeID.ContainsKey(MedicalResearchTypeId)
                && inventory.PointsByTypeID[MedicalResearchTypeId] < activeResearch.tech.costsByResearchTypeID[MedicalResearchTypeId])
                return true;
            return false;
        }

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
            PrimaryElement element = inst.GetComponent<PrimaryElement>();
            float mass = (element != null && element.Mass > 0) ? element.Mass : 1;
            GrantResearchPoints(inst, mass);
            Util.KDestroyGameObject(inst);
        }

        public GameObject CreatePrefab()
        {
            Dictionary<string, float> FlaskEfficiency = new Dictionary<string, float>()
            {
                { PollenFlask.ID, 1 },
                { FoodGermsFlask.ID, 1 },
                { SlimelungFlask.ID, 2 },
                { ZombieSporesFlask.ID, 4 },
                { BogBugsFlask.ID, 1 },
                { FrostShardsFlask.ID, 2 },
                { HungermsFlask.ID, 4 },
                { GassyGermFlask.ID, 4 }
            };

            foreach(string flask in FlaskEfficiency.Keys)
            {
                ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[1]
                {
                new ComplexRecipe.RecipeElement((Tag)flask, 1f)
                };
                ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
                {
                new ComplexRecipe.RecipeElement((Tag) ID, FlaskEfficiency[flask], ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
                };
                ComplexRecipe recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(ApothecaryConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
                {
                    time = 100f,
                    description = STRINGS.MEDICALRESEARCH.DESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                    fabricators = new List<Tag>() { (Tag)ApothecaryConfig.ID },
                    sortOrder = 15
                };
            }

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(
                ID,
                STRINGS.MEDICALRESEARCH.NAME,
                STRINGS.MEDICALRESEARCH.DESC,
                1f,
                true,
                Assets.GetAnim(Kanims.MedicalResearch),
                "object",
                Grid.SceneLayer.Front,
                EntityTemplates.CollisionShape.RECTANGLE,
                0.8f,
                0.4f,
                true);

            return looseEntity;
        }
    }
}
