namespace Domain.Booking.Exceptions
{
    public class RoomNullException : Exception
    {
        public override string Message => "Room was null";
    }
}
