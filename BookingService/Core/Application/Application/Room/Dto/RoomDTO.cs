using Entities = Domain.DomainEntities;
using Domain.DomainValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DomainEnums;

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

        public static Entities.Room MapToEntity(RoomDTO dto)
        {
            return new Entities.Room
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
    }
}
