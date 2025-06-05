using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomManagementAPI.Models;
using System.Text.Json;
using System.IO;
using System.Globalization;

namespace RoomManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomManagementController : ControllerBase
    {
        [HttpGet("Availability")]
        public async Task<IActionResult> GetRoomsAvailable(string hotelIdAvailable, string period, string roomTypeAvailable)
        {
            var bookingsJson = System.IO.File.ReadAllText("JsonFiles/bookings.json");
            var bookings = JsonSerializer.Deserialize<List<RoomBooking>>($"[{bookingsJson}]");

            var hotelsJson = System.IO.File.ReadAllText("JsonFiles/hotels.json");
            var hotels = JsonSerializer.Deserialize<List<RoomManagementModel>>($"[{hotelsJson}]");

            var hotel = hotels.FirstOrDefault(h => h.id == hotelIdAvailable);
            if (hotel == null)
            {
                return NotFound("Hotel not found.");
            }

            var roomCount = hotel.rooms.Count(r => r.roomType == roomTypeAvailable);
            if (roomCount == 0)
            {
                return NotFound("No rooms of the specified type available in the hotel.");
            }

            var availableRooms = roomCount;

            if (period.Contains('-'))
            {
                var dates = period.Split('-');
                var startDateAvailable = DateTime.ParseExact(dates[0], "yyyyMMdd", CultureInfo.InvariantCulture);
                var endDateAvailable = DateTime.ParseExact(dates[1], "yyyyMMdd", CultureInfo.InvariantCulture);

                foreach (var booking in bookings)
                {
                    var arrivalDate = DateTime.ParseExact(booking.arrival, "yyyyMMdd", CultureInfo.InvariantCulture);
                    var departureDate = DateTime.ParseExact(booking.departure, "yyyyMMdd", CultureInfo.InvariantCulture);

                    if (!(startDateAvailable >= departureDate || endDateAvailable < arrivalDate) && booking.hotelId == hotelIdAvailable && booking.roomType == roomTypeAvailable)
                    {
                        availableRooms--;
                    }
                }
            }
            else
            {
                var dateAvailable = DateTime.ParseExact(period, "yyyyMMdd", CultureInfo.InvariantCulture);

                foreach (var booking in bookings)
                {
                    var arrivalDate = DateTime.ParseExact(booking.arrival, "yyyyMMdd", CultureInfo.InvariantCulture);
                    var departureDate = DateTime.ParseExact(booking.departure, "yyyyMMdd", CultureInfo.InvariantCulture);

                    if (!(dateAvailable < arrivalDate || dateAvailable >= departureDate) && booking.hotelId == hotelIdAvailable && booking.roomType == roomTypeAvailable)
                    {
                        availableRooms--;
                    }
                }
            }

            if (availableRooms > 0)
            {
                return Ok(availableRooms);
            }
            else
            {
                return NotFound("Rooms are not available.");
            }
        }

        [HttpGet("Search")]
        public async Task<IActionResult> GetRoomsSearch(string hotelIdAvailable, int numberOfDays, string roomTypeAvailable)
        {
            var bookingsJson = System.IO.File.ReadAllText("JsonFiles/bookings.json");
            var bookings = JsonSerializer.Deserialize<List<RoomBooking>>($"[{bookingsJson}]");

            var hotelsJson = System.IO.File.ReadAllText("JsonFiles/hotels.json");
            var hotels = JsonSerializer.Deserialize<List<RoomManagementModel>>($"[{hotelsJson}]");

            var hotel = hotels.FirstOrDefault(h => h.id == hotelIdAvailable);
            if (hotel == null)
            {
                return NotFound("Hotel not found.");
            }

            var roomCount = hotel.rooms.Count(r => r.roomType == roomTypeAvailable);
            if (roomCount == 0)
            {
                return NotFound("No rooms of the specified type available in the hotel.");
            }

            var availabilityList = new List<string>();
            var currentDate = DateTime.Now;

            for (int i = 0; i < numberOfDays; i++)
            {
                var startDate = currentDate.AddDays(i);
                var endDate = startDate.AddDays(1);

                var availableRooms = roomCount;

                foreach (var booking in bookings)
                {
                    var arrivalDate = DateTime.ParseExact(booking.arrival, "yyyyMMdd", CultureInfo.InvariantCulture);
                    var departureDate = DateTime.ParseExact(booking.departure, "yyyyMMdd", CultureInfo.InvariantCulture);

                    if (!(startDate >= departureDate || endDate < arrivalDate) && booking.hotelId == hotelIdAvailable && booking.roomType == roomTypeAvailable)
                    {
                        availableRooms--;
                    }
                }

                if (availableRooms > 0)
                {
                    availabilityList.Add($"({startDate:yyyyMMdd}-{endDate:yyyyMMdd}, {availableRooms})");
                }
            }

            return Ok(string.Join(", ", availabilityList));
        }
    }
}
