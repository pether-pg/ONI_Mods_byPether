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

        public const string Id = "SpinosaRose";
        public static string Name = UI.FormatAsLink("Spinosa Rose", "SpinosaRose".ToUpper());
        public static string Description = ("The beautiful blossom of a " + UI.FormatAsLink("Spinosa", Plant_SpinosaConfig.Id) + ". It has a simultaneously sweet and spicy smell, giving ode to meadows, honey, and fruit notes.");
        public static readonly Tag TAG = TagManager.Create("SpinosaRose");

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
