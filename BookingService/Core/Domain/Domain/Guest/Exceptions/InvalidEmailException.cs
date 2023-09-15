namespace Domain.Guest.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public override string Message => "Invalid Email";
    }
}
