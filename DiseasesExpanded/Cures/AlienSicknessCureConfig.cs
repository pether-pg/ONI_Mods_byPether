using UnityEngine;
using Klei.AI;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class AlienSicknessCureConfig : IEntityConfig
    {
        public const string ID = "AlienSicknessCure";
        public const string EFFECT_ID = "AlienSicknessCureEffect";
        public static readonly float stressPerSecond = -25 / 600.0f;
        public static ComplexRecipe recipe;

        public static Effect GetEffect()
        {
            Effect effect = new Effect(EFFECT_ID, STRINGS.CURES.ALIENCURE.NAME, STRINGS.CURES.ALIENCURE.DESC, 5 * 600, true, true, false);
            effect.SelfModifiers = new List<AttributeModifier>
            {
                new AttributeModifier("StressDelta", stressPerSecond, STRINGS.CURES.ALIENCURE.NAME),
                new AttributeModifier(Db.Get().Attributes.Sneezyness.Id, 1, STRINGS.CURES.ALIENCURE.NAME)
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
            DefineRecipe();

            MedicineInfo info = new MedicineInfo(ID, null, MedicineInfo.MedicineType.CureSpecific, AdvancedDoctorStationConfig.ID, new string[] { AlienSickness.ID });

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, STRINGS.CURES.ALIENCURE.NAME, STRINGS.CURES.ALIENCURE.DESC, 1f, true, Assets.GetAnim(Kanims.AlienCureKanim), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToMedicine(looseEntity, info);
        }

        private void DefineRecipe()
        {
            if (!Settings.Instance.AlienGoo.IncludeDisease)
                return;

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                new ComplexRecipe.RecipeElement(GingerConfig.ID, 1f),
                new ComplexRecipe.RecipeElement(LightBugBlackConfig.EGG_ID, 1f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement((Tag) ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(ApothecaryConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = 200f,
                description = STRINGS.CURES.ALIENCURE.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { (Tag)ApothecaryConfig.ID },
                sortOrder = 20,
                requiredTech = "MedicineIV"
            };
        }
    }
}
