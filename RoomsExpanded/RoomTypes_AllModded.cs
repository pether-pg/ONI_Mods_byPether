﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomsExpanded
{
    class RoomTypes_AllModded
    {
        
        /*private static RoomType prv_Lab = null;

        public static RoomType LaboratoryRoom
        {
            get
            {
                if (prv_Lab == null)
                    prv_Lab = new RoomTypeLaboratoryData_Obsolete().GetRoomType();
                return prv_Lab;
            }
        }*/

        private static RoomType prv_Kitchenette = null;

        public static RoomType KitchenetteRoom
        {
            get
            {
                if (prv_Kitchenette == null)
                    prv_Kitchenette = new RoomTypeKitchenetteData().GetRoomType();
                return prv_Kitchenette;
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

        private static RoomType prv_gym = null;

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

        private static RoomType prv_Museum = null;
        public static RoomType Museum
        {
            get
            {
                if (prv_Museum == null)
                    prv_Museum = new RoomTypeMuseumData().GetRoomType();
                return prv_Museum;
            }
        }
        
        /*private static RoomType prv_Private = null;

        public static RoomType PrivateRoom
        {
            get
            {
                if (prv_Private == null)
                    prv_Private = new RoomTypePrivateRoomData_Obsolete().GetRoomType();
                return prv_Private;
            }
        }*/

        private static RoomType prv_GeneticNursery = null;

        public static RoomType GeneticNursery
        {
            get
            {
                if (prv_GeneticNursery == null)
                    prv_GeneticNursery = new RoomTypeNurseryGeneticData().GetRoomType();
                return prv_GeneticNursery;
            }
        }

        private static RoomType prv_MuseumSpace = null;

        public static RoomType MuseumSpace
        {
            get
            {
                if (prv_MuseumSpace == null)
                    prv_MuseumSpace = new RoomTypeMuseumSpaceData().GetRoomType();
                return prv_MuseumSpace;
            }
        }

        private static RoomType prv_HistoryMuseum = null;

        public static RoomType HistoryMuseum
        {
            get
            {
                if (prv_HistoryMuseum == null)
                    prv_HistoryMuseum = new RoomTypeMuseumHistoryData().GetRoomType();
                return prv_HistoryMuseum;
            }
        }

        private static RoomType prv_MissionControlRoom = null;

        public static RoomType MissionControlRoom
        {
            get
            {
                if (prv_MissionControlRoom == null)
                    prv_MissionControlRoom = new RoomTypeMissionControlRoomData().GetRoomType();
                return prv_MissionControlRoom;
            }
        }

        private static RoomType prv_BionicUpkeep = null;

        public static RoomType BionicUpkeepRoom
        {
            get
            {
                if (prv_BionicUpkeep == null)
                    prv_BionicUpkeep = new RoomTypeBionicUpkeepData().GetRoomType();
                return prv_BionicUpkeep;
            }
        }

        private static RoomType prv_DataMining = null;

        public static RoomType DataMining
        {
            get
            {
                if (prv_DataMining == null)
                    prv_DataMining = new RoomTypeDataMiningData().GetRoomType();
                return prv_DataMining;
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
