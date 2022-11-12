using ChatRoomWithBot.Domain.Entities;

namespace ChatRoomWithBot.Domain.Bus
{
    public class Event : ChatMessage
    {
        protected Event()
        {

        }

        public Event(Guid userId, string message):base(userId, message)
        {
            UserId = userId;
            Message = message; 
        }
    }
}
