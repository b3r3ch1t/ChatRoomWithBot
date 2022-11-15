 

namespace ChatRoomWithBot.Service.WorkerService.Events
{
    public  class ChatBotMessage
    {

        public string Message { get; set; }
        public Guid CodeRoom { get; set; }
        public bool IsCommand => Message.StartsWith("/");

        public string UserName { get; set; }

        public Guid UserId { get; set; }
    }
}
