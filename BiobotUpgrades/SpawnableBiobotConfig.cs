using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using STRINGS;

namespace BiobotUpgrades
{
    class SpawnableBiobotConfig : IEntityConfig
    {
        const string ID = "SpawnableBiobot";

        public GameObject CreatePrefab()
        {
            GameObject looseEntity = EntityTemplates.CreateLooseEntity(
                ID,
                ROBOTS.MODELS.MORB.NAME,
                ROBOTS.MODELS.MORB.DESC,
                1f,
                true,
                Assets.GetAnim("morbRover_kanim"),
                "object",
                Grid.SceneLayer.Front,
                EntityTemplates.CollisionShape.RECTANGLE,
                0.8f,
                0.4f,
                true);

            looseEntity.AddTag(GameTags.IndustrialIngredient);
            return looseEntity;
        }

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
            GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(MorbRoverConfig.ID), inst.transform.GetPosition(), Grid.SceneLayer.Creatures);
            gameObject.SetActive(true);
            Util.KDestroyGameObject(inst);
        }
    }
}
