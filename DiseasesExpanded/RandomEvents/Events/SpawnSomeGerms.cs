using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class SpawnSomeGerms : DiseaseEvent
    {

        public SpawnSomeGerms(byte germIdx, int weight = 1)
        {
            ID = nameof(SpawnSomeGerms);
            GeneralName = "Spawn Some Germs";
            NameDetails = "";
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.None;
            
            Event = new Action<object>(
                data => 
                {
                    foreach (Telepad pod in Components.Telepads)
                        SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(pod.gameObject), germIdx, 1000000);
                }
            );
        }
    }
}
