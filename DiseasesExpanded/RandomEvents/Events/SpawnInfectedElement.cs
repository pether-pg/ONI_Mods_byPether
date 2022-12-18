using System;
using System.Collections.Generic;
using UnityEngine;
using ONITwitchLib.Utils;

namespace DiseasesExpanded.RandomEvents.Events
{
    class SpawnInfectedElement : RandomDiseaseEvent
    {
        private static readonly CellElementEvent SpawnEvent = new CellElementEvent("RandomlySpawnedElement", "Spawned by Random Event", true);

        public SpawnInfectedElement(byte germIdx, int weight = 1)
        {
            GeneralName = "Spawn Infected Element";
            NameDetails = Db.Get().Diseases[germIdx].Id;
            ID = GenerateId(nameof(SpawnInfectedElement), NameDetails);
            DangerLevel = Helpers.EstimateGermDanger(germIdx);
            AppearanceWeight = weight;

            Condition = new Func<object, bool>(data => GameClock.Instance.GetCycle() > (int)DangerLevel * 100);

            Event = new Action<object>(
                data =>
                {
                    int numberOfCells = 30;
                    int withCavityClearance = GridUtil.FindCellWithCavityClearance(PosUtil.RandomCellNearMouse());
                    HashSet<int> intSet = GridUtil.FloodCollectCells(withCavityClearance, (cell => (Grid.BuildMasks[cell] & (Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation)) == ~Grid.BuildFlags.Any), numberOfCells);
                    SimHashes spawnedElementHash = GetElementHash(germIdx);
                    Element element = ElementLoader.FindElementByHash(spawnedElementHash);
                    float temp = GetTemperature(germIdx);
                    float mass = element.IsGas ? 5 : 1000;

                    foreach (int gameCell in intSet)
                        SimMessages.ReplaceAndDisplaceElement(gameCell, spawnedElementHash, SpawnEvent, mass, temp, germIdx, 100000);

                    ONITwitchLib.ToastManager.InstantiateToastWithPosTarget(GeneralName, $"Spawned some {element.name} filled with germs.", Grid.CellToPos(withCavityClearance));
                });
        }

        public float GetTemperature(byte germIdx)
        {
            float degC = 273.15f;

            if (germIdx == GermIdx.FrostShardsIdx)
                return -30 + degC;
            else if (germIdx == GermIdx.MutatingGermsIdx)
                return 18 + degC;
            else if (germIdx == GermIdx.RadiationPoisoningIdx)
                return 50 + degC;
            else if (germIdx == GermIdx.ZombieSporesIdx)
                return 100 + degC;
            else if (germIdx == GermIdx.AlienGermsIdx)
                return 200 + degC;

            return 20 + degC;
        }

        public SimHashes GetElementHash(byte germIdx)
        {
            List<SimHashes> possibles = new List<SimHashes>();
            
            if(germIdx == GermIdx.PollenGermsIdx)
                possibles.Add(SimHashes.Oxygen);
            else if(germIdx == GermIdx.FoodPoisoningIdx)
                possibles.Add(SimHashes.DirtyWater);
            else if(germIdx == GermIdx.SlimelungIdx)
                possibles.Add(SimHashes.ContaminatedOxygen);
            else if(germIdx == GermIdx.ZombieSporesIdx)
            {
                possibles.Add(SimHashes.Oxygen);
                possibles.Add(SimHashes.CarbonDioxide);
            }
            else if(germIdx == GermIdx.RadiationPoisoningIdx)
            {
                possibles.Add(SimHashes.Oxygen);
                possibles.Add(SimHashes.Fallout);
            }
            else if(germIdx == GermIdx.BogInsectsIdx)
            {
                possibles.Add(SimHashes.Oxygen);
                possibles.Add(SimHashes.ContaminatedOxygen);
            }
            else if(germIdx == GermIdx.FrostShardsIdx)
            {
                possibles.Add(SimHashes.Hydrogen);
                possibles.Add(SimHashes.Oxygen);
                possibles.Add(SimHashes.CarbonDioxide);
            }
            else if(germIdx == GermIdx.GassyGermsIdx)
            {
                possibles.Add(SimHashes.Oxygen);
                possibles.Add(SimHashes.Methane);
                possibles.Add(SimHashes.Chlorine);
            }
            else if(germIdx == GermIdx.HungerGermsIdx)
            {
                possibles.Add(SimHashes.Oxygen);
                possibles.Add(SimHashes.ContaminatedOxygen);
            }
            else if(germIdx == GermIdx.MutatingGermsIdx)
            {
                possibles.Add(SimHashes.Oxygen);
                possibles.Add(SimHashes.ContaminatedOxygen);
            }
            else if(germIdx == GermIdx.AlienGermsIdx)
            {
                possibles.Add(SimHashes.Steam);
                possibles.Add(SimHashes.CarbonDioxide);
            }

            if (possibles.Count == 0)
                possibles.Add(SimHashes.Oxygen);

            possibles.Shuffle();
            return possibles[0];
        }
    }
}
