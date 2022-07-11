using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsExpanded
{
    class RoomTypeCategories_AllModded
    {
        static Dictionary<string, RoomTypeCategory> Categories = null;

        public static void Initalize()
        {
            Categories = new Dictionary<string, RoomTypeCategory>();
            Add(RoomTypeAgriculturalData.RoomId);
            Add(RoomTypeAquariumData.RoomId);
            Add(RoomTypeBathroomData.RoomId);
            Add(RoomTypeBotanicalData.RoomId);
            Add(RoomTypeGraveyardData.RoomId);
            Add(RoomTypeGymData.RoomId);
            Add(RoomTypeIndustrialData.RoomId);
            Add(RoomTypeKitchenData.RoomId);
            Add(RoomTypeLaboratoryData.RoomId);
            Add(RoomTypeMuseumData.RoomId);
            Add(RoomTypeMuseumHistoryData.RoomId);
            Add(RoomTypeMuseumSpaceData.RoomId);
            Add(RoomTypeNurseryData.RoomId);
            Add(RoomTypeNurseryGeneticData.RoomId);
            Add(RoomTypePrivateRoomData.RoomId);
        }

        private static string GetId(string roomId)
        {
            return string.Format("{0}Category", roomId);
        }

        private static void Add(string roomId)
        {
            if(Categories == null)
                Categories = new Dictionary<string, RoomTypeCategory>();
            Categories.Add(roomId, new RoomTypeCategory(GetId(roomId), "", roomId));
        }

        public static RoomTypeCategory GetCategory(string id)
        {
            if (Categories == null) Initalize();
            if (Categories.ContainsKey(id))
                return Categories[id];
            return Db.Get().RoomTypeCategories.None;
        }
    }
}
