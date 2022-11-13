 
using ChatRoomWithBot.Domain.Bus;
using MediatR;

namespace ChatRoomWithBot.Domain.Commands
{
    public class JoinChatRoomCommand : Event, IRequest<CommandResponse>
    {
        public readonly  Guid RoomId;

        public JoinChatRoomCommand(Guid roomId, Guid userId)
        {
            RoomId = roomId;
            UserId = userId;
        }
    }
}
