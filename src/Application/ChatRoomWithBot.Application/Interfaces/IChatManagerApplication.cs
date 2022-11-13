using ChatRoomWithBot.Application.ViewModel;

namespace ChatRoomWithBot.Application.Interfaces;

public interface IChatManagerApplication : IDisposable
{
    Task<ChatMessageViewModel> SendMessageAsync(ChatMessageViewModel message );
    Task<IEnumerable<ChatRoomViewModel>> GetChatRoomsAsync();
    Task<bool> JoinChatRoomAsync(Guid roomId, Guid userId);
    Task<ChatRoomViewModel> GetChatRoomByIdAsync(Guid roomId);
}