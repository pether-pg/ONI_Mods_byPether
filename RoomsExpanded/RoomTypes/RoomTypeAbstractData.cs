using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomsExpanded
{
    public abstract class RoomTypeAbstractData
    {
        public string Id;
        public string Name;
        public string Tooltip;
        public string Effect;
        public RoomTypeCategory Catergory;
        public RoomConstraints.Constraint ConstraintPrimary;
        public RoomConstraints.Constraint[] ConstrantsAdditional;

        public RoomDetails.Detail[] RoomDetails;

        public int Priority;
        public RoomType[] Upgrades;
        public bool SingleAssignee;
        public bool PriorityUse;
        public string[] Effects;
        public int SortKey;

        protected RoomTypeCategory CreateCategory()
        {
            string categoryId = string.Format("{0}Category", Id);
            return new RoomTypeCategory(categoryId, "", Id);
        }

        public RoomType GetRoomType()
        {
            return new RoomType(this.Id,
                                this.Name,
                                this.Tooltip,
                                this.Effect,
                                this.Catergory,
                                this.ConstraintPrimary,
                                this.ConstrantsAdditional,
                                this.RoomDetails,
                                this.Priority,
                                this.Upgrades,
                                this.SingleAssignee,
                                this.PriorityUse,
                                this.Effects,
                                this.SortKey);
        }
    }
}
