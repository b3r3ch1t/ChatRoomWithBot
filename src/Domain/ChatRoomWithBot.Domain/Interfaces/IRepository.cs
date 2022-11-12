namespace ChatRoomWithBot.Domain.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : IEntity
{
    Task  AddAsync(TEntity obj, string password);

    Task<List<TEntity>> GetAllAsync();
}