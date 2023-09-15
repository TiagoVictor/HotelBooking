namespace Domain.Booking.Exceptions
{
    public class GuestNullException : Exception
    {
        public override string Message => "Guest was null";
    }
}
