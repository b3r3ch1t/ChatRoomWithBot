using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Domain.Bus;

namespace ChatRoomWithBot.Application.Interfaces;

public interface IChatManagerApplication : IDisposable
{
    Task<CommandResponse> SendMessageAsync(SendMessageViewModel model );
    Task<IEnumerable<ChatRoomViewModel>> GetChatRoomsAsync();
    Task<bool> JoinChatRoomAsync(Guid roomId, Guid userId);
    Task<ChatRoomViewModel> GetChatRoomByIdAsync(Guid roomId);
}