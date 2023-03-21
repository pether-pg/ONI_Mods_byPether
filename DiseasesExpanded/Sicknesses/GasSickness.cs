using STRINGS;
using System;
using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    public class GasSickness : Sickness
    {
        public const string ID = "GasSickness";
        public const string RECOVERY_ID = "GasSicknessRecovery";
        public const int GERMS_PER_FART = 20000;

        public GasSickness()
            : base(nameof(GasSickness), Sickness.SicknessType.Pathogen, Sickness.Severity.Minor, 0.00025f, new List<Sickness.InfectionVector>()
            {
                Sickness.InfectionVector.Inhalation,
                Sickness.InfectionVector.Exposure,
                Sickness.InfectionVector.Contact,
                Sickness.InfectionVector.Digestion,
            }, 2220f * Settings.Instance.MooFlu.SeverityScale, RECOVERY_ID)
        {
            this.AddSicknessComponent((Sickness.SicknessComponent)new CommonSickEffectSickness());
            this.AddSicknessComponent((Sickness.SicknessComponent)new AnimatedSickness(new HashedString[1]
            {
                (HashedString) "anim_idle_sick_kanim"
            }, Db.Get().Expressions.Sick));
            this.AddSicknessComponent((Sickness.SicknessComponent)new PeriodicEmoteSickness(Db.Get().Emotes.Minion.Sick, 50f));
            this.AddSicknessComponent((Sickness.SicknessComponent)new GasSickness.GasSicknessComponent());
        }
        public class GasSicknessComponent : Sickness.SicknessComponent
        {
            public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
            {
                Flatulence statesInstance = go.FindOrAddUnityComponent<Flatulence>();
                statesInstance.smi.StartSM();
                return (object)statesInstance.smi;
            }

            public override void OnCure(GameObject go, object instance_data)
            {
                ((StateMachine.Instance)instance_data).StopSM("Cured");
            }
        }
    }
}
