 

using ChatRoomWithBot.Domain.Bus;
using MediatR;

namespace ChatRoomWithBot.Domain.Events.FromUser
{
    public  class ChatMessageFromUserEventText: ChatMessageFromUserEvent,  IRequest<CommandResponse>
    {
    }
}
