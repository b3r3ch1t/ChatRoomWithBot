 
namespace ChatRoomWithBot.Domain.Entities
{
    public class ChatMessage : Entity<ChatMessage>
    {
        public Guid UserId { get; protected set; }
        public string Message { get; protected set; }

        protected ChatMessage()
        {

        }

        public ChatMessage(Guid userId, string message)
        {
            UserId = userId;
            Message = message;
            DateCreated  = DateTime.Now;
        }


        public bool IsBotCommand()
        {
            return Message.StartsWith('/');
        }
    }
}
