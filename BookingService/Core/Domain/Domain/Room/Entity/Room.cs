using Domain.Room.Exceptions;
using Domain.Room.Ports;
using Domain.Room.ValueObjects;

namespace Domain.Room.Entity
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintence { get; set; }
        public Price Price { get; set; }
        public ICollection<Booking.Entity.Booking> Bookings { get; set; }
        public bool IsAvailable
        {
            get
            {
                if (InMaintence || HasGuest)
                {
                    return false;
                }
                return true;
            }
        }
        public bool HasGuest
        {
            get
            {
                var noAvailableStatuses = new List<Booking.Enum.Status>()
                {
                    Booking.Enum.Status.Created,
                    Booking.Enum.Status.Paid
                };

                return Bookings.Where(
                    b => b.Room.Id == Id &&
                    noAvailableStatuses.Contains(b.Status)).Count() > 0;
            }
        }

        private void ValidateState()
        {
            if (string.IsNullOrEmpty(Name))
                throw new InvalidRoomDataException();

            if (Price == null || Price.Value < 10)
                throw new InvalidRoomPriceException();
        }

        public async Task Save(IRoomRepository roomRepository)
        {
            ValidateState();

            if (Id == 0)
                Id = await roomRepository.Create(this);
        }

        public bool CanBeBooked()
        {
            try
            {
                ValidateState();
            }
            catch (Exception)
            {
                return false;
            }

            if (!IsAvailable)
            {
                return false;
            }

            return true;
        }
    }
}
