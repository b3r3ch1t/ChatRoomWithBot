using ChatRoomWithBot.Domain.Entities;

namespace ChatRoomWithBot.Domain.Interfaces.Repositories;

public interface IChatRoomRepository : IRepository<ChatRoom>
{ 
    IEnumerable< ChatMessage> GetLastMessagesAsync(int qte, Guid roomId);
}