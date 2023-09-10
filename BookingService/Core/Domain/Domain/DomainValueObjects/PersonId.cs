using Domain.DomainEnums;

namespace Domain.DomainValueObjects
{
    public class PersonId
    {
        public string IdNumber { get; set; }
        public DocumentType DocumentType { get; set; }
    }
}
