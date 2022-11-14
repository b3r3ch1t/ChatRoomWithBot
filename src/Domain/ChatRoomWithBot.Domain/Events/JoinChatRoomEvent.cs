using ChatRoomWithBot.Domain.Bus;
using MediatR;

namespace ChatRoomWithBot.Domain.Events
{
    public class JoinChatRoomEvent : Event, IRequest<CommandResponse>
    {
        public readonly  Guid RoomId;

        public JoinChatRoomEvent(Guid roomId, Guid userId)
        {
            RoomId = roomId;
            UserId = userId;
        }
    }
}
