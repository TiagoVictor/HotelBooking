using Data.Guest;
using Data.Room;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        public DbSet<Domain.Guest.Entity.Guest> Guests { get; set; }
        public DbSet<Domain.Room.Entity.Room> Rooms { get; set; }
        public DbSet<Domain.Booking.Entity.Booking> Bookings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GuestConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
        }

    }
}