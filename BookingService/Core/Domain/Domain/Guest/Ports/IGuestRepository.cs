namespace Domain.Guest.Ports
{
    public interface IGuestRepository
    {
        Task<Entity.Guest?> Get(int id);
        Task<int> Create(Entity.Guest guest);
    }
}