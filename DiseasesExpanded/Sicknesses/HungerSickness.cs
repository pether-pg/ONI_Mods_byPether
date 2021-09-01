using STRINGS;
using System;
using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    public class HungerSickness : Sickness
    {
        private const float COUGH_FREQUENCY = 20f;
        private const float COUGH_MASS = 0.1f;
        private const int DISEASE_AMOUNT = 1000;
        public const string ID = "HungerSickness";
        public const string RECOVERY_ID = "HungerSicknessRecovery";

        public HungerSickness()
            : base(nameof(HungerSickness), Sickness.SicknessType.Pathogen, Sickness.Severity.Minor, 0.00025f, new List<Sickness.InfectionVector>()
            {
                Sickness.InfectionVector.Inhalation
            }, 2220f, "HungerSicknessRecovery")
        {
            this.AddSicknessComponent((Sickness.SicknessComponent)new CommonSickEffectSickness());
            this.AddSicknessComponent((Sickness.SicknessComponent)new AttributeModifierSickness(new AttributeModifier[1]
            {
                new AttributeModifier("CaloriesDelta", -1666.682f, (string) DUPLICANTS.DISEASES.SLIMESICKNESS.NAME)
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
        }
    }
}
