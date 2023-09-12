using Application.Guest.Dto;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Application.Responses;
using Domain.DomainExceptions;
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

                await guest.Save(_guestRepository);

                request.Data.Id = guest.Id;

                return new GuestResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (InvalidPersonDocumentIdException)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.INVALID_DOCUMENT,
                    Message = "Invalid ID passed"
                };
            }
            catch (MissingRequiredInformation)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.MISSING_REQUIRED_INFORMATION,
                    Message = "Missing Required Information"
                };
            }
            catch (InvalidEmailException)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.INVALID_EMAIL,
                    Message = "Invalid Email"
                };
            }
            catch (Exception)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.COULDNOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }
    }
}
