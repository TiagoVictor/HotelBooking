﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<GuestResponse>> Post(GuestDto guest)
        {
            var request = new CreateGuestRequest
            {
                Data = guest
            };

            var res = await _guestManager.CreateGuest(request);

            if (res.Success)
                return Created("", res.Data);

            return res.ErrorCode switch
            {
                ErrorCode.COULDNOT_STORE_DATA => BadRequest(res),
                ErrorCode.INVALID_DOCUMENT => BadRequest(res),
                ErrorCode.MISSING_REQUIRED_INFORMATION => BadRequest(res),
                ErrorCode.INVALID_EMAIL => BadRequest(res),
                _ => BadRequest(500),
            };
        }

        [HttpGet]
        public async Task<ActionResult<GuestDto>> Get(int guestId)
        {
            var res = await _guestManager.GetGuestAsync(guestId);

            if (res.Success) return Ok(res.Data);

            return NotFound(res);
        }
    }
}
