using MediatR;

namespace ChatRoomWithBot.Domain.Events
{
    public class Event :  INotification
    {
        public Guid UserId { get; set; }
        public string Message { get; set; }
        protected Event()
        {

        }

        public Event(Guid userId, string message) 
        {
            UserId = userId;
            Message = message;
        }

        public bool IsBotCommand()
        {
            return Message.StartsWith('/');
        }
    }
}
