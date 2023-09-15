namespace Domain.Booking.Ports
{
    public interface IBookingRepository
    {
        Task<int> CreateBooking(Entity.Booking booking);
        Task<Entity.Booking?> Get(int id);
    }
}
