using STRINGS;
using System;
using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    public class HungerSickness : Sickness
    {
        public const float DURATION = 2220f;
        public const string ID = "HungerSickness";
        public const string CRITTER_EFFECT_ID = "CritterHungerSickness";
        public const string RECOVERY_ID = "HungerSicknessRecovery";
        public const float caloriesPerDay = 1666.682f;
        public static Effect GetCritterSicknessEffect()
        {
            float scale = Settings.Instance.HungerGerms.SeverityScale;
            Effect effect = new Effect(CRITTER_EFFECT_ID, STRINGS.DISEASES.HUNGERSICKNESS.NAME, STRINGS.DISEASES.HUNGERSICKNESS.DESCRIPTION, DURATION, true, true, true);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier(Db.Get().CritterAttributes.Metabolism.Id, 100 * scale * (Settings.Instance.RebalanceForDiseasesRestored ? 3 : 1)));
            return effect;
        }

        public HungerSickness()
            : base(nameof(HungerSickness), Sickness.SicknessType.Pathogen, Sickness.Severity.Minor, 0.00025f, new List<Sickness.InfectionVector>()
            {
                Sickness.InfectionVector.Contact,
                Sickness.InfectionVector.Exposure,
                Sickness.InfectionVector.Inhalation
            }, DURATION, RECOVERY_ID)
        {
            float scale = Settings.Instance.HungerGerms.SeverityScale;
            this.AddSicknessComponent((Sickness.SicknessComponent)new CommonSickEffectSickness());
            this.AddSicknessComponent((Sickness.SicknessComponent)new AttributeModifierSickness(new AttributeModifier[1]
            {
                new AttributeModifier("CaloriesDelta", -caloriesPerDay * scale *(Settings.Instance.RebalanceForDiseasesRestored ? 3 : 1), (string) STRINGS.DISEASES.HUNGERSICKNESS.NAME)
            }));
            this.AddSicknessComponent((Sickness.SicknessComponent)new AnimatedSickness(new HashedString[1]
            {
                (HashedString) "anim_idle_sick_kanim"
            }, Db.Get().Expressions.Sick));
            this.AddSicknessComponent((Sickness.SicknessComponent)new PeriodicEmoteSickness(Db.Get().Emotes.Minion.Sick, 50f));
        }
    }
}
