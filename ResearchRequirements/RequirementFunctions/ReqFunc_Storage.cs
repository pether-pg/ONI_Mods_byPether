using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace ResearchRequirements
{
    class ReqFunc_Storage
    {
        private static Dictionary<Tag, float> StoredGases = new Dictionary<Tag, float>();
        private static Dictionary<Tag, float> StoredLiquids = new Dictionary<Tag, float>();
        private static float TotalHEPs;

        public static void InitalizeDictionaries()
        {
            TotalHEPs = 0;
            StoredGases = new Dictionary<Tag, float>();
            StoredLiquids = new Dictionary<Tag, float>();

            if (!TechRequirements.Instance.GetGameTech("Plastics").IsComplete())
                StoredLiquids.Add(ElementLoader.FindElementByHash(SimHashes.CrudeOil).tag, 0);
            if (!TechRequirements.Instance.GetGameTech("ValveMiniaturization").IsComplete())
                StoredLiquids.Add(ElementLoader.FindElementByHash(SimHashes.Petroleum).tag, 0);
            if (!TechRequirements.Instance.GetGameTech("LiquidFiltering").IsComplete())
                StoredLiquids.Add(ElementLoader.FindElementByHash(SimHashes.SaltWater).tag, 0);
            if (!TechRequirements.Instance.GetGameTech("LiquidFiltering").IsComplete())
                StoredLiquids.Add(ElementLoader.FindElementByHash(SimHashes.Brine).tag, 0);
            if (!TechRequirements.Instance.GetGameTech("Distillation").IsComplete())
                StoredLiquids.Add(ElementLoader.FindElementByHash(SimHashes.DirtyWater).tag, 0);

            if (!TechRequirements.Instance.GetGameTech("Catalytics").IsComplete())
                StoredGases.Add(ElementLoader.FindElementByHash(SimHashes.CarbonDioxide).tag, 0);
            if (!TechRequirements.Instance.GetGameTech("RenewableEnergy").IsComplete())
                StoredGases.Add(ElementLoader.FindElementByHash(SimHashes.Steam).tag, 0);

            //DLC Techs:
            if (DlcManager.IsContentSubscribed(DlcManager.EXPANSION1_ID) && !TechRequirements.Instance.GetGameTech("CryoFuelPropulsion").IsComplete())
                StoredLiquids.Add(ElementLoader.FindElementByHash(SimHashes.LiquidHydrogen).tag, 0);
            if (DlcManager.IsContentSubscribed(DlcManager.EXPANSION1_ID) && !TechRequirements.Instance.GetGameTech("HydrocarbonPropulsion").IsComplete())
                StoredLiquids.Add(ElementLoader.FindElementByHash(SimHashes.LiquidOxygen).tag, 0);
        }

        private static void PopulateDictionary(BuildingComplete building, ref Dictionary<Tag, float> dictionary)
        {
            Reservoir res = building.gameObject.GetComponent<Reservoir>();
            if (res != null)
            {
                Storage storage = Traverse.Create(res).Field("storage").GetValue<Storage>();
                if (storage != null)
                {
                    List<Tag> keys = dictionary.Keys.ToList();
                    foreach (Tag key in keys)
                        dictionary[key] += storage.GetMassAvailable(key);
                }
            }
        }

        private static void AddTotalHEPs(BuildingComplete building)
        {
            HighEnergyParticleStorage storage = building.gameObject.GetComponent<HighEnergyParticleStorage>();
            if (storage != null)
                TotalHEPs += storage.Particles;
        }

        public static void CountResourcesInReservoirs()
        {
            InitalizeDictionaries();

            foreach (BuildingComplete building in Components.BuildingCompletes)
            {
                if (building.gameObject.name == "GasReservoirComplete" && StoredGases.Keys.Count > 0)
                    PopulateDictionary(building, ref StoredGases);
                else if (building.gameObject.name == "LiquidReservoirComplete" && StoredLiquids.Keys.Count > 0)
                    PopulateDictionary(building, ref StoredLiquids);
                else if (building.gameObject.name == "HEPBatteryComplete")
                    AddTotalHEPs(building);
            }
        }

        public static float StoredGas(SimHashes hash)
        {
            Tag tag = ElementLoader.FindElementByHash(hash).tag;

            if (!StoredGases.ContainsKey(tag))
            {
                Debug.Log($"Research Requirements: Stored tag not tracked: {tag}");
                return 0;
            }
            return StoredGases[tag];
        }

        public static float StoredLiquid(SimHashes hash)
        {
            Tag tag = ElementLoader.FindElementByHash(hash).tag;

            if (!StoredLiquids.ContainsKey(tag))
            {
                Debug.Log($"Research Requirements: Stored tag not tracked: {tag}");
                return 0;
            }
            return StoredLiquids[tag];
        }

        public static float StoredHEPs()
        {
            return TotalHEPs;
        }

        public static float Resources(SimHashes hash)
        {
            Tag tag = ElementLoader.FindElementByHash(hash).tag;
            return Resources(tag);
        }

        public static float Resources(Tag tag)
        {
            if (ClusterManager.Instance.GetAllWorldsAccessibleAmounts().ContainsKey(tag))
                return ClusterManager.Instance.GetAllWorldsAccessibleAmounts()[tag];
            return 0;
        }

    }
}
