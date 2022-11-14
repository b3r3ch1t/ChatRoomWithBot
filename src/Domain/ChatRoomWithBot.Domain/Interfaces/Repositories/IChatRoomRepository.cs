using ChatRoomWithBot.Domain.Entities;

namespace ChatRoomWithBot.Domain.Interfaces.Repositories;

public interface IChatRoomRepository : IRepository<ChatRoom>
{
    Task<bool> ExistsRoomIdAsync(Guid roomId);
    IEnumerable< ChatMessage> GetLastMessagesAsync(int qte);
}