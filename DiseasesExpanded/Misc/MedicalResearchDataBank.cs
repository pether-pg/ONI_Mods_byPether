using UnityEngine;
using Database;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class MedicalResearchDataBank : IEntityConfig
    {
        public const string ID = nameof(MedicalResearchDataBank);
        public const string MedicalResearchTypeId = "medical";

        public static void GrantResearchPoints(GameObject go, float amount = 1)
        {
            if (!Settings.Instance.EnableMedicalResearchPoints)
                return;

            TechInstance techToBoost = FindTechToBoost();
            if (techToBoost == null)
                return;
            techToBoost.progressInventory.AddResearchPoints(MedicalResearchTypeId, amount);
            PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Research, STRINGS.MEDICALRESEARCH.NAME, go.transform);
        }

        private static TechInstance FindTechToBoost()
        {
            Techs techs = Db.Get().Techs;
            for (int i = 0; i < techs.Count; i++)
            {
                Tech tech = techs[i];
                if (!tech.costsByResearchTypeID.ContainsKey(MedicalResearchTypeId))
                    continue;
                if (tech.IsComplete())
                    continue;

                float techCost = tech.costsByResearchTypeID[MedicalResearchTypeId];
                TechInstance techInstance = Research.Instance.GetTechInstance(tech.Id);

                if (techInstance.progressInventory.PointsByTypeID[MedicalResearchTypeId] > 0
                    && techInstance.progressInventory.PointsByTypeID[MedicalResearchTypeId] < techCost)
                    return techInstance;

                if (techInstance.progressInventory.PointsByTypeID[MedicalResearchTypeId] == 0)
                {
                    if (tech.ArePrerequisitesComplete())
                        return techInstance;

                    bool firstMedTech = true;
                    bool previousMedTechCompleted = true;

                    foreach (Tech previousTech in tech.requiredTech)
                    {
                        if (previousTech.costsByResearchTypeID.ContainsKey(MedicalResearchTypeId))
                        {
                            firstMedTech = false;
                            float prevTechCost = previousTech.costsByResearchTypeID[MedicalResearchTypeId];
                            TechInstance prevTechInstance = Research.Instance.GetTechInstance(previousTech.Id);
                            if (prevTechInstance.progressInventory.PointsByTypeID[MedicalResearchTypeId] < prevTechCost)
                                previousMedTechCompleted = false;
                        }
                    }

                    if (firstMedTech || previousMedTechCompleted)
                        return techInstance;
                }
            }
            return null;
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

        private void DefineRecipes()
        {
            Dictionary<string, float> FlaskEfficiency = new Dictionary<string, float>()
            {
                { UnspecifiedFlask.ID, 1 },
                { RadiationGermsFlask.ID, 1 },
                { PollenFlask.ID, 1 },
                { FoodGermsFlask.ID, 1 },
                { SlimelungFlask.ID, 2 },
                { ZombieSporesFlask.ID, 4 },
                { BogBugsFlask.ID, 1 },
                { FrostShardsFlask.ID, 2 },
                { HungermsFlask.ID, 4 },
                { GassyGermFlask.ID, 4 },
                { AlienGermFlask.ID, 8 },
                { MutatingGermFlask.ID, 2 }
            };

            foreach (string flask in FlaskEfficiency.Keys)
            {
                ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[1]
                {
                new ComplexRecipe.RecipeElement((Tag)flask, 1f)
                };
                ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
                {
                new ComplexRecipe.RecipeElement((Tag) ID, FlaskEfficiency[flask], ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
                };
                ComplexRecipe recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(ApothecaryConfig.ID, ingredients, results), ingredients, results)
                {
                    time = 100f,
                    description = STRINGS.MEDICALRESEARCH.DESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                    fabricators = new List<Tag>() { (Tag)ApothecaryConfig.ID },
                    sortOrder = 15
                };
            }
        }

        public GameObject CreatePrefab()
        {
            if (Settings.Instance.EnableMedicalResearchPoints)
                DefineRecipes();

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
