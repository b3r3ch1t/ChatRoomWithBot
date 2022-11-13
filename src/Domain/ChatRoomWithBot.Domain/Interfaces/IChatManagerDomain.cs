namespace ChatRoomWithBot.Domain.Interfaces;

public interface IChatManagerDomain:IDisposable
{
   Task< bool> JoinChatRoomAsync(Guid roomId, Guid userId);
}