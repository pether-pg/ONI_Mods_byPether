using STRINGS;
using System;
using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    public class GasSickness : Sickness
    {
        private const float COUGH_FREQUENCY = 20f;
        private const float COUGH_MASS = 0.1f;
        private const int DISEASE_AMOUNT = 1000;
        public const string ID = "GasSickness";
        public const string RECOVERY_ID = "GasSicknessRecovery";

        public GasSickness()
            : base(nameof(GasSickness), Sickness.SicknessType.Pathogen, Sickness.Severity.Minor, 0.00025f, new List<Sickness.InfectionVector>()
            {
                Sickness.InfectionVector.Inhalation
            }, 2220f, "GasSicknessRecovery")
        {
            this.AddSicknessComponent((Sickness.SicknessComponent)new CommonSickEffectSickness());
            this.AddSicknessComponent((Sickness.SicknessComponent)new AnimatedSickness(new HashedString[1]
            {
                (HashedString) "anim_idle_sick_kanim"
            }, Db.Get().Expressions.Sick));
            this.AddSicknessComponent((Sickness.SicknessComponent)new PeriodicEmoteSickness((HashedString)"anim_idle_sick_kanim", new HashedString[3]
            {
                (HashedString) "idle_pre",
                (HashedString) "idle_default",
                (HashedString) "idle_pst"
            }, 50f));
            this.AddSicknessComponent((Sickness.SicknessComponent)new GasSickness.GasSicknessComponent());
        }

        public class GasSicknessComponent : Sickness.SicknessComponent
        {
            public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
            {
                throw new NotImplementedException();
            }

            public override void OnCure(GameObject go, object instance_data)
            {
            }
        }
    }
}
