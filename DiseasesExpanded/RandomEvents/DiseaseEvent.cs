using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents
{
    class DiseaseEvent
    {
        public string ID;
        public string GeneralName;
        public string NameDetails;
        public int AppearanceWeight;
        public Action<object> Event;
        public ONITwitchLib.Danger DangerLevel;

        public string GetFriednlyName()
        {
            return GeneralName;
        }
    }
}
