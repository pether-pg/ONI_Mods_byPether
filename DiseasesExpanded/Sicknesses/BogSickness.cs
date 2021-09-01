using STRINGS;
using System;
using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    public class BogSickness : Sickness
    {
        private const float COUGH_FREQUENCY = 20f;
        private const float COUGH_MASS = 0.1f;
        private const int DISEASE_AMOUNT = 1000;
        public const string ID = "BogSickness";
        public const string RECOVERY_ID = "BogSicknessRecovery";

        public BogSickness()
            : base(nameof(BogSickness), Sickness.SicknessType.Pathogen, Sickness.Severity.Minor, 0.00025f, new List<Sickness.InfectionVector>()
            {
                Sickness.InfectionVector.Inhalation
            }, 2220f, "BogSicknessRecovery")
        {
            this.AddSicknessComponent((Sickness.SicknessComponent)new CommonSickEffectSickness());
            this.AddSicknessComponent((Sickness.SicknessComponent)new AttributeModifierSickness(new AttributeModifier[11]
            {
                new AttributeModifier(Db.Get().Attributes.Athletics.Id, -10f, (string) DUPLICANTS.DISEASES.ZOMBIESICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Strength.Id, -10f, (string) DUPLICANTS.DISEASES.ZOMBIESICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Digging.Id, -10f, (string) DUPLICANTS.DISEASES.ZOMBIESICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Construction.Id, -10f, (string) DUPLICANTS.DISEASES.ZOMBIESICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Art.Id, -10f, (string) DUPLICANTS.DISEASES.ZOMBIESICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Caring.Id, -10f, (string) DUPLICANTS.DISEASES.ZOMBIESICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Learning.Id, -10f, (string) DUPLICANTS.DISEASES.ZOMBIESICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Machinery.Id, -10f, (string) DUPLICANTS.DISEASES.ZOMBIESICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Cooking.Id, -10f, (string) DUPLICANTS.DISEASES.ZOMBIESICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Botanist.Id, -10f, (string) DUPLICANTS.DISEASES.ZOMBIESICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Ranching.Id, -10f, (string) DUPLICANTS.DISEASES.ZOMBIESICKNESS.NAME)
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
