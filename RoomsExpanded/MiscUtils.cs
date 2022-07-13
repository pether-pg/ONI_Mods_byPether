using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsExpanded
{
    class MiscUtils
    {
        public static string Percent(float p)
        {
            int i = (int)(100 * p);
            return $"{i}%";
        }
    }
}
