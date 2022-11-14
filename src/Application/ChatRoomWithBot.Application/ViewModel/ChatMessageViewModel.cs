 

namespace ChatRoomWithBot.Application.ViewModel
{
    public  class ChatMessageViewModel
    {
         public string UserName { get; set; }
        
         public DateTime Date { get; set; }

         public string Message { get; set; }

        public Guid RoomId { get; set; }

    }
}
