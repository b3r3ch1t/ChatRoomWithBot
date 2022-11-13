using ChatRoomWithBot.Domain.Entities;

namespace ChatRoomWithBot.Domain.Interfaces.Repositories;

public interface IChatMessageRepository : IRepository<ChatMessage>
{
    Task<IQueryable<ChatMessage>> GetAllMessagesAsync(int qte);
}