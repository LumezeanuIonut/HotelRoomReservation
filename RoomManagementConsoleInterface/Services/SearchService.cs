using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomManagementConsoleInterface.Models;
using System.Net.Http.Json;
namespace RoomManagementConsoleInterface.Services
{
    class SearchService
    {
        private readonly HttpClient _httpClient;

        public SearchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RoomManagementModel> GetRoomByAsync(string hotelId, int numberOfDays, string roomType)
        {
            var hotels = await _httpClient.GetFromJsonAsync<RoomManagementModel>($"/api/{hotelId}");
            return hotels;
        }
    }
}
