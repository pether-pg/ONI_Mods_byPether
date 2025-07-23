using System.Collections.Generic;
using UnityEngine;
using Klei.AI;
using HarmonyLib;

namespace DiseasesExpanded
{
    class RustSickness_1 : Sickness
    {
        public const float DURATION = 3 * 600;
        public const string ID = nameof(RustSickness_1);
        public const string RECOVERY_ID = "RustSicknessRecovery";
        public const string WATTAGE_MODIFIER_ID = "RustSickness_1_WattageModifier";

        private const float stageScale = 2;

        public static BionicBatteryMonitor.WattageModifier GetWattageModifier()
        {
            return new BionicBatteryMonitor.WattageModifier(WATTAGE_MODIFIER_ID, WATTAGE_MODIFIER_ID, 10000, 10000);
        }

        public RustSickness_1()
            : base(ID, SicknessType.Pathogen, Severity.Minor, 0.00025f, new List<InfectionVector>() { InfectionVector.Inhalation }, DURATION, RECOVERY_ID)
        {
            this.AddSicknessComponent(new CommonSickEffectSickness());
            this.AddSicknessComponent(new AnimatedSickness(new HashedString[1]
            {
                (HashedString) "anim_idle_sick_kanim"
            }, Db.Get().Expressions.Sick));

            this.AddSicknessComponent((Sickness.SicknessComponent)new AttributeModifierSickness(new AttributeModifier[4]
            {
                new AttributeModifier(Db.Get().Attributes.AirConsumptionRate.Id, RustSickness_0.YUCKY_LUNGGS_VALUE * stageScale, (string) STRINGS.DISEASES.RUST_SICKNESS_1.NAME),
                new AttributeModifier(Db.Get().Attributes.BionicBatteryCountCapacity.Id, RustSickness_0.BASE_BATTERIES_VALUE * stageScale, (string) STRINGS.DISEASES.RUST_SICKNESS_1.NAME),
                new AttributeModifier(Db.Get().Attributes.Learning.Id, RustSickness_0.BASE_ATTRIBUTE_VALUE * stageScale, (string) STRINGS.DISEASES.RUST_SICKNESS_1.NAME), // Blame Aki
                new AttributeModifier("BionicOilDelta", RustSickness_0.BASE_OIL_VALUE * stageScale, (string) STRINGS.DISEASES.RUST_SICKNESS_1.NAME) // Blame Pink Hoodie
                // For extra line that adds nothing to the code - blame Rex (Th3Fanbus) who wanted to be included
            }));
            this.AddSicknessComponent(new PeriodicEmoteSickness(Db.Get().Emotes.Minion.Sick, 50f));
            this.AddSicknessComponent(new RustSickness_1_Component());
        }

        public class RustSickness_1_Component : SicknessComponent
        {
            public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
            {
                BionicBatteryMonitor.Instance battery = go.GetSMI<BionicBatteryMonitor.Instance>();
                if(battery != null)
                {
                    Traverse.Create(battery).Method("OnBatteryCapacityChanged").GetValue();
                }

                RustSicknessHistory rsh = go.GetComponent<RustSicknessHistory>();
                if (rsh != null)
                {
                    rsh.InfectionsOnStages[1]++;
                    rsh.LastFateRoll = Random.Range(0, 100);
                }

                return diseaseInstance;
            }

            public override void OnCure(GameObject go, object instance_data)
            {
                BionicBatteryMonitor.Instance battery = go.GetSMI<BionicBatteryMonitor.Instance>();
                if (battery != null)
                {
                    Traverse.Create(battery).Method("OnBatteryCapacityChanged").GetValue();
                }

                // Do not progress for non-Bionics
                RustSicknessHistory rsh = go.GetComponent<RustSicknessHistory>();
                if (rsh == null)
                    return;

                // Do not progress sickness if it was cured before reaching 100% of duration
                SicknessInstance si = (SicknessInstance)instance_data;
                if (si.GetPercentCured() < 1)
                {
                    rsh.CuresOnStages[1]++;
                    return;
                }

                float advChance = rsh.GetDeathChance();
                int fateRoll = rsh.LastFateRoll;

                Debug.Log($"{ModInfo.Namespace}: RustSickness_1 on {go.name}: advChance = {advChance}, roll = {fateRoll}" + (fateRoll > advChance ? "" : " (HIT!)"));
                if (fateRoll > advChance)
                    return;

                SicknessHelper.Infect(go, RustSickness_2.ID, STRINGS.DISEASES.RUST_SICKNESS_2.EXPOSURE_INFO);
            }

            public override List<Descriptor> GetSymptoms(GameObject victim)
            {
                float chance = 0;
                RustSicknessHistory rsh = victim.GetComponent<RustSicknessHistory>();
                if (rsh != null)
                    chance = rsh.GetChanceForStage(3);

                return new List<Descriptor>()
                {
                    new Descriptor( STRINGS.SYMPTHOMS.DEVELOPMENT_CHANCE.NAME.Replace("{CHANCE}", chance.ToString()).Replace("{SICKNESS}", STRINGS.DISEASES.RUST_SICKNESS_2.NAME),
                                    STRINGS.SYMPTHOMS.DEVELOPMENT_CHANCE.TOOLTIP.Replace("{CHANCE}", chance.ToString()).Replace("{SICKNESS}", STRINGS.DISEASES.RUST_SICKNESS_2.NAME),
                                    Descriptor.DescriptorType.Symptom)
                };
            }
        }
    }
}
