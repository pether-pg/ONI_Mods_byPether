using System;
using TUNING;
using UnityEngine;
using STRINGS;
using System.Collections.Generic;
using Klei.AI;

namespace Dupes_Aromatics.Plants
{
    public class Crop_SpinosaHipsConfig : IEntityConfig
    {
        public string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        public const string Id = "SpinosaHips";
        public static string Name = UI.FormatAsLink("Spinosa Hips", "SpinosaHips".ToUpper());
        public static string Description = ("A small, berry-sized, reddish fruiting body of a " + UI.FormatAsLink("Fruiting Spinosa", Plant_SuperSpinosaConfig.Id) + ". These have a floral, slightly sweet flavor with a touch of tartness, rich in Vitamin C.");


        public GameObject CreatePrefab()
        {
            GameObject template = EntityTemplates.CreateLooseEntity(
                Id, 
                Name, 
                Description, 
                1f, 
                false, 
                Assets.GetAnim("fruit_spinosahips_kanim"), 
                "object",
                Grid.SceneLayer.Front, 
                EntityTemplates.CollisionShape.RECTANGLE, 
                0.8f, 
                0.4f, 
                true,
                0, 
                SimHashes.Creature, 
                null);

            EdiblesManager.FoodInfo foodInfo = new EdiblesManager.FoodInfo(
                Id, 
                "", 
                1200000f,
                -1, 
                255.15f, 
                277.15f, 
                3200f,
                true);

            return EntityTemplates.ExtendEntityToFood(template, foodInfo);
        }

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }
    }
}
