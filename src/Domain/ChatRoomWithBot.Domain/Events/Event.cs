using MediatR;

namespace ChatRoomWithBot.Domain.Events
{
    public abstract class Event : INotification
    {
        
        public string Message { get; set; }
        public Guid CodeRoom { get; set; }

        public virtual bool IsBotCommand { get; set; }

        public bool IsCommand => Message.StartsWith("/stock=");

    }
}
