using Application.Booking.Dto;
using Application.Booking.Ports;
using Application.Booking.Requests;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingManager _bookingManager;
        public BookingController(IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Post(BookingDto booking)
        {
            var request = new CreateBookingRequest
            {
                Data = booking
            };

            var res = await _bookingManager.CreateBooking(request);

            if (res.Success) return Created("", res.Data);

            return res.ErrorCode switch
            {
                ErrorCode.BOOKING_COLD_NOT_STORE_DATA => BadRequest(res),
                ErrorCode.BOOKING_MISSING_REQUIRED_INFORMATION => BadRequest(res),
                ErrorCode.BOOKING_NOT_FOUND => NotFound(res),
                ErrorCode.BOOKING_ROOM_CANNOT_BE_BOOKED => BadRequest(res),
                _ => BadRequest(500)
            };
        }
    }
}
