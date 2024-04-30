using UnityEngine;
using KSerialization;
using System.Collections.Generic;
using Klei.AI;

namespace DietVariety
{
    [SerializationConfig(MemberSerialization.OptIn)]
    class PastMealsEaten : KMonoBehaviour
    {
        public const int MAX_COST = 20;

        [Serialize]
        public Dictionary<int, Dictionary<string, int>> TimeSinceAte;

        private Dictionary<GameObject, int> DuplicantIdsCache;

        private static PastMealsEaten _instance;

        public static PastMealsEaten Instance
        {
            get
            {
                if (_instance == null)
                    _instance = SaveGame.Instance.GetComponent<PastMealsEaten>();
                return _instance;
            }
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
        }

        // called in Game_DestroyInstances_Patch
        public static void Clear()
        {
            _instance = null;
            Debug.Log($"{ModInfo.Namespace}: PastMealsEaten._instance cleared.");
        }

        public int GetVarietyCost(GameObject go, string foodId)
        {
            EnsureDuplicantTracked(go);

            int id = GetGameObjectId(go);
            if (!TimeSinceAte.ContainsKey(id) || !TimeSinceAte[id].ContainsKey(foodId))
                return 0;

            return Mathf.Min(MAX_COST, TimeSinceAte[id][foodId]);
        }

        public void RegisterNewMeal(GameObject go, string foodId)
        {
            EnsureDuplicantTracked(go);

            int id = GetGameObjectId(go);
            if (!TimeSinceAte[id].ContainsKey(foodId))
                TimeSinceAte[id].Add(foodId, 0);
            else TimeSinceAte[id][foodId] = 0;

            List<string> keysToIncrease = new List<string>();
            foreach (string key in TimeSinceAte[id].Keys)
                keysToIncrease.Add(key);
            foreach (string key in keysToIncrease)
                if (TimeSinceAte[id][key] <= Settings.Instance.MaxMealsCounted)
                    TimeSinceAte[id][key] += 1;
        }

        public void StopTrackingDeadDupe(GameObject deadDupe)
        {
            if (DuplicantIdsCache == null)
                return;

            if (DuplicantIdsCache.ContainsKey(deadDupe))
                DuplicantIdsCache.Remove(deadDupe);
        }

        public int GetUniqueMealsCount(GameObject go)
        {
            EnsureDuplicantTracked(go);

            int count = 0;
            int id = GetGameObjectId(go);
            foreach (int value in TimeSinceAte[id].Values)
                if (value < Settings.Instance.MaxMealsCounted)
                    count++;

            return count;
        }

        private void EnsureDuplicantTracked(GameObject go)
        {
            if (TimeSinceAte == null)
                TimeSinceAte = new Dictionary<int, Dictionary<string, int>>();

            int id = GetGameObjectId(go);
            if (!TimeSinceAte.ContainsKey(id))
                TimeSinceAte.Add(id, new Dictionary<string, int>());

            if (TimeSinceAte[id] == null)
                TimeSinceAte[id] = new Dictionary<string, int>();
        }

        private int GetGameObjectId(GameObject go)
        {
            if (DuplicantIdsCache == null)
                DuplicantIdsCache = new Dictionary<GameObject, int>();

            if (!DuplicantIdsCache.ContainsKey(go))
                DuplicantIdsCache.Add(go, go.GetComponent<KPrefabID>().InstanceID);

            return DuplicantIdsCache[go];
        }
    }
}
