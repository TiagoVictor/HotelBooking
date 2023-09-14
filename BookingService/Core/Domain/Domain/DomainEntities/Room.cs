using Domain.DomainExceptions;
using Domain.DomainPorts;
using Domain.DomainValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainEntities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintence { get; set; }
        public Price Price { get; set; }
        public bool IsAvailable
        {
            get
            {
                if (InMaintence || HasGuest)
                {
                    return false;
                }
                return true;
            }
        }
        public bool HasGuest
        {
            get
            {
                return true;
            }
        }

        private void ValidateState()
        {
            if (string.IsNullOrEmpty(Name))
                throw new InvalidRoomDataException();
        }

        public async Task Save(IRoomRepository roomRepository)
        {
            ValidateState();

            if(Id == 0)
                Id = await roomRepository.Create(this);
        }
    }
}
