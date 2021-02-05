using System;
using STRINGS;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomTypeAgriculturalData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "CreaturePen";
        public static readonly string AgriculturalStation = "AgriculturalStation";


        public static RoomConstraints.Constraint MODIFIED_CONSTRAINT = new RoomConstraints.Constraint(
                                                                            (Func<KPrefabID, bool>)(bc => bc.HasTag(RoomConstraints.ConstraintTags.RanchStation)
                                                                                                || bc.HasTag(RoomConstraints.ConstraintTags.FarmStation)),
                                                                                                (Func<Room, bool>)null,
                                                                                                name: STRINGS.ROOMS.CRITERIA.AGRICULTURAL.NAME,
                                                                                                description: STRINGS.ROOMS.CRITERIA.AGRICULTURAL.DESCRIPTION,
                                                                                                stomp_in_conflict: new List<RoomConstraints.Constraint>() { RoomConstraints.FARM_STATION });

    }
}
