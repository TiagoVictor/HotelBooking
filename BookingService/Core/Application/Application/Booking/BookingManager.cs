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
            catch (PlacedAtNullException ex)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.BOOKING_COLD_NOT_STORE_DATA,
                    Message = ex.Message
                };
            }
            catch (StartNullException ex)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.MISSING_REQUIRED_INFORMATION,
                    Message = ex.Message
                };
            }
            catch (EndNullException ex)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.MISSING_REQUIRED_INFORMATION,
                    Message = ex.Message
                };
            }
            catch (RoomNullException ex)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.MISSING_REQUIRED_INFORMATION,
                    Message = ex.Message
                };
            }
            catch (GuestNullException ex)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.MISSING_REQUIRED_INFORMATION,
                    Message = ex.Message
                };
            }
            catch (RoomCannotBeBookedException ex)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.BOOKING_ROOM_CANNOT_BE_BOOKED,
                    Message = ex.Message
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
