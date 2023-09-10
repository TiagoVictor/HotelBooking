using Domain.DomainEnums;

namespace Domain.DomainEntities
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
        private Status Status { get; set; }
        public Room Room { get; set; }
        public Guest Guest { get; set; }
        public Status CurrentStatus
        {
            get
            {
                return Status;
            }
        }
        public void ChangeState(DomainEnums.Action action)
        {
            Status = (Status, action) switch
            {
                (Status.Created, DomainEnums.Action.Pay) => Status.Paid,
                (Status.Created, DomainEnums.Action.Cancel) => Status.Canceled,
                (Status.Paid, DomainEnums.Action.Finish) => Status.Finished,
                (Status.Paid, DomainEnums.Action.Refound) => Status.Refounded,
                (Status.Canceled, DomainEnums.Action.ReOpen) => Status.Created,
                _ => Status
            };
        }
    }
}
