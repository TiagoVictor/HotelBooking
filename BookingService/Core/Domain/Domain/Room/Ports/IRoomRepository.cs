namespace Domain.Room.Ports
{
    public interface IRoomRepository
    {
        Task<Entity.Room?> Get(int id);
        Task<int> Create(Entity.Room room);
        Task<Entity.Room?> GetAggregate(int id);
    }
}
