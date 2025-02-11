using ChatRoomWithBot.Domain.OperationResults;

namespace ChatRoomWithBot.Domain.Interfaces;

public interface ICacheRepository : IDisposable
{
    Task<OperationResult<T>> GetAsync<T>(string key);
    Task<OperationResult<bool>> SetAsync<T>(string key, T value, int cacheDurationInMinutes);
    Task<OperationResult<bool>> RemoveAsync(string key);
   
}