using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Events.FromBot;
using ChatRoomWithBot.Domain.Events.FromUser;

namespace ChatRoomWithBot.Domain.Interfaces;

public interface IChatManagerDomain : IDisposable
{
    Task<bool> JoinChatRoomAsync(Guid roomId, Guid userId);
    Task<CommandResponse> SendMessageFromUserAsync(ChatMessageFromUserEvent message);

    Task<CommandResponse> SendMessageFromBotAsync(ChatMessageFromBotEvent message);
}