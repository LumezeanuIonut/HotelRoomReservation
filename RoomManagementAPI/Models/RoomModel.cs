namespace RoomManagementAPI.Models
{
    public class Room
    {
        public string roomType { get; set; }
        public string roomId { get; set; }

        public Room(string _roomType, string _roomId)
        {
            roomType = _roomType;
            roomId = _roomId;
        }
        public Room()
        {
            
        }
    }

}
