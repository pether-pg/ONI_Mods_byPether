using UnityEngine;

namespace ExoticCuisine
{
    class SpiralcicleFruitConfig : IEntityConfig
    {
        public const string ID = "SpiralcicleFruit";

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public GameObject CreatePrefab()
        {
            GameObject prefab = EntityTemplates.ExtendEntityToFood(
                    EntityTemplates.CreateLooseEntity(ID,
                    STRINGS.FRUITS.SPIRALCICLE.NAME,
                    STRINGS.FRUITS.SPIRALCICLE.DESC,
                    1f,
                    false,
                    Assets.GetAnim(SpiralciclePlantConfig.FRUIT_KANIM),
                    "object", Grid.SceneLayer.Front,
                    EntityTemplates.CollisionShape.RECTANGLE,
                    0.77f,
                    0.48f,
                    true),
                new EdiblesManager.FoodInfo(ID, "", 2400000f, 0, 255.15f, 277.15f, 4800f, true));

            return prefab;
        }

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }
    }
}