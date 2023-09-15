using Domain.Room.Enum;
using Domain.Room.ValueObjects;

namespace Application.Room.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintence { get; set; }
        public decimal Price { get; set; }
        public AcceptedCurrencies Currency { get; set; }

        public static Domain.Room.Entity.Room MapToEntity(RoomDTO dto)
        {
            return new Domain.Room.Entity.Room
            {
                Id = dto.Id,
                Name = dto.Name,
                Level = dto.Level,
                InMaintence = dto.InMaintence,
                Price = new Price
                {
                    Currency = dto.Currency,
                    Value = dto.Price
                }
            };
        }

        public static RoomDTO MapToDto(Domain.Room.Entity.Room room)
        {
            return new RoomDTO
            {
                Id = room.Id,
                Name = room.Name,
                Level = room.Level,
                InMaintence = room.InMaintence,
                Currency = room.Price.Currency,
                Price = room.Price.Value
            };
        }
    }
}
