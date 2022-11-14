namespace ChatRoomWithBot.Domain.Interfaces.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity : IEntity
{
    Task  AddAsync(TEntity obj );

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity> GetByIdAsync(Guid id);
}