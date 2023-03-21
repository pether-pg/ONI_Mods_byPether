using UnityEngine;
using System.Collections.Generic;
using Klei.AI;

namespace DiseasesExpanded
{
    class SerumYummyConfig : IEntityConfig
    {
        public const string ID = "YummySerum";
        public const string EFFECT_ID = "YummySerumEffect";
        public static ComplexRecipe recipe;
        public static string Name { get => STRINGS.CURES.YUMMYSERUM.NAME; }
        public static string Desc { get => STRINGS.CURES.YUMMYSERUM.DESC; }

        public static Effect GetEffect()
        {
            Effect serumEffect = new Effect(EFFECT_ID, STRINGS.CURES.YUMMYSERUM.NAME, STRINGS.CURES.YUMMYSERUM.DESC, 10 * 600, true, false, false);
            serumEffect.SelfModifiers = new List<AttributeModifier>();
            serumEffect.SelfModifiers.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, HungerSickness.caloriesPerDay / 4, STRINGS.CURES.YUMMYSERUM.NAME));
            serumEffect.SelfModifiers.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.deltaAttribute.Id, 10.0f / 600.0f, STRINGS.CURES.YUMMYSERUM.NAME));

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
            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[3]
            {
                new ComplexRecipe.RecipeElement(HungermsFlask.ID, 1f),
                new ComplexRecipe.RecipeElement(BogBugsFlask.ID, 1f),
                new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(), 100f)
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

            MedicineInfo info = new MedicineInfo(ID, EFFECT_ID, MedicineInfo.MedicineType.Booster, null, null);

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, Name, Desc, 1f, true, Assets.GetAnim(Kanims.YummySerum), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToMedicine(looseEntity, info);
        }
    }
}
