using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterplanarInfrastructure
{
    class WorldBorderChecker
    {
        public static bool IsInTopOfTheWorld(int cell, int buildingHeight = 1)
        {
            if (!Grid.IsWorldValidCell(cell))
                return false;

            byte worldIdx = Grid.WorldIdx[cell];
            WorldContainer world = ClusterManager.Instance.GetWorld(worldIdx);
            if (world == null)
                return false;

            int currentY = Grid.CellToXY(cell).y;
            int requiredY = world.WorldOffset.y + world.WorldSize.y - buildingHeight - 2;

            return currentY == requiredY;
        }

        public static int TilesToTheTop(int cell, int buildingHeight = 1)
        {
            if (!Grid.IsWorldValidCell(cell))
                return -1;

            byte worldIdx = Grid.WorldIdx[cell];
            WorldContainer world = ClusterManager.Instance.GetWorld(worldIdx);
            if (world == null)
                return -1;

            int currentY = Grid.CellToXY(cell).y;
            int requiredY = world.WorldOffset.y + world.WorldSize.y - buildingHeight - 2;

            return requiredY - currentY;
        }
    }
}
