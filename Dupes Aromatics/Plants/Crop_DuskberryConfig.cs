using System;
using TUNING;
using UnityEngine;
using STRINGS;
using System.Collections.Generic;
using Klei.AI;

namespace Dupes_Aromatics.Plants
{
    internal class Crop_DuskberryConfig : IEntityConfig
    {
        public string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        public const string Id = "Duskberry";
        public static string Name = UI.FormatAsLink("Duskberry", "Duskberry".ToUpper());
        public static string Description = ("A small, soft, jelly filled fruiting body of a " + UI.FormatAsLink("Duskberry Lavender", Plant_SuperDuskLavenderConfig.Id) + ". They taste delightful and have a slightly sweet tart flavor that is mixed with a little bit of acidic.");


        public GameObject CreatePrefab()
        {
            GameObject template = EntityTemplates.CreateLooseEntity(
                Id,
                Name,
                Description,
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
