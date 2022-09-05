using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterplanarInfrastructure
{
    class DysonSphere : KMonoBehaviour, ISim1000ms
    {
        private static DysonSphere _instance = null;

        public static DysonSphere Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DysonSphere();
                return _instance;
            }
        }

        public int SateliteCount { get; set; }

        public const float MaxRadiation = 100 * 1000;
        public const float MaxSunlight = 150 * 1000;
        public const int MaxSize = 500; // assumption for calculations sake. number of satelites can be greather though
        public const int INVALID_PATH_LENGTH = -1;

        public static readonly SortedDictionary<int, string> StageDescriptions = new SortedDictionary<int, string>()
        {
            { 10, "Dyson Satelites" },
            { 50, "Dyson Swarm" },
            { 100, "Dyson Ring" },
            { 250, "Dyson Cage" },
            { MaxSize, "Dyson Sphere" }
        };

        private List<AxialI> m_cachedPath;
        private LaserBeam laserBeam;

        [MyCmpAdd]
        private ClusterDestinationSelector destinationSelector;

        public int PathLength()
        {
            if (this.m_cachedPath == null)
                this.UpdatePath();
            if (this.m_cachedPath == null)
                return INVALID_PATH_LENGTH;
            int count = this.m_cachedPath.Count;
            return count;
        }

        public void Sim1000ms(float dt)
        {
            Debug.Log("Dyson Sphere 1000ms");
            UpdateBeam();
        }

        public bool IsDestinationReachable(bool forceRefresh = false)
        {
            if (forceRefresh)
                this.UpdatePath();
            return this.destinationSelector.GetDestinationWorld() != this.gameObject.GetMyWorldId() && this.PathLength() != INVALID_PATH_LENGTH;
        }

        public void UpdatePath()
        { 
            this.m_cachedPath = ClusterGrid.Instance.GetPath(this.gameObject.GetMyWorldLocation(), this.destinationSelector.GetDestination(), this.destinationSelector); 
        }

        public int GetStageKey()
        {
            int smallestGreater = MaxSize;
            foreach (int key in StageDescriptions.Keys)
                if (key > SateliteCount && key < smallestGreater)
                    smallestGreater = key;

            return smallestGreater;
        }

        public string GetFullStageDesc()
        {
            int key = GetStageKey();
            string progress = SateliteCount.ToString() + (key == MaxSize ? "" : " / " + key.ToString());
            return $"{StageDescriptions[key]}: {progress}";
        }

        public float CurrentProgress => 1.0f * SateliteCount / MaxSize;

        public float RadiationIncrease => CurrentProgress * MaxRadiation;

        public float SunlightIncrease => CurrentProgress * MaxSunlight;

        public void DeploySatelite(int count = 1)
        {
            SateliteCount += count;
            UpdateBeam();
        }

        public void UpdateBeam()
        {
            UpdatePath();
            if(laserBeam != null)
                laserBeam.ClearAllRadiations();
            LaserBeam newBeam = new LaserBeam(m_cachedPath, (int)RadiationIncrease, (int)SunlightIncrease);
            laserBeam = newBeam;
            laserBeam.ModifyRadiationOfPath();
        }
    }
}
