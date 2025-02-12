using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;

namespace ChatRoomWithBot.Domain.Interfaces;

public interface IProcessarChatMessageCommandEvent
{
    Task<CommandResponse > ProcessarChatMessageCommandEventAsync(ChatMessageCommandEvent? chatMessage);
}