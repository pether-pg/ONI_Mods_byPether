using System;
using System.Collections.Generic;
using System.Linq;
using KSerialization;

namespace DiseasesExpanded
{
    [SerializationConfig(MemberSerialization.OptIn)]
    class ShieldData : KMonoBehaviour, ISaveLoadable
    {
        private static ShieldData _instance;

        public static ShieldData Instance
        {
            get
            {
                if (_instance == null)
                    _instance = SaveGame.Instance.GetComponent<ShieldData>();
                return _instance;
            }
        }

        // called in Game_DestroyInstances_Patch
        public static void Clear()
        {
            _instance = null;
            Debug.Log($"{ModInfo.Namespace}: ShieldData._instance cleared.");
        }

        public const float SHIELD_PER_GENERATOR_PER_UPDATE = 1.0f;
        public const float SHIELD_NEGATIVE_PER_UPDATE = -3 * SHIELD_PER_GENERATOR_PER_UPDATE;

        [Serialize]
        private float lastUpdateCycle = 0;

        [Serialize]
        private Dictionary<int, float> ShieldStatuses = new Dictionary<int, float>();

        private List<ShieldGenerator.SMInstance> ExistingGenerators;

        public int GetShieldPercent(int worldId)
        {
            return (int)(100 * GetShieldStatus(worldId));
        }

        public float GetRadiationScale(int worldId)
        {
            return 1.0f - GetShieldStatus(worldId);
        }

        public float GetShieldStatus(int worldId)
        {
            if (ShieldStatuses != null && ShieldStatuses.ContainsKey(worldId))
                return ShieldStatuses[worldId];
            return 0;
        }

        public void UpdateShieldStaus()
        {
            float dt = CyclePercentSinceLastUpdate();
            if (dt == 0)
                return;

            if (ShieldStatuses == null)
                ShieldStatuses = new Dictionary<int, float>();

            Dictionary<int, float> deltas = GetWorldsDeltas(dt);
            foreach (int worldId in deltas.Keys)
            {
                if (!ShieldStatuses.ContainsKey(worldId))
                    ShieldStatuses.Add(worldId, 0);
                ShieldStatuses[worldId] += deltas[worldId];
            }

            List<int> keys = ShieldStatuses.Keys.ToList();
            for(int i=0; i<keys.Count; i++)
            {
                int worldId = keys[i];
                if (!deltas.ContainsKey(worldId) || deltas[worldId] <= 0)
                    ShieldStatuses[worldId] += SHIELD_NEGATIVE_PER_UPDATE * dt;

                ShieldStatuses[worldId] = UnityEngine.Mathf.Clamp01(ShieldStatuses[worldId]);
            }
        }

        private Dictionary<int, float> GetWorldsDeltas(float dt)
        {
            Dictionary<int, float> delta = new Dictionary<int, float>();

            if(ExistingGenerators != null)
                foreach (ShieldGenerator.SMInstance shield in ExistingGenerators)
                {
                    if (shield == null || shield.gameObject == null)
                        continue;

                    int worldId = shield.gameObject.GetMyWorldId();
                    if (!delta.ContainsKey(worldId))
                        delta.Add(worldId, 0);

                    if (shield.CanWork())
                        delta[worldId] += SHIELD_PER_GENERATOR_PER_UPDATE * dt;
                }

            return delta;
        }

        float CyclePercentSinceLastUpdate()
        {
            float delta = 0;
            float time = GameClock.Instance.GetTimeInCycles();
            if (lastUpdateCycle != 0)
                delta = time - lastUpdateCycle;
            lastUpdateCycle = time;
            return delta;
        }

        public void Add(ShieldGenerator.SMInstance generatorInstance)
        {
            if (ExistingGenerators == null)
                ExistingGenerators = new List<ShieldGenerator.SMInstance>();

            ExistingGenerators.Add(generatorInstance);
        }

        public void Remove (ShieldGenerator.SMInstance generatorInstance)
        {
            if (ExistingGenerators != null && ExistingGenerators.Contains(generatorInstance))
                ExistingGenerators.Remove(generatorInstance);
        }
    }
}
