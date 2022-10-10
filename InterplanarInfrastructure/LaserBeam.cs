using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterplanarInfrastructure
{
    class LaserBeam
    {
        private List<AxialI> Path = null;
        private int RadiationDelta = 0;
        private int IlluminationDelta = 0;

        private Dictionary<WorldContainer, int> ModifiedSpaceRadiations = null;
        private Dictionary<WorldContainer, int> ModifiedSpaceIllumination = null;
        private string note = TodoList.Todo.Note("Illumination was never used since I failed to make Dyson Sphere");
        private string notf = TodoList.Todo.Note("If Dyson Sphere worked, WorldContainser.sunlight should be modified and later cleared to default");

        public LaserBeam(List<AxialI> axials, int radiation = 0, int illumination =0)
        {
            Path = axials;
            RadiationDelta = radiation;
            IlluminationDelta = illumination;
        }

        public List<WorldContainer> GetAffectedWorlds()
        {
            List<WorldContainer> result = new List<WorldContainer>();

            if(Path != null)
                foreach(AxialI location in Path)
                    foreach (ClusterGridEntity clusterGridEntity in ClusterGrid.Instance.cellContents[location])
                    {
                        WorldContainer world = clusterGridEntity.GetComponent<WorldContainer>();
                        if (world != null)
                            result.Add(world);
                    }

            return result;
        }

        public void ClearAllRadiations()
        {
            if (ModifiedSpaceRadiations == null)
                return;

            List<WorldContainer> modifiedWorlds = ModifiedSpaceRadiations.Keys.ToList();
            foreach (WorldContainer world in modifiedWorlds)
                ClearRadiationIncrease(world);
        }

        public void ClearRadiationIncrease(WorldContainer world)
        {
            if (ModifiedSpaceRadiations == null || !ModifiedSpaceRadiations.ContainsKey(world))
                return;

            if (world.cosmicRadiation > ModifiedSpaceRadiations[world])
            {
                world.cosmicRadiation -= ModifiedSpaceRadiations[world];
                world.sunlight -= 100 * ModifiedSpaceRadiations[world];
            }

            ModifiedSpaceRadiations.Remove(world);
        }

        private void StoreModificationInfo(WorldContainer world, int radiation)
        {
            if (ModifiedSpaceRadiations == null)
                ModifiedSpaceRadiations = new Dictionary<WorldContainer, int>();
            if (!ModifiedSpaceRadiations.ContainsKey(world))
                ModifiedSpaceRadiations.Add(world, 0);

            ModifiedSpaceRadiations[world] += radiation;
        }

        public void ModifyRadiationAtWorld(WorldContainer world)
        {
            if (world != null)
            {
                world.cosmicRadiation += RadiationDelta;
                world.sunlight += 100 * RadiationDelta;
                StoreModificationInfo(world, RadiationDelta);
            }
        }

        public void ModifyRadiationAtAxialI(AxialI location)
        {
            if (location == null)
                return;

            foreach (ClusterGridEntity clusterGridEntity in ClusterGrid.Instance.cellContents[location])
            {
                WorldContainer world = clusterGridEntity.GetComponent<WorldContainer>();
                ModifyRadiationAtWorld(world);
            }
        }

        public void ModifyRadiationOfPath()
        {
            if (this.Path == null)
                return;

            foreach (AxialI axial in this.Path)
                ModifyRadiationAtAxialI(axial);
        }
    }
}
