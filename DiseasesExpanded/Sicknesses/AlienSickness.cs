using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    class AlienSickness : Sickness
    {
        public const string ID = nameof(AlienSickness);
        public const string RECOVERY_ID = "AlienSicknessRecovery";
        public const string ASSIMILATION_EFFECT_ID = "AlienSicknessAssimilation";

        public static readonly float stressPerSecond = (25 / 600.0f) * (Settings.Instance.RebalanceForDiseasesRestored ? 2 : 1);

        public static Effect GetRecoveryEffect()
        {
            Effect alienRecovery = new Effect(AlienSickness.RECOVERY_ID, STRINGS.EFFECTS.ALIENRECOVERY.NAME, STRINGS.EFFECTS.ALIENRECOVERY.DESC, 5 * 600, true, true, false);
            alienRecovery.SelfModifiers = new List<AttributeModifier>();
            alienRecovery.SelfModifiers.Add(new AttributeModifier("StressDelta", -stressPerSecond, STRINGS.EFFECTS.ALIENRECOVERY.NAME));
            return alienRecovery;
        }
        public static Effect GetAssimilationEffect()
        {
            Effect alienRecovery = new Effect(AlienSickness.ASSIMILATION_EFFECT_ID, STRINGS.EFFECTS.ALIENASSIMILATION.NAME, STRINGS.EFFECTS.ALIENASSIMILATION.DESC, 5 * 600, true, true, true);
            alienRecovery.SelfModifiers = new List<AttributeModifier>();
            alienRecovery.SelfModifiers.Add(new AttributeModifier("StressDelta", 2 * stressPerSecond, STRINGS.EFFECTS.ALIENASSIMILATION.NAME));
            return alienRecovery;
        }

        public static void ApplyFinishEffect(GameObject go, float assimilationPercent)
        {
            Effects effects = go.GetComponent<Effects>();
            if (effects == null)
                return;

            if (assimilationPercent >= 1 && !effects.HasEffect(ASSIMILATION_EFFECT_ID))
                effects.Add(GetAssimilationEffect(), true);
            else if(!effects.HasEffect(RECOVERY_ID))
                effects.Add(GetRecoveryEffect(), true);
        }

        public AlienSickness()
            : base(ID, Sickness.SicknessType.Pathogen, Sickness.Severity.Minor, 0.00025f, new List<Sickness.InfectionVector>()
            {
                Sickness.InfectionVector.Inhalation,
                Sickness.InfectionVector.Contact
            }, 3000f, recovery_effect: null)
        {
            this.AddSicknessComponent((Sickness.SicknessComponent)new CommonSickEffectSickness());

            this.AddSicknessComponent((Sickness.SicknessComponent)new AttributeModifierSickness(new AttributeModifier[12]
            {
                new AttributeModifier("StressDelta", stressPerSecond, (string) STRINGS.DISEASES.ALIENSICKNESS.NAME),

                new AttributeModifier(Db.Get().Attributes.Athletics.Id, 5f, (string) STRINGS.DISEASES.ALIENSICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Strength.Id, 5f, (string) STRINGS.DISEASES.ALIENSICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Digging.Id, 5f, (string) STRINGS.DISEASES.ALIENSICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Construction.Id, 5f, (string) STRINGS.DISEASES.ALIENSICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Art.Id, 5f, (string) STRINGS.DISEASES.ALIENSICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Caring.Id, 5f, (string) STRINGS.DISEASES.ALIENSICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Learning.Id, 5f, (string) STRINGS.DISEASES.ALIENSICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Machinery.Id, 5f, (string) STRINGS.DISEASES.ALIENSICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Cooking.Id, 5f, (string) STRINGS.DISEASES.ALIENSICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Botanist.Id, 5f, (string) STRINGS.DISEASES.ALIENSICKNESS.NAME),
                new AttributeModifier(Db.Get().Attributes.Ranching.Id, 5f, (string) STRINGS.DISEASES.ALIENSICKNESS.NAME)
            }));
        }
    }
}
