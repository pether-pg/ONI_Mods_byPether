using Klei.AI;
using STR = STRINGS;
using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;
using Database;


namespace FragrantFlowers
{
    public class Crop_CottonBollConfig : IEntityConfig
    {
        public string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        public const string ID = "RimedCotton";
        public const string SPICE_ID = "CottonBollSpice";
        public const string SPICE_SPRITE = "mallowSpice_125";
        public const float GROW_TIME = 4500f;
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
                    GameTags.CookingIngredient, 
                    GameTags.IndustrialIngredient,
                    GameTags.BuildingFiber
                });
            go.AddOrGet<EntitySplitter>();
            go.AddOrGet<SimpleMassStatusItem>();

            Rottable.Def def = go.AddOrGetDef<Rottable.Def>();
            def.preserveTemperature = 255.15f;
            def.rotTemperature = 277.15f;
            def.spoilTime = 4800f;
            def.staleTime = def.spoilTime / 2;

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
                new ComplexRecipe.RecipeElement(ID, 2f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(BasicFabricConfig.ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(RockCrusherConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = 100f,
                description = STR.ITEMS.INDUSTRIAL_PRODUCTS.BASIC_FABRIC.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { RockCrusherConfig.ID },
                sortOrder = 11
            };
        }

        public static Spice CreateSpice(Spices parent)
        {
            BasicModUtils.MakeSpiceStrings(SPICE_ID, STRINGS.SPICES.MALLOW.NAME, STRINGS.SPICES.MALLOW.DESC);

            Spice spice = new Spice(
                parent,
                SPICE_ID,
                new Spice.Ingredient[2] {
                    new Spice.Ingredient() { IngredientSet = new Tag[1] { ID }, AmountKG = 0.1f },
                    new Spice.Ingredient() { IngredientSet = new Tag[1] { SimHashes.Ice.CreateTag() }, AmountKG = 3f }
                },
                MallowScent.colorValue,
                Color.white,
                statBonus: new AttributeModifier(Db.Get().Attributes.Athletics.Id, 3, nameof(Spices)),
                imageName: SPICE_SPRITE,
                dlcID: DlcManager.AVAILABLE_EXPANSION1_ONLY
            );

            return spice;
        }
    }
}
