using System;
using TUNING;
using UnityEngine;
using STRINGS;
using System.Collections.Generic;
using Klei.AI;


namespace Dupes_Aromatics.Plants
{
    public class Crop_DuskbloomConfig : IEntityConfig
    {
        public string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        public const string ID = "Duskbloom";
        public static readonly Tag TAG = TagManager.Create(ID);

        public GameObject CreatePrefab()
        {
            GameObject ingredient = EntityTemplates.CreateLooseEntity(
                ID,
                STRINGS.CROPS.DUSKBLOOM.NAME,
                STRINGS.CROPS.DUSKBLOOM.DESC,
                1f,
                false,
                Assets.GetAnim("item_duskbloom_kanim"),
                "object",
                Grid.SceneLayer.Front,
                EntityTemplates.CollisionShape.CIRCLE,
                0.35f,
                0.35f,
                true,
                0,
                SimHashes.Creature,
                new List<Tag> { GameTags.IndustrialIngredient }
                );
            ingredient.AddOrGet<EntitySplitter>();
            ingredient.AddOrGet<SimpleMassStatusItem>();
            return ingredient;
        }

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }
    }
}
