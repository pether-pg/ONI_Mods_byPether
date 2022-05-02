using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinedConduitDisplay
{
    class FollowParentsZ : KMonoBehaviour
    {
        public FollowZTarget Target = FollowZTarget.All;

        public void Update()
        {
            if(transform.hasChanged)
            {
                if(Target == FollowZTarget.LogicCounter || Target == FollowZTarget.All)
                    KanimRefresh.TryMeterRefresh_LogicCounter(this.gameObject);
                if (Target == FollowZTarget.LogicFilter || Target == FollowZTarget.All)
                    KanimRefresh.TryMeterRefresh_LogicFilter(this.gameObject);
                if (Target == FollowZTarget.LogicBuffer || Target == FollowZTarget.All)
                    KanimRefresh.TryMeterRefresh_LogicBuffer(this.gameObject);
                transform.hasChanged = false;
            }
        }
    }

    enum FollowZTarget
    {
        LogicCounter = 1,
        LogicFilter = 2,
        LogicBuffer = 4,
        All = 0xFF
    }
}
