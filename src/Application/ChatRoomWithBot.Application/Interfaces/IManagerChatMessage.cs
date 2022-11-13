using ChatRoomWithBot.Application.ViewModel;

namespace ChatRoomWithBot.Application.Interfaces;

public interface IManagerChatMessage:IDisposable
{
    Task<ChatMessageViewModel> SendMessageAsync(ChatMessageViewModel message, Guid roomId); 
}