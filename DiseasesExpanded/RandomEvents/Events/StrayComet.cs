using System;
using System.Collections.Generic;
using DiseasesExpanded.RandomEvents;
using UnityEngine;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class StrayComet : RandomDiseaseEvent
    {
        public StrayComet(bool isMoo = false, int weight = 1)
        {
            GeneralName = "Stray Comet";
            NameDetails = isMoo ? GassyMooCometConfig.ID : "AlienComet";
            ID = GenerateId(nameof(StrayComet), NameDetails);
            AppearanceWeight = weight;
            DangerLevel = Helpers.EstimateGermDanger(isMoo ? GermIdx.GassyGermsIdx : GermIdx.AlienGermsIdx);

            Condition = new Func<object, bool>(
                data => 
                {
                    if (!isMoo && GameClock.Instance.GetCycle() < 0)
                        return false;
                    return Game.Instance.savedInfo.discoveredSurface; 
                });

            Event = new Action<object>(
                data =>
                {
                    int worldId = ClusterManager.Instance.activeWorldId;
                    string cometId = GassyMooCometConfig.ID;
                    if(!isMoo)
                    {
                        List<string> possibles = new List<string>()
                        {
                            DustCometConfig.ID,
                            CopperCometConfig.ID,
                            FullereneCometConfig.ID,
                            GoldCometConfig.ID,
                            IronCometConfig.ID,
                            RockCometConfig.ID
                        };
                        possibles.Shuffle();
                        cometId = possibles[0];
                    }

                    SpawnBombard(worldId, cometId);
                });
        }

        private GameObject SpawnBombard(int worldID, string prefab)
        {
            WorldContainer world = ClusterManager.Instance.GetWorld(worldID);
            Vector3 position = new Vector3(world.Width * UnityEngine.Random.value + world.WorldOffset.x, (world.Height + world.WorldOffset.y - 1), Grid.GetLayerZ(Grid.SceneLayer.FXFront));
            GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(prefab), position, Quaternion.identity);
            gameObject.SetActive(true);
            return gameObject;
        }
    }
}
