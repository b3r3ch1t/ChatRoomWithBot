using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;

namespace ChatRoomWithBot.Domain.Interfaces;

public interface IMediatorHandler
{
    Task<CommandResponse> SendMessage<T>(T chatMessage) where T : Event;
}