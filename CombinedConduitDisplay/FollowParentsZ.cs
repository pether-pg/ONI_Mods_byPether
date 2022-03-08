using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinedConduitDisplay
{
    class FollowParentsZ : KMonoBehaviour
    {
        public void Update()
        {
            if(transform.hasChanged)
            {
                KanimRefresh.TryMeterRefresh_LogicCounter(this.gameObject);
                transform.hasChanged = false;
            }
        }
    }
}
