namespace Domain.Booking.Exceptions
{
    public class EndNullException : Exception
    {
        public override string Message => "End was null";
    }
}
