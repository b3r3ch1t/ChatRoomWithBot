using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Entities;

namespace ChatRoomWithBot.Application.Interfaces;

public interface IChatManagerApplication : IDisposable
{
    Task<IEnumerable<ChatRoomViewModel>> GetChatRoomsAsync();
    Task<ChatRoomViewModel> GetChatRoomByIdAsync(Guid roomId);
    Task<CommandResponse> SendMessageAsync(SendMessageViewModel model);
    Task<CommandResponse> AddCommitedAsync(ChatMessage chatMessage);
    Task<IEnumerable<ChatMessageViewModel>> GetMessagesAsync();
}