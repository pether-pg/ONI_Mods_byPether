using UnityEngine;

namespace DiseasesExpanded
{
    class MutatingGermFlask : IEntityConfig, IHasDlcRestrictions
    {
        public const string ID = nameof(MutatingGermFlask);

        public string[] GetDlcIds() => (string[])null; // Obsolete

        public string[] GetRequiredDlcIds() => (string[])null;

        public string[] GetForbiddenDlcIds() => (string[])null;

        public void OnPrefabInit(GameObject inst)
        {
            KAnimControllerBase kAnimBase = inst.GetComponent<KAnimControllerBase>();
            if (kAnimBase != null && MutationData.IsReadyToUse())
                kAnimBase.TintColour = MutationData.Instance.GetGermColor();
        }

        public void OnSpawn(GameObject inst)
        {
        }

        public GameObject CreatePrefab()
        {
            GameObject looseEntity = EntityTemplates.CreateLooseEntity(
                ID,
                string.Format(STRINGS.GERMFLASK.NAME, GermIdx.GetGermName(GermIdx.MutatingGermsIdx)),
                string.Format(STRINGS.GERMFLASK.DESC_NOGERM, GermIdx.GetGermName(GermIdx.MutatingGermsIdx)),
                1f,
                true,
                Assets.GetAnim(Kanims.GermFlaskKanim),
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
