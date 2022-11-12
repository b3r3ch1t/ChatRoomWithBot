using ChatRoomWithBot.Domain.Bus;

namespace ChatRoomWithBot.Domain.Commands
{
    public  class ChatMessageEvent:Event
    {
        public ChatMessageEvent(Guid userId, string message ) : base(userId, message)
        {
             
        }
    }
}
