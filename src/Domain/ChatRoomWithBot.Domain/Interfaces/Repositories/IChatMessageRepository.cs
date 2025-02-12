using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Entities;

namespace ChatRoomWithBot.Domain.Interfaces.Repositories;

public interface IChatMessageRepository : IRepository<ChatMessage>
{
    Task<IQueryable<ChatMessage>> GetAllMessagesAsync(int qte);
    Task<CommandResponse> AddCommitedAsync(ChatMessage chatMessage);
    IEnumerable<ChatMessage> GetLastMessagesAsync(int qte, Guid roomId);
}