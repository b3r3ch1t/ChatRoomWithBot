using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Domain.Bus;

namespace ChatRoomWithBot.Application.Interfaces;

public interface IChatManagerApplication : IDisposable
{
    Task<IEnumerable<ChatRoomViewModel>> GetChatRoomsAsync();
    Task<ChatRoomViewModel> GetChatRoomByIdAsync(Guid roomId);
    Task<CommandResponse> SendMessageAsync(SendMessageViewModel model);
}