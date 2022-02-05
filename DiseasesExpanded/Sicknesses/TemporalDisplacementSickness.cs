using STRINGS;
using System;
using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    class TemporalDisplacementSickness : Sickness
    {
        // Sickness does not work, do not use
        public const string ID = nameof(TemporalDisplacementSickness);
        public const string RECOVERY_ID = "TemporalDisplacementSicknessRecovery";

        public TemporalDisplacementSickness()
            : base(nameof(TemporalDisplacementSickness), Sickness.SicknessType.Ailment, Sickness.Severity.Minor, 0.00025f, new List<Sickness.InfectionVector>()
            {
                Sickness.InfectionVector.Exposure
            }, 2220f, RECOVERY_ID)
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
            this.AddSicknessComponent((Sickness.SicknessComponent)new TemporalSicknessComponent(TemporalSicknessComponent.AnomalyMode.Displacement));
        }
    }
}
