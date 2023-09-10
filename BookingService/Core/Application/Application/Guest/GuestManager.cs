using Application.Guest.Dto;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Application.Responses;
using Domain.DomainPorts;

namespace Application.Guest
{
    public class GuestManager : IGuestManager
    {
        private readonly IGuestRepository _guestRepository;
        public GuestManager(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<GuestResponse> CreateGuest(CreateGuestRequest request)
        {
            try
            {
                var guest = GuestDTO.MapEntity(request.Data);

                request.Data.Id = await _guestRepository.Create(guest);

                return new GuestResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.COULDNOT_STORE_DATA,
                    Message = " There was an error when saving to DB"
                };
            }
        }
    }
}
