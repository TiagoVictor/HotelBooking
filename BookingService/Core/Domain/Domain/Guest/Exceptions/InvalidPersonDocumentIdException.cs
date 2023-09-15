namespace Domain.Guest.Exceptions
{
    public class InvalidPersonDocumentIdException : Exception
    {
        public override string Message => "Invalid ID passed";
    }
}
