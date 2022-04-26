using STRINGS;
using System;
using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    public class FrostSickness : Sickness
    {
        public const string ID = "FrostSickness";
        public const string RECOVERY_ID = "FrostSicknessRecovery";

        public FrostSickness()
            : base(nameof(FrostSickness), Sickness.SicknessType.Pathogen, Sickness.Severity.Minor, 0.00025f, new List<Sickness.InfectionVector>()
            {
                Sickness.InfectionVector.Inhalation
            }, 2220f, RECOVERY_ID)
        {
            this.AddSicknessComponent((Sickness.SicknessComponent)new CommonSickEffectSickness());
            this.AddSicknessComponent((Sickness.SicknessComponent)new AttributeModifierSickness(new AttributeModifier[2]
            {
                new AttributeModifier("ThermalConductivityBarrier", -0.004f, (string) STRINGS.DISEASES.FROSTSICKNESS.NAME),
                new AttributeModifier("GermResistance", -3f, (string) STRINGS.DISEASES.FROSTSICKNESS.NAME)
            }));
            this.AddSicknessComponent((Sickness.SicknessComponent)new AnimatedSickness(new HashedString[3]
            {
                (HashedString) "anim_idle_cold_kanim",
                (HashedString) "anim_loco_run_cold_kanim",
                (HashedString) "anim_loco_walk_cold_kanim"
            }, Db.Get().Expressions.SickCold));
            this.AddSicknessComponent((Sickness.SicknessComponent)new PeriodicEmoteSickness((HashedString)"anim_idle_cold_kanim", new HashedString[3]
            {
                (HashedString) "idle_pre",
                (HashedString) "idle_default",
                (HashedString) "idle_pst"
            }, 15f));
        }
    }
}
