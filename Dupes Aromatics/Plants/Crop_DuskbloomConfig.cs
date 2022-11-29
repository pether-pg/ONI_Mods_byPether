using Database;
using TUNING;
using UnityEngine;
using STRINGS;
using System.Collections.Generic;
using Klei.AI;


namespace Dupes_Aromatics.Plants
{
    public class Crop_DuskbloomConfig : IEntityConfig
    {
        public string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        public const string ID = "Duskbloom";
        public const string SPICE_ID = "DuskbloomSpice";
        public const float GROW_TIME = 4500f;
        public static readonly Tag TAG = TagManager.Create(ID);

        public GameObject CreatePrefab()
        {
            GameObject ingredient = EntityTemplates.CreateLooseEntity(
                ID,
                STRINGS.CROPS.DUSKBLOOM.NAME,
                STRINGS.CROPS.DUSKBLOOM.DESC,
                1f,
                false,
                Assets.GetAnim("item_duskbloom_kanim"),
                "object",
                Grid.SceneLayer.Front,
                EntityTemplates.CollisionShape.CIRCLE,
                0.35f,
                0.35f,
                true,
                0,
                SimHashes.Creature,
                new List<Tag> { GameTags.IndustrialIngredient }
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
                new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(), 100f),
                new ComplexRecipe.RecipeElement(ID, 1f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(BasicCureConfig.ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            ComplexRecipe recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(ApothecaryConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = 100f,
                description = ITEMS.PILLS.BASICCURE.RECIPEDESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { ApothecaryConfig.ID },
                sortOrder = 10
            };
        }

        public static Spice CreateSpice(Spices parent)
        {
            BasicModUtils.MakeSpiceStrings(SPICE_ID, STRINGS.SPICES.LAVENDER.NAME, STRINGS.SPICES.LAVENDER.DESC);

            Spice spice = new Spice(
                parent,
                SPICE_ID,
                new Spice.Ingredient[2] {
                    new Spice.Ingredient() { IngredientSet = new Tag[1] { ID }, AmountKG = 0.1f },
                    new Spice.Ingredient() { IngredientSet = new Tag[1] { SimHashes.Aluminum.CreateTag() }, AmountKG = 3f }
                },
                LavenderScent.colorValue,
                Color.white,
                statBonus: new AttributeModifier(Db.Get().Attributes.Ranching.Id, 3, nameof(Spices)),
                imageName: "unknown",
                dlcID: DlcManager.AVAILABLE_EXPANSION1_ONLY
            );

            return spice;
        }
    }
}
