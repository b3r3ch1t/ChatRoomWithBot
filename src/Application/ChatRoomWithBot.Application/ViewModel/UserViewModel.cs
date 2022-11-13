namespace ChatRoomWithBot.Application.ViewModel
{
    public  class UserViewModel
    {
        public Guid Id { get; protected set; }

        public string Name { get; set; }
        
        public DateTime DateCreated { get; set; }

        public string Email { get; set;  }
    }
}
