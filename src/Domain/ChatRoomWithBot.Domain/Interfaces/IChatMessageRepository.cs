using ChatRoomWithBot.Domain.Entities;

namespace ChatRoomWithBot.Domain.Interfaces;

public interface IChatMessageRepository : IRepository<ChatMessage>
{
    Task<IQueryable<ChatMessage>> GetAllMessagesAsync(int qte);
}