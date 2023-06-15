using Klei.AI;
using UnityEngine;

namespace DiseasesExpanded
{
    class CustomFxSickEffectSickness : Sickness.SicknessComponent
    {
        private string kanim;
        private string animName;
        private Vector3 offset;

        public static readonly string BLUE_SMOKE_KANIM = "de_fx_frost_kanim";
        public static readonly string NGAS_SMOKE_KANIM = "de_moo_odor_fx_kanim";
        public static readonly string FX_LOOP_ANIM = "working_loop";
        public static readonly Vector3 DEFAULT_OFFSET = new Vector3(0, 0, -0.1f);

        public CustomFxSickEffectSickness(string kanim, string animName, Vector3 offset)
        {
            this.kanim = kanim;
            this.animName = animName;
            this.offset = offset;
        }

        public CustomFxSickEffectSickness(string kanim)
        {
            this.kanim = kanim;
            this.animName = FX_LOOP_ANIM;
            this.offset = DEFAULT_OFFSET;
        }

        public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
        {
            KBatchedAnimController effect = FXHelpers.CreateEffect(kanim, go.transform.GetPosition() + offset, go.transform, true);
            effect.Play((HashedString)animName, KAnim.PlayMode.Loop);
            return (object)effect;
        }

        public override void OnCure(GameObject go, object instance_data) => ((Component)instance_data).gameObject.DeleteObject();
    }
}
