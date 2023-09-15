namespace Domain.Booking.Enum
{
    public enum Action
    {
        Pay = 0,
        Finish = 1, // after paid and used
        Cancel = 2, // can never be paid
        Refound = 3, // Paid, then refound
        ReOpen = 4 // Only canceled
    }
}
