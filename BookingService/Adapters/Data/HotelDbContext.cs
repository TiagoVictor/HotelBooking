using Data.Guest;
using Data.Room;
using Microsoft.EntityFrameworkCore;
using Entities = Domain.DomainEntities;

namespace Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        public DbSet<Entities.Guest> Guests { get; set; }
        public DbSet<Entities.Room> Rooms { get; set; }
        public DbSet<Entities.Booking> Bookings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GuestConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
        }

    }
}