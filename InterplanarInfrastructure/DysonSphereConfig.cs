using UnityEngine;

namespace InterplanarInfrastructure
{
    class DysonSphereConfig : IEntityConfig
    {
        public const string ID = "DysonSphere";

        public string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public GameObject CreatePrefab()
        {
            GameObject entity = EntityTemplates.CreateEntity(ID, "Temporal Tear Duson Sphere");
            entity.AddOrGet<SaveLoadRoot>();
            entity.AddOrGet<DysonSphere>();
            entity.AddOrGet<ClusterDestinationSelector>();
            return entity;
        }

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }
    }
}
