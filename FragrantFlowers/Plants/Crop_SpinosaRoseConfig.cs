using System;
using TUNING;
using UnityEngine;
using STRINGS;
using System.Collections.Generic;
using Klei.AI;
using Database;

namespace FragrantFlowers
{
    public class Crop_SpinosaRoseConfig : IEntityConfig
    {
        public string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        public const string ID = "SpinosaRose";
        public const string SPICE_ID = "SpinosaRoseSpice";
        public const string SPICE_SPRITE = "roseSpice_125";
        public const float GROW_TIME = 4500f;
        public static readonly Tag TAG = TagManager.Create(ID);

        public GameObject CreatePrefab()
        {
            GameObject go = EntityTemplates.CreateLooseEntity(
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
                new List<Tag>{
                    GameTags.CookingIngredient,
                    GameTags.IndustrialIngredient
                });
            go.AddOrGet<EntitySplitter>();
            go.AddOrGet<SimpleMassStatusItem>();

            Rottable.Def def = go.AddOrGetDef<Rottable.Def>();
            def.preserveTemperature = 255.15f;
            def.rotTemperature = 277.15f;
            def.spoilTime = 4800f;
            def.staleTime = def.spoilTime / 2;

            DefineRecipe();

            return go;
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
                new ComplexRecipe.RecipeElement(ID, 1f),
                new ComplexRecipe.RecipeElement(SimHashes.Phosphorite.CreateTag(), 1f),
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
                sortOrder = 10,
                requiredTech = "MedicineII"
            };
        }

        public static Spice CreateSpice(Spices parent)
        {
            BasicModUtils.MakeSpiceStrings(SPICE_ID, STRINGS.SPICES.ROSE.NAME, STRINGS.SPICES.ROSE.DESC);

            Spice spice = new Spice(
                parent,
                SPICE_ID,
                new Spice.Ingredient[2] {
                    new Spice.Ingredient() { IngredientSet = new Tag[1] { ID }, AmountKG = 0.1f },
                    new Spice.Ingredient() { IngredientSet = new Tag[1] { SimHashes.SandStone.CreateTag() }, AmountKG = 3f }
                },
                RoseScent.colorValue,
                Color.white,
                statBonus: new AttributeModifier(Db.Get().Attributes.Learning.Id, 3, nameof(Spices)),
                imageName: SPICE_SPRITE,
                dlcID: DlcManager.AVAILABLE_EXPANSION1_ONLY
            );

            return spice;
        }
    }
}
