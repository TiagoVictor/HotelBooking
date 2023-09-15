using Domain.Booking.Enum;
using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using Domain.Room.Exceptions;

namespace Domain.Booking.Entity
{
    public class Booking
    {
        public Booking()
        {
            Status = Status.Created;
        }

        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Status Status { get; set; }
        public Room.Entity.Room Room { get; set; }
        public Guest.Entity.Guest Guest { get; set; }
        public void ChangeState(Enum.Action action)
        {
            Status = (Status, action) switch
            {
                (Status.Created, Enum.Action.Pay) => Status.Paid,
                (Status.Created, Enum.Action.Cancel) => Status.Canceled,
                (Status.Paid, Enum.Action.Finish) => Status.Finished,
                (Status.Paid, Enum.Action.Refound) => Status.Refounded,
                (Status.Canceled, Enum.Action.ReOpen) => Status.Created,
                _ => Status
            };
        }
        private void ValidateState()
        {
            if (PlacedAt == DateTime.MinValue)
                throw new PlacedAtNullException();

            if (Start == DateTime.MinValue)
                throw new StartNullException();

            if (End == DateTime.MinValue)
                throw new EndNullException();

            if (Room == null)
                throw new RoomNullException();

            if (Guest == null)
                throw new GuestNullException();
            
        }

        public async Task Save(IBookingRepository bookingRepository)
        {
            ValidateState();

            Guest.IsValid();

            if (!Room.CanBeBooked())
                throw new RoomCannotBeBookedException();
            

            if (Id == 0)
                Id = await bookingRepository.CreateBooking(this);
        }
    }
}
