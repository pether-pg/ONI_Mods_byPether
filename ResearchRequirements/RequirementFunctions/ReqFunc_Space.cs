using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRequirements
{
    class ReqFunc_Space
    {
        public static int AnalysedPlanets()
        {
            int count = 0;
            foreach (SpaceDestination destination in SpacecraftManager.instance.destinations)
                if (destination.AnalysisState() == SpacecraftManager.DestinationAnalysisState.Complete)
                    count++;
            return count;
        }

        public static int SurfaceBreached()
        {
            return Game.Instance.savedInfo.discoveredSurface ? 1 : 0;
        }

        // DLC specific functions

        public static int WorldsWithBeds()
        {
            if (!DlcManager.IsContentSubscribed(DlcManager.EXPANSION1_ID))
                return 1;

            List<int> UniqueWorldIds = new List<int>();
            foreach (int worldID in GetAllWorldIds())
                foreach (Sleepable sleepable in Components.NormalBeds.WorldItemsEnumerate(worldID, true))
                    if (!UniqueWorldIds.Contains(sleepable.GetMyWorldId()) && !sleepable.GetMyWorld().IsModuleInterior)
                        UniqueWorldIds.Add(sleepable.GetMyWorldId());

            foreach (int i in UniqueWorldIds)
                Debug.Log($"Unique sleepable world: {i}");

            return UniqueWorldIds.Count;
        }

        public static int RevealedSpaceHexes(int radiusMin, int radiusMax)
        {
            if (!DlcManager.IsContentSubscribed(DlcManager.EXPANSION1_ID))
                return 0;

            int count = 0;
            ClusterFogOfWarManager.Instance smi = SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>();
            if (smi == null)
                return 0;

            List<AxialI> ColonisedWorlds = new List<AxialI>();
            foreach (int worldID in GetAllWorldIds())
                foreach (Sleepable sleepable in Components.NormalBeds.WorldItemsEnumerate(worldID, true))
                    if (!ColonisedWorlds.Contains(sleepable.GetMyWorldLocation()))
                    ColonisedWorlds.Add(sleepable.GetMyWorldLocation());

            List<AxialI> HexesMinRad = new List<AxialI>();
            List<AxialI> HexesMaxRad = new List<AxialI>();
            List<AxialI> SearchedHexes = new List<AxialI>();

            foreach (AxialI world in ColonisedWorlds)
            {
                foreach (AxialI hex in AxialUtil.GetAllPointsWithinRadius(world, radiusMin))
                    if (!HexesMinRad.Contains(hex))
                        HexesMinRad.Add(hex);

                foreach (AxialI hex in AxialUtil.GetAllPointsWithinRadius(world, radiusMax))
                    if (!HexesMaxRad.Contains(hex))
                        HexesMaxRad.Add(hex);
            }
            foreach (AxialI hex in HexesMaxRad)
                if (!HexesMinRad.Contains(hex))
                    SearchedHexes.Add(hex);

            foreach (AxialI hex in SearchedHexes)
                if (smi.IsLocationRevealed(hex))
                    count++;

            return count;
        }

        public static int MaxColonyDistance()
        {
            int max = 0;
            foreach (Telepad telepad1 in Components.Telepads)
                foreach (Telepad telepad2 in Components.Telepads)
                {
                    int distance = AxialUtil.GetDistance(telepad1.GetMyWorldLocation(), telepad2.GetMyWorldLocation());
                    if (distance > max)
                        max = distance;
                }

            return max;
        }

        public static List<int> GetAllWorldIds()
        {
            return ClusterManager.Instance.GetWorldIDsSorted();
        }
    }
}
