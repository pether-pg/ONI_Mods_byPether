using TUNING;
using UnityEngine;

namespace MultiplayerStorage
{
    class SharedStorageConfig : IBuildingConfig
    {
        public const string ID = nameof(SharedStorageConfig);
        public const string statusItemRebootId = "RebootRequired";
        public const string statusItemOccupiedId = "StorageOccupied";

        public override BuildingDef CreateBuildingDef()
        {
            float[] construction_mass = new float[3] { 10000f, 10000f, 10000f };
            string[] construction_materials = new string[3]
            {
              SimHashes.Steel.ToString(),
              SimHashes.Diamond.ToString(),
              SimHashes.Polypropylene.ToString()
            };
            EffectorValues none = NOISE_POLLUTION.NONE;
            EffectorValues tieR1 = BUILDINGS.DECOR.PENALTY.TIER1;
            EffectorValues noise = none;
            // multiverse_gate_kanim artwork is done by Ronivan, great thanks!
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, 6, 5, "multiverse_gate_kanim", 1000, 480f, construction_mass, construction_materials, 1600f, BuildLocationRule.OnFloor, tieR1, noise);
            buildingDef.Floodable = false;
            buildingDef.AudioCategory = "Metal";
            buildingDef.Overheatable = false;
            buildingDef.OnePerWorld = true;
            buildingDef.ShowInBuildMenu = true;
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            SoundEventVolumeCache.instance.AddVolume("storagelocker_kanim", "StorageLocker_Hit_metallic_low", NOISE_POLLUTION.NOISY.TIER1);
            Prioritizable.AddRef(go);
            Storage storage = go.AddOrGet<Storage>();
            storage.showInUI = true;
            storage.allowItemRemoval = true;
            storage.showDescriptor = true;
            storage.storageFilters = STORAGEFILTERS.NOT_EDIBLE_SOLIDS;
            storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
            storage.showCapacityStatusItem = true;
            storage.showCapacityAsMainStatus = true;
            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.StorageLocker;
            go.AddOrGet<StorageLocker>();
            go.AddOrGet<UserNameable>();
            go.AddOrGet<SharedStorage>();
            go.AddOrGet<Operational>();
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGetDef<SharedStorageController.Def>();
        }
    }
}
