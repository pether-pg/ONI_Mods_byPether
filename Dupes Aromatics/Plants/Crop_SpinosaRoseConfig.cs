using System;
using TUNING;
using UnityEngine;
using STRINGS;
using System.Collections.Generic;
using Klei.AI;

namespace Dupes_Aromatics.Plants
{
    public class Crop_SpinosaRoseConfig : IEntityConfig
    {
        public string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        public const string ID = "SpinosaRose";
        public const float GROW_TIME = 3600f;
        public static readonly Tag TAG = TagManager.Create(ID);

        public GameObject CreatePrefab()
        {
            GameObject ingredient = EntityTemplates.CreateLooseEntity(
                ID, 
                STRINGS.CROPS.SPINOSAROSE.NAME,
                STRINGS.CROPS.SPINOSAROSE.DESC, 
                1f, 
                false, 
                Assets.GetAnim("item_spinosarose_kanim"), 
                "object", 
                Grid.SceneLayer.Front, 
                EntityTemplates.CollisionShape.CIRCLE, 
                0.35f, 
                0.35f, 
                true, 
                0, 
                SimHashes.Creature, 
                new List<Tag>{GameTags.IndustrialIngredient}
                );
            ingredient.AddOrGet<EntitySplitter>();
            ingredient.AddOrGet<SimpleMassStatusItem>();

            DefineRecipe();

            return ingredient;
        }

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }

        public static void DefineRecipe()
        {
            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Phosphorite.CreateTag(), 100f),
                new ComplexRecipe.RecipeElement(ID, 1f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(IntermediateCureConfig.ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            ComplexRecipe recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(ApothecaryConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = 100f,
                description = ITEMS.PILLS.INTERMEDIATECURE.RECIPEDESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { ApothecaryConfig.ID },
                sortOrder = 10
            };
        }
    }
}
