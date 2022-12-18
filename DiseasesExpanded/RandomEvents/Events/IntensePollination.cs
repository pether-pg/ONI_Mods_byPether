using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class IntensePollination : RandomDiseaseEvent
    {
        public IntensePollination(int weight = 1)
        {
            ID = nameof(IntensePollination);
            GeneralName = "Intense Pollination";
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.Small;

            Condition = new Func<object, bool>(data => true);

            Event = new Action<object>(
                data =>
                {
                    int scale = 100;
                    
                    foreach (DiseaseDropper.Instance inst in DiseasesExpanded_Patches_Twitch.DiseaseDropperInstance_Initialize_Patch.DiseaseDroppers)
                        if (inst != null && inst.gameObject != null && inst.gameObject.HasTag(GameTags.Plant))
                        {
                            int count = inst.def.singleEmitQuantity * scale;
                            SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(inst.gameObject), inst.def.diseaseIdx, count);
                        }

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, "All of the plants released increased amount of pollen.");
                });
        }
    }
}
