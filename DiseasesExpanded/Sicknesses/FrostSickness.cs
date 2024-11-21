﻿using STRINGS;
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
            float scale = Settings.Instance.FrostPox.SeverityScale;
            this.AddSicknessComponent((Sickness.SicknessComponent)new AttributeModifierSickness(new AttributeModifier[2]
            {
                new AttributeModifier("ThermalConductivityBarrier", -0.004f * scale, (string) STRINGS.DISEASES.FROSTSICKNESS.NAME),
                new AttributeModifier("GermResistance", -3f * scale, (string) STRINGS.DISEASES.FROSTSICKNESS.NAME)
            }));
            this.AddSicknessComponent(new SicknessExpressionEffect(ModdedExpressions.FrostyExpression));
        }
    }
}
