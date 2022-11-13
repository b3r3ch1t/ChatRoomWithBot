 

namespace ChatRoomWithBot.Domain.Entities
{
    public  class ChatRoom:  Entity<ChatRoom>
    {
        public string Name { get; set; }

        protected ChatRoom()
        {

        }

        public ChatRoom(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name.ToLower()} - {Id}";
        }
    }
}
