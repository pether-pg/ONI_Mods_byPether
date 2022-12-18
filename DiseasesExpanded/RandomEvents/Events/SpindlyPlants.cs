using System;
using Klei.AI;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class SpindlyPlants : RandomDiseaseEvent
    {
        public SpindlyPlants(int weight = 1)
        {
            ID = nameof(SpindlyPlants);
            GeneralName = "Spindly Plants";
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.Small;

            Condition = new Func<object, bool>(data => true);

            Event = new Action<object>(
                data =>
                {
                    foreach(Harvestable harvestable in Components.Harvestables)
                    {
                        Effects effects = harvestable.gameObject.GetComponent<Effects>();
                        if (effects == null)
                            continue;

                        Effect effect = Db.Get().effects.Get(DiseasesExpanded_Patches_Spindly.SPINDLY_PLANTS_EFFECT_ID);
                        effects.Add(effect, true);
                    }

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, "Our plants have developed protective spindles.");
                });
        }
    }
}
