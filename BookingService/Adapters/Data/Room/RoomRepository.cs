using Domain.DomainPorts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Room
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _context;

        public RoomRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Domain.DomainEntities.Room room)
        {
            await _context
                .Rooms
                .AddAsync(room);

            await _context
                .SaveChangesAsync();

            return room.Id;
        }

        public async Task<Domain.DomainEntities.Room?> Get(int id)
        {
            return await _context
                .Rooms
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
