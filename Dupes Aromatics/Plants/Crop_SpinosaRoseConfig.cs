using System;
using TUNING;
using UnityEngine;
using STRINGS;
using System.Collections.Generic;
using Klei.AI;

namespace Dupes_Aromatics.Plants
{
    public class Crop_SpinosaRoseConfig : IEntityConfig
    {
        public string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        public const string ID = "SpinosaRose";
        public static readonly Tag TAG = TagManager.Create(ID);

        public GameObject CreatePrefab()
        {
            GameObject ingredient = EntityTemplates.CreateLooseEntity(
                ID, 
                STRINGS.CROPS.SPINOSAROSE.NAME,
                STRINGS.CROPS.SPINOSAROSE.DESC, 
                1f, 
                false, 
                Assets.GetAnim("item_spinosarose_kanim"), 
                "object", 
                Grid.SceneLayer.Front, 
                EntityTemplates.CollisionShape.CIRCLE, 
                0.35f, 
                0.35f, 
                true, 
                0, 
                SimHashes.Creature, 
                new List<Tag>{GameTags.IndustrialIngredient}
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
