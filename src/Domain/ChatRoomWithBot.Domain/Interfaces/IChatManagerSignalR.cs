namespace ChatRoomWithBot.Domain.Interfaces;

public interface IChatManagerSignalR:IDisposable
{
    Task<bool> JoinChatRoomAsync(Guid roomId, Guid userId);
}