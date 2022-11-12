using ChatRoomWithBot.Domain.Bus;

namespace ChatRoomWithBot.Domain.Interfaces;

public interface IMediatorHandler
{
    Task<CommandResponse> SendMessage<T>(T chatMessage) where T : Event;
}