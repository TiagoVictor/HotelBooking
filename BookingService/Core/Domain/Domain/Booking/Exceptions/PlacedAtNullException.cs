namespace Domain.Booking.Exceptions
{
    public class PlacedAtNullException : Exception
    {
        public override string Message => "PlacedAt was null";
    }
}
