using Domain.DomainEntities;
using Domain.DomainEnums;

namespace DomainTests.Bookings
{
    public class StateMachineTests
    {
        [Fact]
        public void ShouldAwaysStartWithCreatedStatus()
        {
            var booking = new Booking();

            Assert.Equal(Status.Created, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusPaidWhenPayingForABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.DomainEnums.Action.Pay);

            Assert.Equal(Status.Paid, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusToCanceldWhenCancelingABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.DomainEnums.Action.Cancel);

            Assert.Equal(Status.Canceled, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusToPaidWhenFinishingAPaidABooking()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.DomainEnums.Action.Pay);
            booking.ChangeState(Domain.DomainEnums.Action.Finish);

            Assert.Equal(Status.Finished, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusToRefoundeddWhenFinishingAPaidABooking()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.DomainEnums.Action.Pay);
            booking.ChangeState(Domain.DomainEnums.Action.Refound);

            Assert.Equal(Status.Refounded, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusToReopendWhenCanceldABooking()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.DomainEnums.Action.Cancel);
            booking.ChangeState(Domain.DomainEnums.Action.ReOpen);

            Assert.Equal(Status.Created, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldNotChangeStatusWhenRefoundingABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.DomainEnums.Action.Refound);

            Assert.Equal(Status.Created, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldNotChangeStatusWhenRefoundingABookingWithFinishedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.DomainEnums.Action.Pay);
            booking.ChangeState(Domain.DomainEnums.Action.Finish);
            booking.ChangeState(Domain.DomainEnums.Action.Refound);

            Assert.Equal(Status.Finished, booking.CurrentStatus);
        }
    }
}