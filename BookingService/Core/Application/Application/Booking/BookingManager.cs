using Application.Booking.Dto;
using Application.Booking.Ports;
using Application.Booking.Requests;
using Application.Booking.Responses;
using Application.Guest.Dto;
using Application.Guest.Ports;
using Application.Responses;
using Application.Room.DTO;
using Application.Room.Ports;
using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using Domain.Guest.Ports;
using Domain.Room.Exceptions;
using Domain.Room.Ports;

namespace Application.Booking
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IGuestRepository _guestManager;
        private readonly IRoomRepository _roomManager;

        public BookingManager(IBookingRepository bookingRepository, IGuestRepository guestManager, IRoomRepository roomManager)
        {
            _bookingRepository = bookingRepository;
            _guestManager = guestManager;
            _roomManager = roomManager;
        }

        public async Task<BookingResponse> CreateBooking(CreateBookingRequest request)
        {
            try
            {
                var booking = BookingDto.MapEntity(request.Data);
                booking.Guest = await _guestManager.Get(request.Data.GuestId) ?? new();
                booking.Room = await _roomManager.GetAggregate(request.Data.RoomId) ?? new();

                await booking.Save(_bookingRepository);
                request.Data.Id = booking.Id;

                return new BookingResponse
                {
                    Success = true,
                    Data = request.Data
                };
            }
            catch (PlacedAtNullException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.BOOKING_COLD_NOT_STORE_DATA,
                    Message = "PlacedAt was null"
                };
            }
            catch (StartNullException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.MISSING_REQUIRED_INFORMATION,
                    Message = "Start was null"
                };
            }
            catch (EndNullException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.MISSING_REQUIRED_INFORMATION,
                    Message = "End was null"
                };
            }
            catch (RoomNullException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.MISSING_REQUIRED_INFORMATION,
                    Message = "Room was null"
                };
            }
            catch (GuestNullException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.MISSING_REQUIRED_INFORMATION,
                    Message = "Guest was null"
                };
            }
            catch (RoomCannotBeBookedException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.BOOKING_ROOM_CANNOT_BE_BOOKED,
                    Message = "Room Cannot Be Booked"
                }; 
            }
            catch (Exception)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.BOOKING_COLD_NOT_STORE_DATA,
                    Message = "There was an error when using DB"
                };
            }            
        }

        public Task<BookingResponse> GetBooking(int id)
        {
            throw new NotImplementedException();
        }
    }
}
