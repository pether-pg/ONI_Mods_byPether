using STRINGS;
using System;
using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    public class FrostSickness : Sickness
    {
        private const float COUGH_FREQUENCY = 20f;
        private const float COUGH_MASS = 0.1f;
        private const int DISEASE_AMOUNT = 1000;
        public const string ID = "FrostSickness";
        public const string RECOVERY_ID = "FrostSicknessRecovery";

        public FrostSickness()
            : base(nameof(FrostSickness), Sickness.SicknessType.Pathogen, Sickness.Severity.Minor, 0.00025f, new List<Sickness.InfectionVector>()
            {
                Sickness.InfectionVector.Inhalation
            }, 2220f, "FrostSicknessRecovery")
        {
            this.AddSicknessComponent((Sickness.SicknessComponent)new CommonSickEffectSickness());
            this.AddSicknessComponent((Sickness.SicknessComponent)new AttributeModifierSickness(new AttributeModifier[2]
            {
                new AttributeModifier("ThermalConductivityBarrier", -0.004f, (string) DUPLICANTS.DISEASES.SLIMESICKNESS.NAME),
                new AttributeModifier("GermResistance", -3f, (string) DUPLICANTS.DISEASES.SLIMESICKNESS.NAME)
            }));
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
    }
}
