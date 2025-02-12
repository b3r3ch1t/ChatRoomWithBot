namespace ChatRoomWithBot.Domain.Entities
{
    public class ChatRoom : Entity<ChatRoom>
    {
        public string Name { get; private set; }

        protected ChatRoom() { }

        public  ChatRoom(Guid id, string name)
        {
            Id = id; // Define o ID manualmente
            Name = name;
        }

        public static ChatRoom GetChatRoomDefault ()
        {
            Guid fixedId = Guid.Parse("c1a9f5a7-3b2e-4d6f-9c8e-123456789abc"); // GUID fixo
            return new ChatRoom(fixedId, "Geral");
        }

        public override string ToString()
        {
            return $"{Name.ToLower()} - {Id}";
        }
    }
}