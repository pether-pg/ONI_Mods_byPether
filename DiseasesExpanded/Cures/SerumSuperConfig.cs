using UnityEngine;
using System.Collections.Generic;
using Klei.AI;

namespace DiseasesExpanded
{
    class SerumSuperConfig : IEntityConfig
    {
        public const string ID = "SuperSerum";
        public const string EFFECT_ID = "SuperSerumEffect";
        public static ComplexRecipe recipe;

        public static string Name { get => STRINGS.CURES.SUPERSERUM.NAME; }
        public static string Desc { get => STRINGS.CURES.SUPERSERUM.DESC; }

        public static Effect GetEffect()
        {
            float attributeChange = 5 * Settings.Instance.AlienGoo.SeverityScale;
            Effect serumEffect = new Effect(EFFECT_ID, STRINGS.CURES.SUPERSERUM.NAME, STRINGS.CURES.SUPERSERUM.DESC, 10 * 600, true, false, false);
            serumEffect.SelfModifiers = new List<AttributeModifier>();
            serumEffect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Athletics.Id, attributeChange, STRINGS.CURES.SUPERSERUM.NAME));
            serumEffect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Strength.Id, attributeChange, STRINGS.CURES.SUPERSERUM.NAME));
            serumEffect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Digging.Id, attributeChange, STRINGS.CURES.SUPERSERUM.NAME));
            serumEffect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Construction.Id, attributeChange, STRINGS.CURES.SUPERSERUM.NAME));
            serumEffect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Art.Id, attributeChange, STRINGS.CURES.SUPERSERUM.NAME));
            serumEffect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Caring.Id, attributeChange, STRINGS.CURES.SUPERSERUM.NAME));
            serumEffect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Learning.Id, attributeChange, STRINGS.CURES.SUPERSERUM.NAME));
            serumEffect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Machinery.Id, attributeChange, STRINGS.CURES.SUPERSERUM.NAME));
            serumEffect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Cooking.Id, attributeChange, STRINGS.CURES.SUPERSERUM.NAME));
            serumEffect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Botanist.Id, attributeChange, STRINGS.CURES.SUPERSERUM.NAME));
            serumEffect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Ranching.Id, attributeChange, STRINGS.CURES.SUPERSERUM.NAME));

            return serumEffect;
        }

        public string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }

        public GameObject CreatePrefab()
        {
            DefineRecipe();

            MedicineInfo info = new MedicineInfo(ID, EFFECT_ID, MedicineInfo.MedicineType.Booster, null, null);

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, Name, Desc, 1f, true, Assets.GetAnim(Kanims.SuperSerum), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToMedicine(looseEntity, info);
        }

        private void DefineRecipe()
        {
            if (!Settings.Instance.AlienGoo.IncludeDisease)
                return;

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[3]
            {
                new ComplexRecipe.RecipeElement(ZombieSporesFlask.ID, 1f),
                new ComplexRecipe.RecipeElement(AlienGermFlask.ID, 1f),
                VaccineApothecaryConfig.GetMainIngridient()
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(VaccineApothecaryConfig.ID, ingredients, results), ingredients, results)
            {
                time = VaccineApothecaryConfig.RecipeTime,
                description = Desc,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { VaccineApothecaryConfig.ID },
                sortOrder = 1
            };
        }
    }
}