using ChatRoomWithBot.Domain.Bus;
using MediatR;

namespace ChatRoomWithBot.Domain.Events
{
    public class JoinChatRoomEvent : Event, IRequest<CommandResponse>
    {
        public    Guid RoomId { get; set; }
        public Guid UserId { get; set; }
        public JoinChatRoomEvent(Guid roomId, Guid userId)
        {
            RoomId = roomId;
            UserId = userId;
        }
    }
}
