namespace RoomManagementConsoleInterface.Models
{
    public class RoomManagementModel
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public List<RoomType> roomTypes { get; set; } = new List<RoomType>();
        public List<Room> rooms { get; set; } = new List<Room>();

        public RoomManagementModel(string? _id, string? _name, List<RoomType> _roomTypes, List<Room> _rooms)
        {
            id = _id;
            name = _name;
            roomTypes = _roomTypes;
            rooms = _rooms;
        }
        public RoomManagementModel()
        {
            
        }
    }
}
