using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomManagementAPI.Models;
namespace RoomManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomManagementController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRoomManagement()
        {
            RoomManagementModel room = new RoomManagementModel(
                "1",
                "Room Management",
                new RoomType[]
                {
                    new RoomType("SGL","Single Room",new string[] { "TV", "AC" },new string[] { "Free WiFi", "Breakfast Included" }),
                    new RoomType("DBL","Double Room",new string[] { "TV", "AC" },new string[] { "Sea View", "Non-smoking" })

                }
            );

            return Ok(room);
        }
    }
}
