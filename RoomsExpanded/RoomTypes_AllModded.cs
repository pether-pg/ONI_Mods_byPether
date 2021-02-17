using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomsExpanded
{
    class RoomTypes_AllModded
    {
        private static RoomType prv_Lab = null;

        public static RoomType LaboratoryRoom
        {
            get
            {
                if (prv_Lab == null)
                    prv_Lab = new RoomTypeLaboratoryData().GetRoomType();
                return prv_Lab;
            }
        }

        private static RoomType prv_Kitchen = null;

        public static RoomType KitchenRoom
        {
            get
            {
                if (prv_Kitchen == null)
                    prv_Kitchen = new RoomTypeKitchenData().GetRoomType();
                return prv_Kitchen;
            }
        }

        private static RoomType prv_Bathroom = null;
        public static RoomType BathroomRoom
        {
            get
            {
                if (prv_Bathroom == null)
                    prv_Bathroom = new RoomTypeBathroomData().GetRoomType();
                return prv_Bathroom;
            }
        }

        private static RoomType prv_Graveyard = null;

        public static RoomType GraveyardRoom
        {
            get
            {
                if (prv_Graveyard == null)
                    prv_Graveyard = new RoomTypeGraveyardData().GetRoomType();
                return prv_Graveyard;
            }
        }

        public static RoomType prv_gym = null;

        public static RoomType GymRoom
        {
            get
            {
                if (prv_gym == null)
                    prv_gym = new RoomTypeGymData().GetRoomType();
                return prv_gym;
            }
        }
        public static RoomType prv_nursery = null;

        public static RoomType Nursery
        {
            get
            {
                if (prv_nursery == null)
                    prv_nursery = new RoomTypeNurseryData().GetRoomType();
                return prv_nursery;
            }
        }

        private static RoomType prv_Aquarium = null;
        public static RoomType Aquarium
        {
            get
            {
                if (prv_Aquarium == null)
                    prv_Aquarium = new RoomTypeAquariumData().GetRoomType();
                return prv_Aquarium;
            }
        }

        private static RoomType prv_Botanical = null;
        public static RoomType Botanical
        {
            get
            {
                if (prv_Botanical == null)
                    prv_Botanical = new RoomTypeBotanicalData().GetRoomType();
                return prv_Botanical;
            }
        }

        private static RoomType prv_Industrial = null;
        public static RoomType IndustrialRoom(RoomType[] upgrades)
        {
            if (prv_Industrial == null)
                prv_Industrial = new RoomTypeIndustrialData(upgrades).GetRoomType();
            return prv_Industrial;
        }

        public static bool IsInTheRoom(KMonoBehaviour item, string roomId)
        {
            CavityInfo info = Game.Instance.roomProber.GetCavityForCell(Grid.PosToCell(item));
            if (info == null || info.room == null || info.room.roomType == null)
                return false;
            return info.room.roomType.Id == roomId;
        }
    }
}
