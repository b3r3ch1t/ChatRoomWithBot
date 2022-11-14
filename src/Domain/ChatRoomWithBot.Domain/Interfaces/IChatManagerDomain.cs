using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;

namespace ChatRoomWithBot.Domain.Interfaces;

public interface IChatManagerDomain : IDisposable
{
    Task<CommandResponse> SendMessageAsync(Event message);

}