using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomsExpanded
{
    public static class SortingCounter
    {
        public static readonly int PlumbedBathroomSortKey = 2;
        public static readonly int BedroomSortKey = 4;
        public static readonly int FarmSortKey = 11;
        public static readonly int CreaturePenSortKey = 12;
        public static readonly int RecreationRoom = 14;
        public static readonly int ParkSortKey = 15;

        private static int value = 0;

        // non-modded RoomTypes has last room type with sorting key = 16
        public static void Init(int startingValue = 16)
        {
            value = startingValue;
        }

        public static int GetAndIncrement(int forced = -1)
        {
            value++;
            if (forced != -1)
                return forced;
            return value;
        }
    }
}
