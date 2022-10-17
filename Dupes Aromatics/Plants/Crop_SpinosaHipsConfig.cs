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

        public const string ID = "SpinosaHips";

        public GameObject CreatePrefab()
        {
            GameObject template = EntityTemplates.CreateLooseEntity(
                ID, 
                STRINGS.CROPS.SPINOSAHIPS.NAME,
                STRINGS.CROPS.SPINOSAHIPS.DESC,
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
                ID, 
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
