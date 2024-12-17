using UnityEngine;
using System.Collections.Generic;
using Klei.AI;

namespace DiseasesExpanded
{
    class HappyPillConfig : IEntityConfig
    {
        public const string ID = "HappyPill";
        public const string EFFECT_ID = "HappyPillEffect";
        public static ComplexRecipe recipe;
        public static readonly float stressPerSecond = -15.0f / 600.0f;
        public static readonly float moraleBonus = 5;
        public static readonly float attributePenalty = -5;

        public static Effect GetEffect()
        {
            Effect effect = new Effect(EFFECT_ID, STRINGS.CURES.HAPPYPILL.NAME, STRINGS.CURES.HAPPYPILL.DESC, 600, true, true, false);
            effect.SelfModifiers = new List<AttributeModifier>
            {
                new AttributeModifier("StressDelta", stressPerSecond, STRINGS.CURES.HAPPYPILL.NAME),
                new AttributeModifier("QualityOfLife", moraleBonus, STRINGS.CURES.HAPPYPILL.NAME),
                new AttributeModifier(Db.Get().Attributes.Athletics.Id, attributePenalty, STRINGS.CURES.HAPPYPILL.NAME),
                new AttributeModifier(Db.Get().Attributes.Strength.Id, attributePenalty, STRINGS.CURES.HAPPYPILL.NAME),
                new AttributeModifier(Db.Get().Attributes.Digging.Id, attributePenalty, STRINGS.CURES.HAPPYPILL.NAME),
                new AttributeModifier(Db.Get().Attributes.Construction.Id, attributePenalty, STRINGS.CURES.HAPPYPILL.NAME),
                new AttributeModifier(Db.Get().Attributes.Art.Id, attributePenalty, STRINGS.CURES.HAPPYPILL.NAME),
                new AttributeModifier(Db.Get().Attributes.Caring.Id, attributePenalty, STRINGS.CURES.HAPPYPILL.NAME),
                new AttributeModifier(Db.Get().Attributes.Learning.Id, attributePenalty, STRINGS.CURES.HAPPYPILL.NAME),
                new AttributeModifier(Db.Get().Attributes.Machinery.Id, attributePenalty, STRINGS.CURES.HAPPYPILL.NAME),
                new AttributeModifier(Db.Get().Attributes.Cooking.Id, attributePenalty, STRINGS.CURES.HAPPYPILL.NAME),
                new AttributeModifier(Db.Get().Attributes.Botanist.Id, attributePenalty, STRINGS.CURES.HAPPYPILL.NAME),
                new AttributeModifier(Db.Get().Attributes.Ranching.Id, attributePenalty, STRINGS.CURES.HAPPYPILL.NAME)
            };

            return effect;
        }

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }
        public GameObject CreatePrefab()
        {
            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                new ComplexRecipe.RecipeElement(SimHashes.OxyRock.CreateTag(), 1f),
                new ComplexRecipe.RecipeElement(LightBugConfig.EGG_ID, 1f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement((Tag) ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(ApothecaryConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = 100f,
                description = STRINGS.CURES.HAPPYPILL.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { (Tag)ApothecaryConfig.ID },
                sortOrder = 12
            };

            MedicineInfo info = new MedicineInfo(ID, EFFECT_ID, MedicineInfo.MedicineType.Booster, (string)null);

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, STRINGS.CURES.HAPPYPILL.NAME, STRINGS.CURES.HAPPYPILL.DESC, 1f, true, Assets.GetAnim(Kanims.HappyPill), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToMedicine(looseEntity, info);
        }
    }
}
