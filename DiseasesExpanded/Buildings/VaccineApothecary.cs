using TUNING;
using UnityEngine;

namespace DiseasesExpanded
{
    class VaccineApothecary : ComplexFabricator
    {
        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();
            this.choreType = Db.Get().ChoreTypes.Compound;
            this.fetchChoreTypeIdHash = Db.Get().ChoreTypes.DoctorFetch.IdHash;
            this.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            this.workable.WorkerStatusItem = Db.Get().DuplicantStatusItems.Fabricating;
            this.workable.AttributeConverter = Db.Get().AttributeConverters.CompoundingSpeed;
            this.workable.SkillExperienceSkillGroup = Db.Get().SkillGroups.MedicalAid.Id;
            this.workable.SkillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
            this.workable.requiredSkillPerk = Db.Get().SkillPerks.CanAdvancedMedicine.Id;
            this.workable.overrideAnims = new KAnimFile[1]
            {
                Assets.GetAnim((HashedString) "anim_interacts_metalrefinery_kanim")
            };
        }
    }

}
