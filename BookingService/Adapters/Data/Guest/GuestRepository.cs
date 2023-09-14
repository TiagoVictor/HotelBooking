using Domain.DomainPorts;
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

        public async Task<int> Create(Domain.DomainEntities.Guest guest)
        {
            await _context
                .Guests
                .AddAsync(guest);
            await _context
                .SaveChangesAsync();

            return guest.Id;
        }

        public async Task<Domain.DomainEntities.Guest?> Get(int id)
        {
            return await _context
                .Guests
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
