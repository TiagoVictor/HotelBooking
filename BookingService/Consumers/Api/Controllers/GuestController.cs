using Microsoft.AspNetCore.Mvc;
using Application.Guest.Ports;
using Application.Guest.Dto;
using Application.Responses;
using Application.Guest.Requests;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly IGuestManager _guestManager;

        public GuestController(
            IGuestManager guestManager)
        {
            _guestManager = guestManager;
        }

        [HttpPost]
        public async Task<ActionResult<GuestDTO>> Post(GuestDTO guest)
        {
            var request = new CreateGuestRequest
            {
                Data = guest
            };

            var res = await _guestManager.CreateGuest(request);

            if(res.Success) return Created("", res.Data);

            if(res.ErrorCode == ErrorCode.NOT_FOUND)
            {
                return BadRequest(res);
            }

            return BadRequest(500);
        }
    }
}
