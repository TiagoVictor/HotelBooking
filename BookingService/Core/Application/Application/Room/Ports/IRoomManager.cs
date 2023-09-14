using Application.Room.DTO;
using Application.Room.Requests;
using Application.Room.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Room.Ports
{
    public interface IRoomManager
    {
        Task<RoomResponse> CreateRoomAsync(CreateRoomRequest request);
        Task<RoomResponse> GetRoomAsync(int id);
    }
}
