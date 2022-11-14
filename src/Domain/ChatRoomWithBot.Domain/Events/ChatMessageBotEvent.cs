

namespace ChatRoomWithBot.Domain.Events
{
    public class ChatMessageBotEvent : Event
    {

        public Guid CodeRoom { get; set; }

        public ChatMessageBotEvent(string message, Guid codeRoom, Guid userId) : base(userId, message)
        {
            Message = message;
            CodeRoom = codeRoom;
            UserId = userId;
        }
    }
}
