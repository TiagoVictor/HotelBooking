namespace Domain.Booking.Exceptions
{
    public class StartNullException : Exception
    {
        public override string Message => "Start was null";
    }
}
