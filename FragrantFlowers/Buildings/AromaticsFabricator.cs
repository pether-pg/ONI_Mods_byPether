using Database;
using Klei.AI;
using UnityEngine;
using System.Collections.Generic;

namespace FragrantFlowers
{
    class AromaticsFabricator : ComplexFabricator, ISim200ms
    {
        public const string FabricatorId = VaporizerConfig.ID;
        public const float WORKING_TIME = 600;
        private string LastGermId = string.Empty;

        public static readonly Tag BasicCanIngridientTag = SimHashes.Ethanol.CreateTag();
        public static readonly float BasicCanIngridientMass = 100;
        public static Dictionary<ComplexRecipe, string> RecipesScents = new Dictionary<ComplexRecipe, string>();

        public static void RegisterAromaticsRecipe(ComplexRecipe.RecipeElement[] ingredients, string germId, string Description)
        {
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(SimHashes.CarbonDioxide.CreateTag(), 0.0f)
            };

            ComplexRecipe recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(FabricatorId, ingredients, results), ingredients, results)
            {
                time = 60,
                description = Description,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Ingredient,
                fabricators = new List<Tag>() { (Tag)FabricatorId },
                sortOrder = 1
            };

            if (RecipesScents == null)
                RecipesScents = new Dictionary<ComplexRecipe, string>();
            RecipesScents.Add(recipe, germId);
        }

        public void SpawnGerms(GameObject go, string germId, float dt, int amountPerSecond = 5000)
        {
            Diseases diseases = Db.Get().Diseases;
            Disease germ = diseases.TryGet(germId);
            if (germ == null)
            {
                UpdateSourceVisibility(go, string.Empty);
                return;
            }

            SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(go.transform.position), diseases.GetIndex(germId), (int)(amountPerSecond * dt));
        }

        public void UpdateSourceVisibility(GameObject go, string germId)
        {
            DiseaseSourceVisualizer source = go.AddOrGet<DiseaseSourceVisualizer>();
            source.alwaysShowDisease = germId;
            source.UpdateVisibility();
        }

        public string GetGermIdFromRecipe(ComplexRecipe recipe)
        {
            if (recipe != null && RecipesScents.ContainsKey(recipe))
                return RecipesScents[recipe];
            return string.Empty;
        }

        private float Delta(ComplexRecipe recipe, float dt)
        {
            return dt / WORKING_TIME;
        }

        public void Sim200ms(float dt)
        {
            base.Sim200ms(dt);

            ComplexRecipe recipe = CurrentWorkingOrder;

            if (recipe == null || !operational.IsOperational)
            {
                UpdateSourceVisibility(gameObject, string.Empty);
                return;
            }

            foreach (var ingridient in recipe.ingredients)
                buildStorage.ConsumeIgnoringDisease(ingridient.material, ingridient.amount * Delta(recipe, dt));

            string currentGermId = GetGermIdFromRecipe(recipe);
            SpawnGerms(gameObject, currentGermId, dt);

            if (LastGermId == currentGermId)
                return;

            LastGermId = currentGermId;
            if (!string.IsNullOrEmpty(currentGermId))
                UpdateSourceVisibility(gameObject, currentGermId);
        }

        protected override List<GameObject> SpawnOrderProduct(ComplexRecipe recipe)
        {
            return new List<GameObject>();
        }

        public override List<Descriptor> AdditionalEffectsForRecipe(ComplexRecipe recipe)
        {
            List<Descriptor> result = new List<Descriptor>();
            string germId = GetGermIdFromRecipe(recipe);
            if (string.IsNullOrEmpty(germId))
                return result;

            string germName = Db.Get().Diseases.Get(germId).Name; 
            
            result.Add(new Descriptor(STRINGS.DESCRIPTORS.SPAWNGERMS.NAME.Replace("{GERMS}", germName), 
                                        STRINGS.DESCRIPTORS.SPAWNGERMS.DESC.Replace("{GERMS}", germName)));
            return result;
        }
    }
}
