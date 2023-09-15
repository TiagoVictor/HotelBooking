using Domain.Guest.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Guest
{
    public class GuestRepository : IGuestRepository
    {
        private readonly HotelDbContext _context;

        public GuestRepository(HotelDbContext hotelDbContext)
        {
            _context = hotelDbContext;
        }

        public async Task<int> Create(Domain.Guest.Entity.Guest guest)
        {
            await _context
                .Guests
                .AddAsync(guest);
            await _context
                .SaveChangesAsync();

            return guest.Id;
        }

        public async Task<Domain.Guest.Entity.Guest?> Get(int id)
        {
            return await _context
                .Guests
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
