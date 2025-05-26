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

            // Read bookings.json (as an array)
            var bookingsJson = System.IO.File.ReadAllText("JsonFiles/bookings.json");
            var bookings = JsonSerializer.Deserialize<List<RoomBooking>>($"[{bookingsJson}]");

            var availableRooms = 0;


            if (bookings == null)
            {
                return NotFound("Bookings data not found.");
            }

            if (period.Contains('-'))
            {
                var dates = period.Split('-');
                var startDateAvalable = DateTime.ParseExact(dates[0],"yyyyMMdd",CultureInfo.InvariantCulture);
                var endDateAvalable = DateTime.ParseExact(dates[1], "yyyyMMdd", CultureInfo.InvariantCulture);
                foreach (var booking in bookings) 
                {
                    var arrivalDate = DateTime.ParseExact(booking.arrival, "yyyyMMdd", CultureInfo.InvariantCulture);
                    var departureDate = DateTime.ParseExact(booking.departure, "yyyyMMdd", CultureInfo.InvariantCulture);


                    if ((startDateAvalable >= departureDate  || endDateAvalable < arrivalDate ) && booking.hotelId == hotelIdAvailable && booking.roomType == roomTypeAvailable) 
                    {
                        availableRooms++;
                    }
                }
            }
            else
            {
                var dateAvalable = DateTime.ParseExact(period, "yyyyMMdd", CultureInfo.InvariantCulture);
                foreach (var booking in bookings)
                {
                    var arrivalDate = DateTime.ParseExact(booking.arrival, "yyyyMMdd", CultureInfo.InvariantCulture);
                    var departureDate = DateTime.ParseExact(booking.departure, "yyyyMMdd", CultureInfo.InvariantCulture);
                    if ((dateAvalable < arrivalDate || dateAvalable >= departureDate) && booking.hotelId == hotelIdAvailable && booking.roomType == roomTypeAvailable)
                    {
                        availableRooms++;
                    }
                }

            }
            if (availableRooms != 0)
            {
                return Ok(availableRooms);
            }
            else
            {
                Exception noRoomsAvaileble = new Exception("Rooms are not available");
                throw noRoomsAvaileble;
            }
        }

        [HttpGet("Search")]
        public async Task<IActionResult> GetRoomsAvailable(string hotelIdAvailable, int numberOfDays, string roomTypeAvailable)
        {

            var bookingsJson = System.IO.File.ReadAllText("JsonFiles/bookings.json");
            var bookings = JsonSerializer.Deserialize<List<RoomBooking>>($"[{bookingsJson}]");
            return Ok(bookings);
        }
    }
}
