using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ResearchRequirements
{
    class ReqFunc_Dupes
    {
        public static int LoudDupes()
        {
            int count = 0;
            foreach (MinionResume resume in Components.MinionResumes)
            {
                if (resume.gameObject.GetComponent<Snorer>() != null
                    || resume.gameObject.GetComponent<Flatulence>() != null)
                    count++;
            }
            return count;
        }

        public static bool HasEffect(GameObject duplicant, string effectName)
        {
            if (duplicant == null)
                return false;

            Klei.AI.Effects AIeffects = duplicant.GetComponent<Klei.AI.Effects>();
            if (AIeffects == null)
                return false;
            if (AIeffects.HasEffect(effectName))
                return true;

            return false;
        }


        public static int DuplicantsWithEffect(string effectName)
        {
            int count = 0;
            foreach (MinionIdentity identity in Components.MinionIdentities)
            {
                GameObject go = identity.gameObject;
                if (HasEffect(go, effectName))
                    count++;
            }
            return count;
        }

        public static float PercentOfDupesWithEffect(string effectName)
        {
            int count = DuplicantsWithEffect(effectName);
            return 100.0f * count / Components.MinionIdentities.Count;
        }

        public static int SickDuplicants(string diseaseId)
        {
            int count = 0;
            foreach (MinionIdentity identity in Components.MinionIdentities)
            {
                MinionModifiers modifiers = identity.GetComponent<MinionModifiers>();
                if (modifiers == null || modifiers.sicknesses == null)
                    continue;

                foreach (Klei.AI.SicknessInstance sickness in modifiers.sicknesses.ModifierList)
                {
                    if (sickness.Sickness.Id == diseaseId)
                    {
                        count++;
                        break;
                    }
                }
            }

            return count;
        }

        public static int SickDuplicants()
        {
            int count = 0;
            foreach (MinionIdentity identity in Components.MinionIdentities)
            {
                MinionModifiers modifiers = identity.GetComponent<MinionModifiers>();
                if (modifiers == null || modifiers.sicknesses == null)
                    continue;

                if(HasEffect(identity.gameObject, "RadiationExposureMinor") 
                    || HasEffect(identity.gameObject, "RadiationExposureMajor") 
                    || HasEffect(identity.gameObject, "RadiationExposureExtreme"))
                {
                    count++;
                    continue;
                }

                if (modifiers.sicknesses.Count > 0)
                    count++;
            }
            return count;
        }

        public static float MaximumAttribute(string attributeName)
        {
            float max = 0;
            foreach (MinionIdentity identity in Components.MinionIdentities)
            {
                MinionModifiers modifiers = identity.GetComponent<MinionModifiers>();
                if (modifiers == null)
                    continue;
                Klei.AI.AttributeInstance attributeInstance = modifiers.attributes.AttributeTable.Where(p => p.Name == attributeName).FirstOrDefault();
                if (attributeInstance == null)
                {
                    continue;
                }

                float value = attributeInstance.GetTotalValue();
                if (value > max)
                    max = value;
            }
            return max;
        }

        public static int DuplicantsWithInterest(string skillId)
        {
            int count = 0;
            foreach (MinionResume resume in Components.MinionResumes)
                if (resume.HasSkillAptitude(Db.Get().Skills.Get(skillId)))
                    count++;
            return count;
        }

        public static int DuplicantsWithSkill(string skillId)
        {
            int count = 0;
            foreach (MinionResume resume in Components.MinionResumes)
                if (resume.HasMasteredSkill(skillId))
                    count++;
            return count;
        }

        public static int DuplicantsWithTrait(string traitId)
        {
            int count = 0;
            foreach (MinionResume resume in Components.MinionResumes)
            {
                Klei.AI.Traits traits = resume.gameObject.GetComponent<Klei.AI.Traits>();
                if (traits != null)
                {
                    if (traits.HasTrait(traitId))
                        count++;
                }
            }
            return count;
        }

        public static int DuplicantsWithOneOfTraits(List<string> traitIds)
        {
            int count = 0;
            foreach (MinionResume resume in Components.MinionResumes)
            {
                Klei.AI.Traits traits = resume.gameObject.GetComponent<Klei.AI.Traits>();
                if (traits != null)
                {
                    foreach(string traitId in traitIds)
                        if (traits.HasTrait(traitId))
                        {
                            count++;
                            break;
                        }
                }
            }
            return count;
        }

        public static int PilotWithTrait(string traitId)
        {
            int count = 0;
            foreach (MinionResume resume in Components.MinionResumes)
                if (resume.HasMasteredSkill("RocketPiloting1"))
                {
                    Klei.AI.Traits traits = resume.gameObject.GetComponent<Klei.AI.Traits>();
                    if (traits != null)
                    {
                        if (traits.HasTrait(traitId))
                            count++;
                    }
                }
            return count;
        }


        public static int DuplicantsWithMorale(int morale)
        {
            int count = 0;
            foreach (MinionIdentity identity in Components.MinionIdentities)
            {
                Klei.AI.AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLife.Lookup((Component)identity.gameObject.GetComponent<MinionModifiers>());
                if (attributeInstance == null)
                    continue;

                float value = attributeInstance.GetTotalValue();
                if (value >= morale)
                    count++;
            }
            return count;
        }
    }
}
