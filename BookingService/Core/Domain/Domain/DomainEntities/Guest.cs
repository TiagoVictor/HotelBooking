﻿using Domain.DomainExceptions;
using Domain.DomainPorts;
using Domain.DomainValueObjects;

namespace Domain.DomainEntities
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public PersonId DocumentId { get; set; }
        private void ValidateState()
        {
            if (DocumentId == null ||
                DocumentId.IdNumber.Length <= 3 ||
                DocumentId.DocumentType == 0)
            {
                throw new InvalidPersonDocumentIdException();
            }

            if (Name == null || Surname == null || Email == null)
            {
                throw new MissingRequiredInformation();
            }

            if (!Utils.Utils.ValidateEmail(Email))
            {
                throw new InvalidEmailException();
            }
        }

        public async Task Save(IGuestRepository guestRepository)
        {
            ValidateState();

            if (Id == 0)
                Id = await guestRepository.Create(this);
            //else 
                // await guestRepository.Update(this);
            
        }
    }
}
