namespace RoomManagementConsoleInterface.Models
{
    public class RoomBooking
    {
        public string hotelId { get; set; }
        public string arrival { get; set; }
        public string departure { get; set; }
        public string roomType { get; set; }
        public string roomRate { get; set; }

        public RoomBooking(string _hotelId,string _arrival , string _departure ,string _roomType , string _roomRate)
        {
            hotelId = _hotelId;
            arrival = _arrival;
            departure = _departure;
            roomType = _roomType;
            roomRate = _roomRate;
        }
        public RoomBooking()
        {
            
        }
    }
}
