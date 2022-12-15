using System;
using TUNING;
using UnityEngine;
using STRINGS;
using System.Collections.Generic;
using Klei.AI;

namespace Dupes_Aromatics
{
    internal class Crop_DuskberryConfig : IEntityConfig
    {
        public string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        public const string ID = "Duskberry";
        public const float GROW_TIME = 4500f;


        public GameObject CreatePrefab()
        {
            GameObject template = EntityTemplates.CreateLooseEntity(
                ID,
                STRINGS.CROPS.DUSKBERRY.NAME,
                STRINGS.CROPS.DUSKBERRY.DESC,
                1f,
                false,
                Assets.GetAnim("fruit_duskberry_kanim"),
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
