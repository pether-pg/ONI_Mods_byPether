using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents
{
    class RandomDiseaseEvent
    {
        public string ID;
        public string GeneralName;
        public string NameDetails;
        public int AppearanceWeight;
        public Func<object, bool> Condition;
        public Action<object> Event;
        public ONITwitchLib.Danger DangerLevel;

        protected string GenerateId(string baseId, object details)
        {
            if (details == null)
                return baseId;
            return string.Format("{0}_{1}", baseId, details.ToString());
        }

        public string GetFriednlyName(bool friendly = true)
        {
            if(!friendly)
                return GeneralName;

            if (!string.IsNullOrEmpty(NameDetails))
                return string.Format("{0} ({1})", GeneralName, NameDetails);

            return GeneralName;
        }
    }
}
