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

        public const string Id = "Duskbloom";
        public static string Name = UI.FormatAsLink("Duskbloom", "Duskbloom".ToUpper());
        public static string Description = ("The gentle blossom of a " + UI.FormatAsLink("Duskbloom Lavender", Plant_DuskLavenderConfig.Id) + ". It has a delicate, sweet smell that is floral, herbal, and evergreen woodsy at the same time.");
        public static readonly Tag TAG = TagManager.Create("LavenderBloom");

        public GameObject CreatePrefab()
        {
            List<Tag> additionalTags = new List<Tag>();
            additionalTags.Add(GameTags.IndustrialIngredient);
            GameObject ingredient = EntityTemplates.CreateLooseEntity(
                Id,
                Name,
                Description,
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
