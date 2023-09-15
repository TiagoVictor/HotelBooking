using Application.Room.DTO;
using Application.Room.Ports;
using Application.Room.Requests;
using Application.Room.Responses;
using Domain.Room.Exceptions;
using Domain.Room.Ports;

namespace Application.Room
{
    public class RoomManager : IRoomManager
    {
        private readonly IRoomRepository _roomRepository;

        public RoomManager(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<RoomResponse> CreateRoomAsync(CreateRoomRequest request)
        {
            try
            {
                var room = RoomDTO.MapToEntity(request.Data);

                await room.Save(_roomRepository);
                request.Data.Id = room.Id;

                return new RoomResponse
                {
                    Success = true,
                    Data = request.Data
                };
            }
            catch (InvalidRoomDataException ex)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = Application.Responses.ErrorCode.ROOM_MISSING_REQUIRED_INFORMATION,
                    Message = ex.Message
                };
            }
            catch (Exception)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = Application.Responses.ErrorCode.ROOM_COLD_NOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                }; 
            }
        }

        public async Task<RoomResponse> GetRoomAsync(int id)
        {
            return new RoomResponse{
                Data = RoomDTO.MapToDto(await _roomRepository.Get(id) ?? new())
            };
        }
    }
}
