using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded.MutatingDisease
{
    class EnforcedVirusMutationConfig : IEntityConfig, IHasDlcRestrictions
    {
        public const string ID = "EnforcedVirusMutatio";

        public string[] GetDlcIds() => (string[])null; // Obsolete

        public string[] GetRequiredDlcIds() => (string[])null;

        public string[] GetForbiddenDlcIds() => (string[])null;

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