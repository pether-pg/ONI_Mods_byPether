using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.EntityScripts
{
    class DisinfectOwnLocation : KMonoBehaviour, ISim200ms
    {
        public void Sim200ms(float dt)
        {
            int cell = Grid.PosToCell(this.gameObject.transform.position);

            int radius = 3;
            DisinfectCells(cell, radius);
            DisinfectSceneEntries<Pickupable>(cell, radius, GameScenePartitioner.Instance.pickupablesLayer);
            DisinfectSceneEntries<BuildingComplete>(cell, radius, GameScenePartitioner.Instance.completeBuildings);
        }

        public void DisinfectCells(int startingCell, int radius)
        {
            if (!Grid.IsValidCell(startingCell))
                return;

            int x0, y0;
            Grid.CellToXY(startingCell, out x0, out y0);

            for (int x = x0 - radius; x < x0 + radius; x++)
                for (int y = y0 - radius; y < y0 + radius; y++)
                {
                    if (x < 0 || y < 0)
                        continue;

                    if (x > Grid.WidthInCells || y > Grid.HeightInCells)
                        continue;

                    int cell = Grid.XYToCell(x, y);
                    if (!Grid.IsValidCell(cell))
                        continue;

                    SimMessages.ConsumeDisease(cell, 1.0f, int.MaxValue, 0);
                }
        }

        public void DisinfectSceneEntries<T>(int cell, int radius, ScenePartitionerLayer layer) where T : KMonoBehaviour
        {
            var entries = ListPool<ScenePartitionerEntry, DisinfectOwnLocation>.Allocate();
            GameScenePartitioner.Instance.GatherEntries(new Extents(cell, radius), layer, entries);

            foreach (var entry in entries)
            {
                if (entry.obj is T kmonobeh)
                {
                    PrimaryElement prime = kmonobeh.GetComponent<PrimaryElement>();
                    if (prime == null)
                        continue;

                    prime.AddDisease(prime.DiseaseIdx, -prime.DiseaseCount, "Purified by DisinfectOwnLocation");
                }
            }

            entries.Recycle();
        }
    }
}
