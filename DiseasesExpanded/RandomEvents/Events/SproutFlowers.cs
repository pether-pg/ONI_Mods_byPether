using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using ONITwitchLib.Utils;

namespace DiseasesExpanded.RandomEvents.Events
{
    class SproutFlowers : RandomDiseaseEvent
    {
        public static Dictionary<string, byte> GermyFlowers;

        public SproutFlowers(string flowerId, int weight = 1)
        {
            GeneralName = "Sprout Flowers";
            NameDetails = flowerId;
            ID = GenerateId(nameof(SproutFlowers), NameDetails);
            AppearanceWeight = weight;
            DangerLevel = SupportedFlowers().Contains(flowerId) ? Helpers.EstimateGermDanger(GermyFlowers[flowerId]) : ONITwitchLib.Danger.None;

            Condition = new Func<object, bool>(
                data => 
                {
                    if (!SupportedFlowers().Contains(flowerId))
                        return false;

                    int minCycle = 0;
                    if (flowerId == GasGrassConfig.ID)
                        minCycle = 500;
                    else
                        minCycle = (int)DangerLevel * 50;

                    return GameClock.Instance.GetCycle() > minCycle;
                });

            Event = new Action<object>(
                data =>
                {
                    List<int> cells = GridUtil.ActiveSimCells().ToList();
                    int numberOfSpawns = 1;
                    int possibleRetries = 100;

                    for(int i = 0; i<numberOfSpawns; i++)
                    {
                        int cellIdx = UnityEngine.Random.Range(0, cells.Count);
                        int randomCell = cells[cellIdx];
                        int cell = GridUtil.NearestEmptyCell(randomCell);
                        int worldId = Grid.WorldIdx[cell];
                        while (!Grid.IsSolidCell(Grid.CellBelow(cell)))
                            cell = Grid.CellBelow(cell);

                        if((Grid.IsSolidCell(Grid.CellAbove(cell)) || !Grid.IsValidCell(cell) || worldId != Grid.WorldIdx[cell])
                            && possibleRetries > 0)
                        {
                            i--;
                            possibleRetries--;
                            if (possibleRetries < 0)
                                break;

                            continue;
                        }

                        Vector3 pos = (Grid.CellToPos(cell) + Grid.CellToPos(Grid.CellRight(cell))) / 2;
                        GameObject go = GameUtil.KInstantiate(Assets.GetPrefab(flowerId), pos, Grid.SceneLayer.Creatures);
                        go.SetActive(true);

                        ONITwitchLib.ToastManager.InstantiateToastWithGoTarget(GeneralName, "New flower just sprouted!", go);
                    }

                });

        }

        public static List<string> SupportedFlowers()
        {
            List<string> result = new List<string>();
            if (GermyFlowers == null || GermyFlowers.Keys.Count == 0)
                InitalizeDict();

            foreach (string key in GermyFlowers.Keys)
                result.Add(key);

            return result;
        }

        private static void InitalizeDict()
        {
            GermyFlowers = new Dictionary<string, byte>();
            GermyFlowers.Add(PrickleFlowerConfig.ID, GermIdx.PollenGermsIdx);
            GermyFlowers.Add(BulbPlantConfig.ID, GermIdx.PollenGermsIdx);
            GermyFlowers.Add(EvilFlowerConfig.ID, GermIdx.ZombieSporesIdx);
            GermyFlowers.Add(ColdBreatherConfig.ID, GermIdx.FrostShardsIdx);
            GermyFlowers.Add(GasGrassConfig.ID, GermIdx.GassyGermsIdx);

            if (!DlcManager.IsExpansion1Active())
                return;

            GermyFlowers.Add(SwampHarvestPlantConfig.ID, GermIdx.BogInsectsIdx);
            GermyFlowers.Add(CritterTrapPlantConfig.ID, GermIdx.HungerGermsIdx);

        }
    }
}
