using Application.Responses;
using Application.Room.DTO;
using Application.Room.Ports;
using Application.Room.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomManager _roomManager;

        public RoomController(IRoomManager roomManager)
        {
            _roomManager = roomManager;
        }

        [HttpPost]
        public async Task<ActionResult<RoomDTO>> Post(RoomDTO room)
        {
            var request = new CreateRoomRequest
            {
                Data = room
            };

            var res = await _roomManager.CreateRoomAsync(request);

            if (res.Success) return Created("", res.Data);

            return res.ErrorCode switch
            {
                ErrorCode.COULDNOT_STORE_DATA => BadRequest(res),
                ErrorCode.ROOM_MISSING_REQUIRED_INFORMATION => BadRequest(res),
                _ => BadRequest(500)
            };
        }
    }
}
