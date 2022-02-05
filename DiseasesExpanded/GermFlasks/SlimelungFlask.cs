using UnityEngine;

namespace DiseasesExpanded
{
    class SlimelungFlask : IEntityConfig
    {
        public const string ID = nameof(SlimelungFlask);

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public void OnPrefabInit(GameObject inst)
        {
            KAnimControllerBase kAnimBase = inst.GetComponent<KAnimControllerBase>();
            if (kAnimBase != null)
                kAnimBase.TintColour = ColorPalette.SlimelungGreen;
        }

        public void OnSpawn(GameObject inst)
        {
        }

        public GameObject CreatePrefab()
        {
            GameObject looseEntity = EntityTemplates.CreateLooseEntity(
                ID,
                string.Format(STRINGS.GERMFLASK.NAME, GermIdx.GetGermName(GermIdx.SlimelungIdx)),
                string.Format(STRINGS.GERMFLASK.DESC, GermIdx.GetGermName(GermIdx.SlimelungIdx)),
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
