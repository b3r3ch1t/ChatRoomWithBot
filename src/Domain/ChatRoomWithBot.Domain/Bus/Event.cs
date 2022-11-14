using ChatRoomWithBot.Domain.Entities;
using MediatR;

namespace ChatRoomWithBot.Domain.Bus
{
    public class Event : ChatMessage, INotification
    {
        protected Event()
        {

        }

        public Event(Guid userId, string message) : base(userId, message)
        {
            UserId = userId;
            Message = message;
        }

        
    }
}
