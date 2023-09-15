using Domain.Room.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Room
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _context;

        public RoomRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Domain.Room.Entity.Room room)
        {
            await _context
                .Rooms
                .AddAsync(room);

            await _context
                .SaveChangesAsync();

            return room.Id;
        }

        public async Task<Domain.Room.Entity.Room?> Get(int id)
        {
            return await _context
                .Rooms
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Domain.Room.Entity.Room?> GetAggregate(int id)
        {
            return await _context
                .Rooms
                .Include(x => x.Bookings)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
