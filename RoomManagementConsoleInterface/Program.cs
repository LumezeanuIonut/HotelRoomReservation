using RoomManagementConsoleInterface.Services;
using System.Net.Http;
using System.Text.Json;

public class AvailabilityService
{
    private readonly HttpClient _httpClient;

    public AvailabilityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetRoomAvailabilityAsync(string hotelId, string period, string roomType)
    {
        var response = await _httpClient.GetAsync($"api/RoomManagement/Availability?hotelIdAvailable={hotelId}&period={period}&roomTypeAvailable={roomType}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetRoomSearchAsync(string hotelId, int numberOfDays, string roomType)
    {
        var response = await _httpClient.GetAsync($"api/RoomManagement/Search?hotelIdAvailable={hotelId}&numberOfDays={numberOfDays}&roomTypeAvailable={roomType}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5208") // Adjust if needed
        };
        var availabilityService = new AvailabilityService(httpClient);

        while (true)
        {
            Console.WriteLine("Room Management Console Interface");
            Console.WriteLine("Type 'Availability(hotelId, period, roomType)' to check room availability.");
            Console.WriteLine("Type 'Search(hotelId, numberOfDays, roomType)' to search room availability.");
            Console.WriteLine("Type 'Exit' to quit.");
            Console.Write("Enter command: ");

            var command = Console.ReadLine();

            if (command.StartsWith("Availability("))
            {
                try
                {
                    var parameters = command.Substring("Availability(".Length).TrimEnd(')').Split(',');
                    var hotelId = parameters[0].Trim();
                    var period = parameters[1].Trim();
                    var roomType = parameters[2].Trim();

                    var availability = await availabilityService.GetRoomAvailabilityAsync(hotelId, period, roomType);
                    Console.WriteLine("Availability Information:");
                    Console.WriteLine($"Available Rooms: {availability}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else if (command.StartsWith("Search("))
            {
                try
                {
                    var parameters = command.Substring("Search(".Length).TrimEnd(')').Split(',');
                    var hotelId = parameters[0].Trim();
                    var numberOfDays = int.Parse(parameters[1].Trim());
                    var roomType = parameters[2].Trim();

                    var searchResult = await availabilityService.GetRoomSearchAsync(hotelId, numberOfDays, roomType);
                    Console.WriteLine("Search Results:");
                    Console.WriteLine(searchResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else if (command.Equals("Exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting...");
                return;
            }
            else
            {
                Console.WriteLine("Invalid command. Please try again.");
            }

            Console.WriteLine(new string('-', 40));
        }
    }
}
