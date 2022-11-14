using MediatR;

namespace ChatRoomWithBot.Domain.Events
{
    public abstract  class Event :  INotification
    {
        
        public string Message { get;  }
        public Guid CodeRoom { get; }

        public virtual bool IsBotCommand { get; } 

    }
}
