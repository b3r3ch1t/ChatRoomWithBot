using ChatRoomWithBot.Domain.Bus;
using MediatR;

namespace ChatRoomWithBot.Domain.Events
{
    public  class ChatMessageTextEvent:Event, IRequest<CommandResponse>
    {
       
    }
}
