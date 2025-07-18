using System;
using UnityEngine;

namespace DiseasesExpanded
{
    class NanobotForgeConfig : IBuildingConfig
    {
        public const string ID = "NanobotForge";
        private HashedString[] dupeInteractAnims;

        public override BuildingDef CreateBuildingDef()
        {
            float[] tieR5 = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER6;
            string[] steelOnly = new string[1] { SimHashes.Steel.ToString() };
            EffectorValues tieR6 = TUNING.NOISE_POLLUTION.NOISY.TIER6;
            EffectorValues tieR2 = TUNING.BUILDINGS.DECOR.PENALTY.TIER2;
            EffectorValues noise = tieR6;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, 4, 5, "nanobot_forge_tmp_kanim", 30, 480f, tieR5, steelOnly, 2400f, BuildLocationRule.OnFloor, tieR2, noise);
            buildingDef.RequiresPowerInput = true;
            buildingDef.EnergyConsumptionWhenActive = 1600f;
            buildingDef.SelfHeatKilowattsWhenActive = 16f;
            buildingDef.ViewMode = OverlayModes.Power.ID;
            buildingDef.AudioCategory = "HollowMetal";
            buildingDef.AudioSize = "large";
            buildingDef.RequiredSkillPerkID = Db.Get().SkillPerks.CanAdvancedMedicine.Id;
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.AddOrGet<DropAllWorkable>();
            go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
            ComplexFabricator fabricator = go.AddOrGet<ComplexFabricator>();
            fabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
            fabricator.duplicantOperated = true;
            go.AddOrGet<FabricatorIngredientStatusManager>();
            go.AddOrGet<CopyBuildingSettings>();
            go.AddOrGet<ComplexFabricatorWorkable>();
            BuildingTemplates.CreateComplexFabricatorStorage(go, fabricator);
            Prioritizable.AddRef(go);
        }

        public override void DoPostConfigureComplete(GameObject go) => go.GetComponent<KPrefabID>().prefabSpawnFn += (KPrefabID.PrefabFn)(game_object =>
        {
            ComplexFabricatorWorkable component = game_object.GetComponent<ComplexFabricatorWorkable>();
            component.WorkerStatusItem = Db.Get().DuplicantStatusItems.Processing;
            component.AttributeConverter = Db.Get().AttributeConverters.DoctorSpeed;
            component.AttributeExperienceMultiplier = TUNING.DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
            component.SkillExperienceSkillGroup = Db.Get().SkillGroups.MedicalAid.Id;
            component.SkillExperienceMultiplier = TUNING.SKILLS.PART_DAY_EXPERIENCE;
            component.requiredSkillPerk = Db.Get().SkillPerks.CanAdvancedMedicine.Id;
            KAnimFile anim = Assets.GetAnim((HashedString)"anim_interacts_supermaterial_refinery_kanim");
            KAnimFile[] kanimFileArray = new KAnimFile[1] { anim };
            component.overrideAnims = kanimFileArray;
            component.workAnims = new HashedString[2]
            {
                (HashedString) "working_pre",
                (HashedString) "working_loop"
            };
            component.synchronizeAnims = false;
            KAnimFileData data = anim.GetData();
            int animCount = data.animCount;
            this.dupeInteractAnims = new HashedString[animCount - 2];
            int index1 = 0;
            int index2 = 0;
            for (; index1 < animCount; ++index1)
            {
                HashedString name = (HashedString)data.GetAnim(index1).name;
                if (name != (HashedString)"working_pre" && name != (HashedString)"working_pst")
                {
                    this.dupeInteractAnims[index2] = name;
                    ++index2;
                }
            }
            component.GetDupeInteract = (Func<HashedString[]>)(() => new HashedString[2]
            {
                (HashedString) "working_loop",
                this.dupeInteractAnims.GetRandom<HashedString>()
            });
        });
    }
}
