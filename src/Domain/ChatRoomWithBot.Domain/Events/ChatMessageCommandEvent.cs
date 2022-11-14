using ChatRoomWithBot.Domain.Bus;
using MediatR;

namespace ChatRoomWithBot.Domain.Events
{
    public  class ChatMessageCommandEvent: Event , IRequest<CommandResponse>
    {
    }
}
