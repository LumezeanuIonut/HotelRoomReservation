using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomManagementConsoleInterface.Models;
using System.Net.Http.Json;
namespace RoomManagementConsoleInterface.Services
{
    class AvailabilityService
    {
        private readonly HttpClient _httpClient;

        public AvailabilityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RoomManagementModel> GetRoomAvailabilityAsync(string hotelId, DateTime startDate, DateTime endDate, string roomType)
        {
            var availability = await _httpClient.GetFromJsonAsync<RoomManagementModel>($"/api/Availability");
            if (availability != null)
            {
                return availability;
            }
            else
            {
                throw new Exception("Failed to load room availability");
            }
        }
    }
}
