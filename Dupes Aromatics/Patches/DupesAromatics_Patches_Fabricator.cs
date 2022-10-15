using HarmonyLib;
using Database;
using System;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;
using Dupes_Aromatics.Germs;

namespace Dupes_Aromatics.Patches
{
    class DupesAromatics_Patches_Fabricator
    {
        public const string FabricatorId = AirFilterConfig.ID;
        public const float RecipeTime = 600;

        public static Dictionary<ComplexRecipe, string> RecipesScents = new Dictionary<ComplexRecipe, string>();

        public static void RegisterAromaticsRecipe(ComplexRecipe.RecipeElement[] ingredients, string germId, string Description)
        {
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(SimHashes.CarbonDioxide.CreateTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };

            ComplexRecipe recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(FabricatorId, ingredients, results), ingredients, results)
            {
                time = RecipeTime,
                description = Description,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Ingredient,
                fabricators = new List<Tag>() { (Tag)FabricatorId },
                sortOrder = 1
            };

            if (RecipesScents == null)
                RecipesScents = new Dictionary<ComplexRecipe, string>();
            RecipesScents.Add(recipe, germId);
        }

        public static void SpawnGerms(GameObject go, string germId, float dt, int amountPerSecond = 1000)
        {
            Diseases diseases = Db.Get().Diseases;
            Disease germ = diseases.TryGet(germId);
            if (germ == null)
            {
                UpdateSourceVisibility(go, string.Empty);
                return;
            }

            SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(go.transform.position), diseases.GetIndex(germId), (int)(amountPerSecond * dt));
            UpdateSourceVisibility(go, germId);
        }

        public static void UpdateSourceVisibility(GameObject go, string germId)
        {
            DiseaseSourceVisualizer source = go.AddOrGet<DiseaseSourceVisualizer>();
            source.alwaysShowDisease = germId;
            source.UpdateVisibility();
        }

        [HarmonyPatch(typeof(ComplexFabricator))]
        [HarmonyPatch("Sim200ms")]
        public static class ComplexFabricator_Sim200ms_Patch
        {
            public static void Postfix(ComplexFabricator __instance, float dt)
            {
                if (!__instance.HasTag(FabricatorId))
                    return;

                ComplexRecipe recipe = __instance.CurrentWorkingOrder;
                if (recipe == null || !__instance.gameObject.GetComponent<Operational>().IsOperational)
                {
                    UpdateSourceVisibility(__instance.gameObject, string.Empty);
                    return;
                }

                if (RecipesScents.ContainsKey(recipe))
                    SpawnGerms(__instance.gameObject, RecipesScents[recipe], dt);
            }
        }

        [HarmonyPatch(typeof(AirFilterConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class AirFilterConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<Operational>();
                ComplexFabricator cf = go.AddOrGet<ComplexFabricator>();
                cf.duplicantOperated = false;
                BuildingTemplates.CreateComplexFabricatorStorage(go, cf);

                RegisterAromaticsRecipe(new ComplexRecipe.RecipeElement[1] { new ComplexRecipe.RecipeElement(SimHashes.Dirt.CreateTag(), 1f) }, RoseScent.ID, "RoseScent recipe");
                RegisterAromaticsRecipe(new ComplexRecipe.RecipeElement[1] { new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 1f) }, LavenderScent.ID, "LavenderScent recipe");
                RegisterAromaticsRecipe(new ComplexRecipe.RecipeElement[1] { new ComplexRecipe.RecipeElement(SimHashes.SlimeMold.CreateTag(), 1f) }, SlimeGerms.ID, "SlimeGerms recipe");
            }
        }
    }
}
