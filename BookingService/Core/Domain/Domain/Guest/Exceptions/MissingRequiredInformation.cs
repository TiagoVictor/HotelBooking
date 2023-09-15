namespace Domain.Guest.Exceptions
{
    public class MissingRequiredInformation : Exception
    {
        public override string Message => "Missing Required Information";
    }
}
