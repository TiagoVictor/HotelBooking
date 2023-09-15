using Domain.Booking.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Booking
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDbContext _context;
        public BookingRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateBooking(Domain.Booking.Entity.Booking booking)
        {
                 await _context
                .Bookings
                .AddAsync(booking);

                await _context
                    .SaveChangesAsync();

            return booking.Id;
        }

        public async Task<Domain.Booking.Entity.Booking?> Get(int id)
        {
            return await _context
                .Bookings
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
