using Application.Guest.Dto;
using Application.Room.DTO;
using Domain.Booking.Entity;
using Domain.Booking.Enum;

namespace Application.Booking.Dto
{
    public class BookingDto
    {
        public BookingDto()
        {
            PlacedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        private Status Status { get; set; }

        public static Domain.Booking.Entity.Booking MapEntity(BookingDto booking)
        {
            return new Domain.Booking.Entity.Booking
            {
                Id = booking.Id,
                Start = booking.Start,
                End = booking.End,
                PlacedAt = booking.PlacedAt,
                Room = new Domain.Room.Entity.Room { Id = booking.RoomId },
                Guest = new Domain.Guest.Entity.Guest { Id = booking.GuestId },
            };
        }

        public static BookingDto MapToDto(Domain.Booking.Entity.Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                PlacedAt = booking.PlacedAt,
                Start = booking.Start,
                End = booking.End,
                RoomId = booking.Room.Id,
                GuestId = booking.Guest.Id
            };
        }
    }
}
