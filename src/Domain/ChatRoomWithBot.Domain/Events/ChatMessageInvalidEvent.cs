using ChatRoomWithBot.Domain.Bus;

namespace ChatRoomWithBot.Domain.Events
{
    public  class ChatMessageInvalidEvent : Event
    {
        public Guid CodeRoom { get; private set; }

        public ChatMessageInvalidEvent( string message, Guid codeRoom) : base(Guid.Empty, message)
        {
            Id = Guid.Empty;
            CodeRoom = codeRoom;
        }
    }
}
