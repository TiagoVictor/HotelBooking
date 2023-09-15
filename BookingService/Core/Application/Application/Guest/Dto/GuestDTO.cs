using Domain.Guest.Enum;

namespace Application.Guest.Dto
{
    public class GuestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public int IdTypeCode { get; set; }
        public static Domain.Guest.Entity.Guest MapEntity(GuestDto guestDTO)
        {
            return new Domain.Guest.Entity.Guest
            {
                Id = guestDTO.Id,
                Name = guestDTO.Name,
                Surname = guestDTO.Surname,
                Email = guestDTO.Email,
                DocumentId = new Domain.Guest.ValueObjects.PersonId
                {
                    IdNumber = guestDTO.IdNumber,
                    DocumentType = (DocumentType)guestDTO.IdTypeCode
                }
            };
        }

        public static GuestDto MapToDto(Domain.Guest.Entity.Guest guest)
        {
            return new GuestDto
            {
                Id = guest.Id,
                Name = guest.Name,
                Surname = guest.Surname,
                Email = guest.Email,
                IdNumber = guest.DocumentId.IdNumber,
                IdTypeCode = (int)guest.DocumentId.DocumentType
            };
        }
    }
}
