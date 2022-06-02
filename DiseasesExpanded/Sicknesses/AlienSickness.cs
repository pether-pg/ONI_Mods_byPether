using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    class AlienSickness : Sickness
    {
        public const string ID = nameof(AlienSickness);
        public const string RECOVERY_ID = "AlienSicknessRecovery";

        public static readonly float stressPerSecond = (25 / 600.0f) * (Settings.Instance.RebalanceForDiseasesRestored ? 2 : 1);

        public AlienSickness()
            : base(ID, Sickness.SicknessType.Pathogen, Sickness.Severity.Minor, 0.00025f, new List<Sickness.InfectionVector>()
            {
                Sickness.InfectionVector.Inhalation,
                Sickness.InfectionVector.Contact,
                Sickness.InfectionVector.Exposure,
            }, 3000f, RECOVERY_ID)
        {
            this.AddSicknessComponent((Sickness.SicknessComponent)new CommonSickEffectSickness());

            this.AddSicknessComponent((Sickness.SicknessComponent)new AttributeModifierSickness(new AttributeModifier[12]
            {
                new AttributeModifier("StressDelta", stressPerSecond, (string) STRINGS.DISEASES.ALIENSYMBIOT.NAME),

                new AttributeModifier(Db.Get().Attributes.Athletics.Id, 5f, (string) STRINGS.DISEASES.ALIENSYMBIOT.NAME),
                new AttributeModifier(Db.Get().Attributes.Strength.Id, 5f, (string) STRINGS.DISEASES.ALIENSYMBIOT.NAME),
                new AttributeModifier(Db.Get().Attributes.Digging.Id, 5f, (string) STRINGS.DISEASES.ALIENSYMBIOT.NAME),
                new AttributeModifier(Db.Get().Attributes.Construction.Id, 5f, (string) STRINGS.DISEASES.ALIENSYMBIOT.NAME),
                new AttributeModifier(Db.Get().Attributes.Art.Id, 5f, (string) STRINGS.DISEASES.ALIENSYMBIOT.NAME),
                new AttributeModifier(Db.Get().Attributes.Caring.Id, 5f, (string) STRINGS.DISEASES.ALIENSYMBIOT.NAME),
                new AttributeModifier(Db.Get().Attributes.Learning.Id, 5f, (string) STRINGS.DISEASES.ALIENSYMBIOT.NAME),
                new AttributeModifier(Db.Get().Attributes.Machinery.Id, 5f, (string) STRINGS.DISEASES.ALIENSYMBIOT.NAME),
                new AttributeModifier(Db.Get().Attributes.Cooking.Id, 5f, (string) STRINGS.DISEASES.ALIENSYMBIOT.NAME),
                new AttributeModifier(Db.Get().Attributes.Botanist.Id, 5f, (string) STRINGS.DISEASES.ALIENSYMBIOT.NAME),
                new AttributeModifier(Db.Get().Attributes.Ranching.Id, 5f, (string) STRINGS.DISEASES.ALIENSYMBIOT.NAME)

            }));
        }
    }
}
