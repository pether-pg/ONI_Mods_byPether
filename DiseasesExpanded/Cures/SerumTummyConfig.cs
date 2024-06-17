using UnityEngine;
using System.Collections.Generic;
using Klei.AI;

namespace DiseasesExpanded
{
    class SerumTummyConfig : IEntityConfig
    {
        public const string ID = "TummySerum";
        public const string EFFECT_ID = "TummySerumEffect";
        public static ComplexRecipe recipe;
        public static string Name { get => STRINGS.CURES.TUMMYSERUM.NAME; }
        public static string Desc { get => STRINGS.CURES.TUMMYSERUM.DESC; }

        public static Effect GetEffect()
        {
            Effect serumEffect = new Effect(EFFECT_ID, STRINGS.CURES.TUMMYSERUM.NAME, STRINGS.CURES.TUMMYSERUM.DESC, 10 * 600, true, false, false);
            serumEffect.SelfModifiers = new List<AttributeModifier>();
            serumEffect.SelfModifiers.Add(new AttributeModifier("BladderDelta", -0.0416f, STRINGS.CURES.TUMMYSERUM.NAME));
            serumEffect.SelfModifiers.Add(new AttributeModifier("ToiletEfficiency", 0.1f, STRINGS.CURES.TUMMYSERUM.NAME));
            serumEffect.SelfModifiers.Add(new AttributeModifier("StaminaDelta", 0.0166f, STRINGS.CURES.TUMMYSERUM.NAME));

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

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, Name, Desc, 1f, true, Assets.GetAnim(Kanims.TummySerum), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToMedicine(looseEntity, info);
        }

        private void DefineRecipe()
        {
            if (!Settings.Instance.MooFlu.IncludeDisease)
                return;

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[3]
            {
                new ComplexRecipe.RecipeElement(FoodGermsFlask.ID, 1f),
                new ComplexRecipe.RecipeElement(GassyGermFlask.ID, 1f),
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
