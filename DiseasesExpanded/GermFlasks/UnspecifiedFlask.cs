using UnityEngine;

namespace DiseasesExpanded
{
    class UnspecifiedFlask : IEntityConfig, IHasDlcRestrictions
    {
        public const string ID = nameof(UnspecifiedFlask);

        public string[] GetDlcIds() => (string[])null; // Obsolete

        public string[] GetRequiredDlcIds() => (string[])null;

        public string[] GetForbiddenDlcIds() => (string[])null;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }

        public GameObject CreatePrefab()
        {
            GameObject looseEntity = EntityTemplates.CreateLooseEntity(
                ID,
                string.Format(STRINGS.GERMFLASK.NAME, STRINGS.UNSPECIFIEDFLASK.NAME),
                string.Format(STRINGS.GERMFLASK.DESC, STRINGS.UNSPECIFIEDFLASK.DESC),
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
