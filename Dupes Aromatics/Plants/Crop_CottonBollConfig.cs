using Klei.AI;
using STRINGS;
using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;


namespace Dupes_Aromatics.Plants
{
    public class Crop_CottonBollConfig : IEntityConfig
    {
        public string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        public const string ID = "RimedCotton";
        public const float GROW_TIME = 3600f;
        public static readonly Tag TAG = TagManager.Create(ID);

        public static ComplexRecipe recipe;

        public GameObject CreatePrefab()
        {
            GameObject go = EntityTemplates.CreateLooseEntity(
                ID,
                STRINGS.CROPS.COTTONBOLL.NAME,
                STRINGS.CROPS.COTTONBOLL.DESC,
                1f, 
                false, 
                Assets.GetAnim("item_cottonboll_kanim"), 
                "object", 
                Grid.SceneLayer.Front, 
                EntityTemplates.CollisionShape.CIRCLE, 
                0.35f, 
                0.35f, 
                true, 
                0, 
                SimHashes.Creature, 
                new List<Tag>
                {
                    GameTags.IndustrialIngredient,
                    GameTags.BuildingFiber
                });
            go.AddOrGet<EntitySplitter>();
            go.AddOrGet<SimpleMassStatusItem>();

            RegisterRecipe();

            return go;
        }

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }

        private void RegisterRecipe()
        {

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(ID, 4f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(BasicFabricConfig.ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(RockCrusherConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = 100f,
                description = ITEMS.INDUSTRIAL_PRODUCTS.BASIC_FABRIC.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { RockCrusherConfig.ID },
                sortOrder = 11
            };
        }
    }
}
