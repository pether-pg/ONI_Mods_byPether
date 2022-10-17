using Klei.AI;
using STRINGS;
using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;


namespace Dupes_Aromatics.Plants
{
    public class Crop_CottonBollConfig : IEntityConfig
    {
        public string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        public const string ID = "RimedCotton";
        public static readonly Tag TAG = TagManager.Create(ID);

        public GameObject CreatePrefab()
        {
            GameObject go = EntityTemplates.CreateLooseEntity(
                ID,
                STRINGS.CROPS.COTTONBOLL.NAME,
                STRINGS.CROPS.COTTONBOLL.DESC,
                1f, 
                false, 
                Assets.GetAnim("item_cottonboll_kanim"), 
                "object", 
                Grid.SceneLayer.Front, 
                EntityTemplates.CollisionShape.CIRCLE, 
                0.35f, 
                0.35f, 
                true, 
                0, 
                SimHashes.Creature, 
                new List<Tag>
                {
                    GameTags.IndustrialIngredient,
                    GameTags.BuildingFiber
                });
            go.AddOrGet<EntitySplitter>();
            go.AddOrGet<SimpleMassStatusItem>();
            return go;
        }

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }
    }
}
