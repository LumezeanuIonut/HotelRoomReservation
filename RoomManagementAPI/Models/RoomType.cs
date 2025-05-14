namespace RoomManagementAPI.Models
{
    public class RoomType
    {
        public string? code { get; set; }
        public string? description { get; set; }

        public string[] amenities { get; set; } = Array.Empty<string>();
        public string[] features { get; set; } = Array.Empty<string>();

        public RoomType(string? _code, string? _description, string[] _amenities, string[] _features)
        {
            code = _code;
            description = _description;
            amenities = _amenities;
            features = _features;
        }
    }
}
