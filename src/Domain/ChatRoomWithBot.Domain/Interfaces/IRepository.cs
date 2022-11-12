namespace ChatRoomWithBot.Domain.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : IEntity
{
    Task<TEntity> AddAsync(TEntity obj);

    Task<List<TEntity>> GetAllAsync();
}