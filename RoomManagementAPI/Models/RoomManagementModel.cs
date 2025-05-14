namespace RoomManagementAPI.Models
{
    public class RoomManagementModel
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public RoomType[] roomTypes { get; set; } = Array.Empty<RoomType>();

        public RoomManagementModel(string? _id, string? _name, RoomType[] _roomTypes)
        {
            id = _id;
            name = _name;
            roomTypes = _roomTypes;
        }
    }
}
