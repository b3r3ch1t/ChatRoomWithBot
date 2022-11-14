using ChatRoomWithBot.Domain.Bus;

namespace ChatRoomWithBot.Domain.Events
{
    public  class ChatMessageTextEvent:Event 
    {
        public readonly Guid CodeRoom;

        public ChatMessageTextEvent(string message, Guid codeRoom, Guid userId) : base(userId, message)
        {
            Message = message;
            CodeRoom = codeRoom;
            UserId = userId;
            Id = Guid.NewGuid();
        }
    }
}
