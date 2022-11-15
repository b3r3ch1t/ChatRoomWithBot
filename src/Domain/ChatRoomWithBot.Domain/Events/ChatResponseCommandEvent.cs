using ChatRoomWithBot.Domain.Bus;
using MediatR;

namespace ChatRoomWithBot.Domain.Events
{
    public  class ChatResponseCommandEvent:Event, IRequest<CommandResponse>
    {
    }
}
