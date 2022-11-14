using ChatRoomWithBot.Domain.Bus;
using MediatR;

namespace ChatRoomWithBot.Domain.Events.FromUser
{
    public  class ChatMessageFromUserEventInvalid: ChatMessageFromUserEvent, IRequest<CommandResponse>
    {
        public Guid UserId { get; set; }
        public string  UserName { get; set; }
    }
}
