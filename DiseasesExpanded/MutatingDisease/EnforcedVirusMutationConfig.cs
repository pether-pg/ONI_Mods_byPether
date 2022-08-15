using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded.MutatingDisease
{
    class EnforcedVirusMutationConfig : IEntityConfig
    {
        public const string ID = "EnforcedVirusMutatio";

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
            MutationData.Instance.Mutate(inst);
            Util.KDestroyGameObject(inst);
        }

        public GameObject CreatePrefab()
        {
            GameObject looseEntity = EntityTemplates.CreateLooseEntity(
                ID,
                "Enforced Mutation",
                "Enforces Virus mutation for debug purposes",
                1f,
                true,
                Assets.GetAnim(Kanims.ControlledMutation),
                "object",
                Grid.SceneLayer.Front,
                EntityTemplates.CollisionShape.RECTANGLE,
                0.8f,
                0.4f,
                true);

            looseEntity.AddTag(GameTags.IndustrialIngredient);

            return looseEntity;
        }
    }
}