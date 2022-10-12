using UnityEngine;

namespace ExoticCuisine
{

    public class DragonPlantFruitConfig : IEntityConfig
    {
        public static string ID = "DragonPlantFruit";

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public GameObject CreatePrefab()
        {
            GameObject prefab = EntityTemplates.ExtendEntityToFood(
                    EntityTemplates.CreateLooseEntity(DragonPlantFruitConfig.ID, 
                    STRINGS.FRUITS.DRAGONPLANT.NAME,
                    STRINGS.FRUITS.DRAGONPLANT.DESC, 
                    1f, 
                    false, 
                    Assets.GetAnim(DragonPlantConfig.FRUIT_KANIM), 
                    "object", Grid.SceneLayer.Front, 
                    EntityTemplates.CollisionShape.RECTANGLE, 
                    0.77f,
                    0.48f, 
                    true),
                new EdiblesManager.FoodInfo(DragonPlantFruitConfig.ID, "", 2400000f, 0, 255.15f, 277.15f, 4800f, true));

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