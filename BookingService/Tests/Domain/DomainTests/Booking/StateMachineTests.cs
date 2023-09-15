using Domain.Booking.Enum;
using Domain.Booking.Entity;

namespace DomainTests.Bookings
{
    public class StateMachineTests
    {
        [Fact]
        public void ShouldAwaysStartWithCreatedStatus()
        {
            var booking = new Booking();

            Assert.Equal(Status.Created, booking.Status);
        }

        [Fact]
        public void ShouldSetStatusPaidWhenPayingForABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.Booking.Enum.Action.Pay);

            Assert.Equal(Status.Paid, booking.Status);
        }

        [Fact]
        public void ShouldSetStatusToCanceldWhenCancelingABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.Booking.Enum.Action.Cancel);

            Assert.Equal(Status.Canceled, booking.Status);
        }

        [Fact]
        public void ShouldSetStatusToPaidWhenFinishingAPaidABooking()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.Booking.Enum.Action.Pay);
            booking.ChangeState(Domain.Booking.Enum.Action.Finish);

            Assert.Equal(Status.Finished, booking.Status);
        }

        [Fact]
        public void ShouldSetStatusToRefoundeddWhenFinishingAPaidABooking()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.Booking.Enum.Action.Pay);
            booking.ChangeState(Domain.Booking.Enum.Action.Refound);

            Assert.Equal(Status.Refounded, booking.Status);
        }

        [Fact]
        public void ShouldSetStatusToReopendWhenCanceldABooking()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.Booking.Enum.Action.Cancel);
            booking.ChangeState(Domain.Booking.Enum.Action.ReOpen);

            Assert.Equal(Status.Created, booking.Status);
        }

        [Fact]
        public void ShouldNotChangeStatusWhenRefoundingABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.Booking.Enum.Action.Refound);

            Assert.Equal(Status.Created, booking.Status);
        }

        [Fact]
        public void ShouldNotChangeStatusWhenRefoundingABookingWithFinishedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.Booking.Enum.Action.Pay);
            booking.ChangeState(Domain.Booking.Enum.Action.Finish);
            booking.ChangeState(Domain.Booking.Enum.Action.Refound);

            Assert.Equal(Status.Finished, booking.Status);
        }
    }
}