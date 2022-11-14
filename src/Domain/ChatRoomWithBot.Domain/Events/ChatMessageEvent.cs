using ChatRoomWithBot.Domain.Bus;

namespace ChatRoomWithBot.Domain.Events
{
    public  class ChatMessageEvent:Event
    {
        public Guid CodeRoom { get; private set; }
        public ChatMessageEvent(Guid userId, string message, Guid codeRoom) : base(userId, message)
        {
            CodeRoom = codeRoom; 
        }
    }
}
