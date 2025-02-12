using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Entities;

namespace ChatRoomWithBot.Application.Interfaces;

public interface IChatManagerApplication : IDisposable
{ 
    Task<CommandResponse> SendMessageAsync(SendMessageViewModel model);
    Task<CommandResponse> AddCommitedAsync(ChatMessage chatMessage);
    Task<IEnumerable<ChatMessageViewModel>> GetMessagesAsync(Guid roomId, int qte);
}