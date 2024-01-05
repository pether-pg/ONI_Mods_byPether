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
            : base(ID, SicknessType.Ailment, Severity.Minor, 0.00025f, new List<InfectionVector>()
            {
                InfectionVector.Contact
            }, 10000f, RECOVERY_ID)
        {
            float scale = Settings.Instance.SleepingCurse.SeverityScale;
            this.AddSicknessComponent(new CommonSickEffectSickness());
            this.AddSicknessComponent(new AttributeModifierSickness(new AttributeModifier[1]
            {
                new AttributeModifier("StaminaDelta", -0.016666666f * scale, STRINGS.DISEASES.SPINDLYCURSE.NAME)
            }));
            this.AddSicknessComponent(new SpindlySicknessComponent());
        }

        public class SpindlySicknessComponent : SicknessComponent
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
