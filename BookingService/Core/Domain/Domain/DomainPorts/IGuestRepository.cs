using Domain.DomainEntities;

namespace Domain.DomainPorts
{
    public interface IGuestRepository
    {
        Task<Guest?> Get(int id);
        Task<int> Create(Guest guest);
    }
}
