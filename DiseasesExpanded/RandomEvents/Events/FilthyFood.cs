using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class FilthyFood : RandomDiseaseEvent
    {
        public FilthyFood(int weight = 1)
        {
            ID = nameof(FilthyFood);
            GeneralName = "Filthy Food";
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.Small;

            Condition = new Func<object, bool>(data => true);

            Event = new Action<object>(
                data =>
                {
                    foreach(Edible edible in Components.Edibles)
                    {
                        PrimaryElement prime = edible.GetComponent<PrimaryElement>();
                        if (prime != null)
                            prime.AddDisease(GermIdx.FoodPoisoningIdx, 100000, GeneralName);
                    }
                });
        }
    }
}
