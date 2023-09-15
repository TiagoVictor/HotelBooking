namespace Domain.Room.Exceptions
{
    public class RoomCannotBeBookedException : Exception
    {
        public override string Message => "Room Cannot Be Booked";
    }
}
