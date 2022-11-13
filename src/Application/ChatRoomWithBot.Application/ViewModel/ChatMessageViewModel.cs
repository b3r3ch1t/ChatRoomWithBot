 

using System.ComponentModel.DataAnnotations;

namespace ChatRoomWithBot.Application.ViewModel
{
    public  class ChatMessageViewModel
    {

        public ChatMessageViewModel()
        {
            CreationDate = DateTime.Now;
        }

        [Required]
        public string Name { get; set; }

        
        [Required]
        public string Message { get; set; }

        [Required]
        public Guid ChatRoomId { get; set; }
        
        public DateTime CreationDate { get; }



    }
}
