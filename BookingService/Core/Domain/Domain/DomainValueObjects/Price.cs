using Domain.DomainEnums;

namespace Domain.DomainValueObjects
{
    public class Price
    {
        public decimal Value { get; set; }
        public AcceptedCurrencies Currency { get; set; }
    }
}
