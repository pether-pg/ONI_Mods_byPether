using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiseasesExpanded.RandomEvents.Events
{
    class ResupplyFirstAidKits : RandomDiseaseEvent
    {
        public ResupplyFirstAidKits(int weight = 1)
        {
            ID = nameof(ResupplyFirstAidKits);
            GeneralName = STRINGS.RANDOM_EVENTS.RESUPPLY_FIRST_AID_KIT.NAME;
            DangerLevel = ONITwitchLib.Danger.None;
            AppearanceWeight = weight;

            Condition = new Func<object, bool>(data => true);

            Event = new Action<object>(
                data =>
                {
                    int copies = 1;

                    List<GameObject> spawningLocations = new List<GameObject>();
                    spawningLocations.Add(Components.Telepads[0].gameObject);

                    for(int i=0; i<copies; i++)
                    {
                        int randIdx = UnityEngine.Random.Range(0, spawningLocations.Count);
                        GameObject spawnAt = spawningLocations[randIdx];

                        GameObject kit = GameUtil.KInstantiate(Assets.GetPrefab(Configs.RandomFirstAidKitConfig.ID), spawnAt.transform.position, Grid.SceneLayer.BuildingFront);
                        kit.SetActive(true);
                        ONITwitchLib.ToastManager.InstantiateToastWithGoTarget(GeneralName, STRINGS.RANDOM_EVENTS.RESUPPLY_FIRST_AID_KIT.TOAST, kit);
                    }
                });
        }
    }
}
