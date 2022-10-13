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
        public static readonly Tag TAG = TagManager.Create("CottonBoll");
        private AttributeModifier decorModifier = new AttributeModifier("Decor", 0.1f, Name, true, false, true);

        public static string Name = UI.FormatAsLink("Rimed Cotton Boll", ID.ToUpper());
        public static string Description = $"A soft, fluffy staple fiber that grows in a boll encased in ice crystals.  The fiber of the boll is almost pure cellulose, but also contains high concentration of aromatic oils that gives it a smooth and pleasing scent.";

        public GameObject CreatePrefab()
        {
            List<Tag> additionalTags = new List<Tag>();
            additionalTags.Add(GameTags.IndustrialIngredient);
            GameObject go = EntityTemplates.CreateLooseEntity(
                "RimedCotton", 
                Name, 
                Description, 
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
