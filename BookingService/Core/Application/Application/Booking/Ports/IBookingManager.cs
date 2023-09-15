using Application.Booking.Dto;
using Application.Booking.Requests;
using Application.Booking.Responses;

namespace Application.Booking.Ports
{
    public interface IBookingManager
    {
        Task<BookingResponse> CreateBooking(CreateBookingRequest bookingDto);
        Task<BookingResponse> GetBooking(int id);
    }
}
