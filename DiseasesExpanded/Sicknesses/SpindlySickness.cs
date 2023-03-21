using STRINGS;
using System;
using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    class SpindlySickness : Sickness
    {
        public const string ID = "SpindlySickness";
        public const string RECOVERY_ID = "SpindlySicknessRecovery";
        
        public SpindlySickness()
            : base(ID, Sickness.SicknessType.Ailment, Sickness.Severity.Minor, 0.00025f, new List<Sickness.InfectionVector>()
            {
                Sickness.InfectionVector.Contact
            }, 10000f, RECOVERY_ID)
        {
            float scale = Settings.Instance.SleepingCurse.SeverityScale;
            this.AddSicknessComponent((Sickness.SicknessComponent)new CommonSickEffectSickness());
            this.AddSicknessComponent((Sickness.SicknessComponent)new AttributeModifierSickness(new AttributeModifier[1]
            {
                new AttributeModifier("StaminaDelta", -0.016666666f * scale, (string) STRINGS.DISEASES.SPINDLYCURSE.NAME)
            }));
            this.AddSicknessComponent((Sickness.SicknessComponent)new SpindlySickness.SpindlySicknessComponent());
        }

        public class SpindlySicknessComponent : Sickness.SicknessComponent
        {
            public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
            {
                Narcolepsy narcolepsy = go.FindOrAddUnityComponent<Narcolepsy>();
                narcolepsy.smi.StartSM();

                if (narcolepsy.smi.master.GetSMI<StaminaMonitor.Instance>() == null)
                    Debug.Log($"{ModInfo.Namespace}: Null StaminaMonitor.Instance prevents from enforcing Narcolepsy.sleepy state in SpindlySicknessComponentOnInfect()");
                else
                    narcolepsy.smi.GoTo(narcolepsy.smi.sm.sleepy);

                return (object)narcolepsy.smi;
            }

            public override void OnCure(GameObject go, object instance_data)
            {
                ((StateMachine.Instance)instance_data).StopSM("Cured");
            }
        }
    }
}
