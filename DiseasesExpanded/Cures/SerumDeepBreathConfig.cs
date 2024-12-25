using UnityEngine;
using System.Collections.Generic;
using Klei.AI;

namespace DiseasesExpanded
{
    class SerumDeepBreathConfig : IEntityConfig
    {
        public const string ID = "DeepBreathSerum";
        public const string EFFECT_ID = "DeepBreathSerumEffect";
        public static ComplexRecipe recipe;

        public static string Name { get => STRINGS.CURES.DEEPBREATH.NAME; }
        public static string Desc { get => STRINGS.CURES.DEEPBREATH.DESC; }

        public static Effect GetEffect()
        {
            float wheezeScale = Settings.Instance.FrostPox.SeverityScale;
            Effect serumEffect = new Effect(EFFECT_ID, STRINGS.CURES.DEEPBREATH.NAME, STRINGS.CURES.DEEPBREATH.DESC, 10 * 600, true, false, false);
            serumEffect.SelfModifiers = new List<AttributeModifier>
            {
                new AttributeModifier(Db.Get().Attributes.AirConsumptionRate.Id, -0.025f, STRINGS.CURES.DEEPBREATH.NAME),                
                new AttributeModifier("BreathDelta", 0.284f, STRINGS.CURES.DEEPBREATH.NAME),
                new AttributeModifier("GermResistance", 2f * wheezeScale, STRINGS.CURES.DEEPBREATH.NAME)
            };
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

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, Name, Desc, 1f, true, Assets.GetAnim(Kanims.DeepBreathSerum), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToMedicine(looseEntity, info);
        }

        private void DefineRecipe()
        {
            if (!Settings.Instance.FrostPox.IncludeDisease)
                return;

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[3]
            {
                new ComplexRecipe.RecipeElement(FrostShardsFlask.ID, 1f),
                new ComplexRecipe.RecipeElement(SlimelungFlask.ID, 1f),
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
                sortOrder = 1,
                requiredTech = "MedicineIV"
            };
        }
    }
}
