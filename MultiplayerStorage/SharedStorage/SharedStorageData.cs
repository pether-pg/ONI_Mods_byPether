using UnityEngine;
using System.Collections.Generic;

namespace MultiplayerStorage
{
    class SharedStorageData
    {
        private const float operationalCapacity = 200000;

        private static SharedStorageData _instance = null;
        public static SharedStorageData Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SharedStorageData();
                return _instance;
            }
            set { _instance = value; }
        }

        public GameObject GO;
        public GameObject UnderConstruction;

        public bool IsActive
        {
            get
            {
                if (Instance.GO == null)
                    return false;
                Operational operational = Instance.GO.GetComponent<Operational>();
                if (operational == null)
                    return false;
                return operational.IsActive;
            }
        }

        public bool IsAlreadyBuilt
        {
            get
            {
                return Instance.GO != null || Instance.UnderConstruction != null;
            }
        }

        public static Storage GetStorage()
        {
            if (Instance.GO == null)
                return null;
            return Instance.GO.GetComponent<Storage>();
        }

        public static void ClearItems()
        {
            Storage storage = GetStorage();
            if (storage == null || storage.items == null)
                return;

            foreach (GameObject go in storage.items)
                go.DeleteObject();
            storage.items.Clear();
        }

        public static void ItemsListInit()
        {
            Storage storage = GetStorage();
            if (storage == null || storage.items == null)
                return;

            if (storage.items == null)
                storage.items = new List<GameObject>();
        }

        public static void SetStorageCapacity(float capacity)
        {
            Storage storage = GetStorage();
            if (storage != null)
                storage.capacityKg = capacity;
        }

        public static void SetActive(bool active)
        {
            Operational operational = Instance.GO.GetComponent<Operational>();
            operational.SetFlag(new Operational.Flag("SharedStorageActive", Operational.Flag.Type.Functional), active);
            operational.SetActive(active);
            if (active)
                SetStorageCapacity(operationalCapacity);
            else
                SetStorageCapacity(0);
        }
    }
}
