using System;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class BloomingGraves : RandomDiseaseEvent
    {
        public BloomingGraves(int weight = 1)
        {
            ID = nameof(BloomingGraves);
            GeneralName = "Blooming Graves";
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.Medium;

            Condition = new Func<object, bool>(
                data => 
                {
                    foreach (Grave grave in Components.Graves)
                        if (!string.IsNullOrEmpty(grave.graveName))
                            return true;
                    return false; 
                });

            Event = new Action<object>(
                data =>
                {
                    foreach (Grave grave in Components.Graves)
                    if(!string.IsNullOrEmpty(grave.graveName))
                        {
                            GameObject go = GameUtil.KInstantiate(Assets.GetPrefab(EvilFlowerConfig.ID), grave.transform.position, Grid.SceneLayer.Creatures);
                            go.SetActive(true);
                        }
                }
            );

        }
    }
}
