using System;
using System.Collections.Generic;
using System.Linq;
using STRINGS;
using UnityEngine;

namespace DiseasesExpanded.RandomEvents.Configs
{
    class RandomFirstAidKitConfig : IEntityConfig
    {
        public const string ID = "RandomFirstAidKit";
        const string name = "First Aid Pack";
        const string desc = "Whatever is stored inside, it must be useful somehow...";

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public GameObject CreatePrefab()
        {
            EffectorValues tieR0_1 = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
            EffectorValues tieR0_2 = TUNING.NOISE_POLLUTION.NOISY.TIER0;
            KAnimFile anim = Assets.GetAnim((HashedString)"gravitas_first_aid_kit_kanim");
            EffectorValues decor = tieR0_1;
            EffectorValues noise = tieR0_2;

            // TODO: Make Loose Entity. To do this, create custom kanim with pivot point in center not on kanim base

            GameObject placedEntity =
                EntityTemplates.CreatePlacedEntity(
                    ID,
                    name,
                    desc,
                    50f,
                    anim,
                    "off",
                    Grid.SceneLayer.Building,
                    1,
                    1,
                    decor,
                    noise,
                    additionalTags: new List<Tag>() { GameTags.MedicalSupplies });

            placedEntity.AddComponent<RandomEvents.EntityScripts.FirstAidKitOpener>();

            return placedEntity;
        }


        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }
    }
}
