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
        private const int MAX_RETRIES = 500;

        public SproutFlowers(string flowerId, int weight = 1)
        {
            GeneralName = STRINGS.RANDOM_EVENTS.SPROUT_FLOWERS.NAME;
            NameDetails = flowerId;
            ID = GenerateId(nameof(SproutFlowers), NameDetails);
            Group = nameof(SproutFlowers);
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
                        minCycle = (int)DangerLevel * 100;

                    return GameClock.Instance.GetCycle() > minCycle;
                });

            Event = new Action<object>(
                data =>
                {
                    List<int> cells = GridUtil.ActiveSimCells().Where(i => Grid.IsVisible(i)).ToList();
                    int numberOfSpawns = 1;

                    for(int i = 0; i<numberOfSpawns; i++)
                    {
                        int cell = FindCell(cells, flowerId, MAX_RETRIES);
                        if(cell == Grid.InvalidCell)
                        {
                            ONITwitchLib.ToastManager.InstantiateToast(GeneralName, STRINGS.RANDOM_EVENTS.SPROUT_FLOWERS.TOAST_FAILED);
                            continue;
                        }

                        Vector3 pos = (Grid.CellToPos(cell) + Grid.CellToPos(Grid.CellRight(cell))) / 2;
                        GameObject go = GameUtil.KInstantiate(Assets.GetPrefab(flowerId), pos, Grid.SceneLayer.BuildingFront);
                        go.SetActive(true);

                        ONITwitchLib.ToastManager.InstantiateToastWithGoTarget(GeneralName, STRINGS.RANDOM_EVENTS.SPROUT_FLOWERS.TOAST, go);
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

        private int FindCell(List<int> cells, string flowerId, int retries)
        {
            //Debug.Log($"FindCell: flowerId = {flowerId}, retries = {retries}");
            if (retries <= 0 && !string.IsNullOrEmpty(flowerId))
                return FindCell(cells, string.Empty, MAX_RETRIES);
            else if (retries <= 0)
                return Grid.InvalidCell;

            int cellIdx = UnityEngine.Random.Range(0, cells.Count);
            int randomCell = cells[cellIdx];
            int cell = GridUtil.NearestEmptyCell(randomCell);

            if (!Grid.IsValidCell(cell))
                return FindCell(cells, flowerId, retries - 1);

            int worldId = Grid.WorldIdx[cell];
            while (!Grid.IsSolidCell(Grid.CellBelow(cell)))
                cell = Grid.CellBelow(cell);

            if (!TestCell(cell, worldId))
                return FindCell(cells, flowerId, retries - 1);

            if (!string.IsNullOrEmpty(flowerId) && !TestCellForFlower(flowerId, cell))
                return FindCell(cells, flowerId, retries - 1);

            return cell;
        }

        private bool TestCellForFlower(string flowerId, int cell)
        {
            string seedId = string.Empty;
            switch(flowerId)
            {
                case PrickleFlowerConfig.ID:
                    seedId = PrickleFlowerConfig.SEED_ID;
                    break;
                case BulbPlantConfig.ID:
                    seedId = BulbPlantConfig.SEED_ID;
                    break;
                case EvilFlowerConfig.ID:
                    seedId = EvilFlowerConfig.SEED_ID;
                    break;
                case ColdBreatherConfig.ID:
                    seedId = ColdBreatherConfig.SEED_ID;
                    break;
                case GasGrassConfig.ID:
                    seedId = GasGrassConfig.SEED_ID;
                    break;
                case SwampHarvestPlantConfig.ID:
                    seedId = SwampHarvestPlantConfig.SEED_ID;
                    break;
                case CritterTrapPlantConfig.ID:
                    seedId = "CritterTrapPlantSeed";
                    break;
            }
            //Debug.Log($"{ModInfo.Namespace}: SproutFlowers: TestCellForFlower - string.IsNullOrEmpty(seedId)");
            if (string.IsNullOrEmpty(seedId))
                return false;

            //Debug.Log($"{ModInfo.Namespace}: SproutFlowers: TestCellForFlower - PlantableSeed == null");
            PlantableSeed seed = Assets.GetPrefab(seedId)?.GetComponent<PlantableSeed>();
            if (seed == null)
                return false;

            bool result = seed.TestSuitableGround(cell);
            //Debug.Log($"{ModInfo.Namespace}: SproutFlowers: TestCellForFlower - seed.TestSuitableGround(cell) = {result}");
            return result;
        }

        private bool TestCell(int cell, int worldId)
        {
            /*Debug.Log($"{ModInfo.Namespace}: SproutFlowers: TestCell - cell = {cell}");
            Debug.Log($"{ModInfo.Namespace}: SproutFlowers: TestCell - Grid.IsValidCell(cell) = {Grid.IsValidCell(cell)}");
            Debug.Log($"{ModInfo.Namespace}: SproutFlowers: TestCell - Grid.Revealed[cell] = {Grid.Revealed[cell]}");
            Debug.Log($"{ModInfo.Namespace}: SproutFlowers: TestCell - Grid.IsVisible(cell) = {Grid.IsVisible(cell)}");
            Debug.Log($"{ModInfo.Namespace}: SproutFlowers: TestCell - Grid.WorldIdx[cell] = {Grid.WorldIdx[cell]} ?= {worldId}");
            Debug.Log($"{ModInfo.Namespace}: SproutFlowers: TestCell - Grid.WorldIdx[Grid.CellRight(cell)] = {Grid.WorldIdx[Grid.CellRight(cell)]} ?= {worldId}");
            Debug.Log($"{ModInfo.Namespace}: SproutFlowers: TestCell - Grid.IsSolidCell(Grid.CellAbove(cell)) = {Grid.IsSolidCell(Grid.CellAbove(cell))}");*/

            if (!Grid.IsValidCell(cell))
                return false;
            if (!Grid.IsVisible(cell))
                return false;
            if (worldId != Grid.WorldIdx[cell])
                return false;
            if (worldId != Grid.WorldIdx[Grid.CellRight(cell)])
                return false;
            if (Grid.IsSolidCell(Grid.CellAbove(cell)))
                return false;

            return true;
        }
    }
}
