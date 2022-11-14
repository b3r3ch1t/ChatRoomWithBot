using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;

namespace ChatRoomWithBot.Domain.Interfaces;

public interface IChatManagerDomain:IDisposable
{
   Task< bool> JoinChatRoomAsync(Guid roomId, Guid userId);
   Task<CommandResponse> SendMessageAsync(Event message);
}