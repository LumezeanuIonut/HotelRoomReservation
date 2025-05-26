using RoomManagementConsoleInterface.Services;
using RoomManagementConsoleInterface.Models;

// ... other using statements

class Program
{
    static async Task Main(string[] args)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5208") // Adjust if needed
        };
        //var hotelService = new HotelService(httpClient);
        var availabilityService = new AvailabilityService(httpClient);

        try
        {
           // var hotels = await hotelService.GetHotelsAsync();
            

            // --- Link to AvailabilityService ---
            Console.WriteLine("Check room availability:");
            Console.Write("Enter Hotel ID: ");
            var hotelId = Console.ReadLine();
            Console.Write("Enter Period (e.g., 2025-05-21_to_2025-05-22): ");
            var period = Console.ReadLine();
            Console.Write("Enter Room Type: ");
            var roomTypeInput = Console.ReadLine();

            var availability = await availabilityService.GetRoomAvailabilityAsync(hotelId, period, roomTypeInput);

            Console.WriteLine("Availability Information:");
            Console.WriteLine($"Hotel ID: {availability.id}");
            Console.WriteLine($"Hotel Name: {availability.name}");
            Console.WriteLine("Available Rooms:");
            if (availability.rooms != null)
            {
                foreach (var room in availability.rooms)
                {
                    Console.WriteLine($"  - Room ID: {room.roomId}, Type: {room.roomType}");
                }
            }
            Console.WriteLine(new string('-', 40));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
