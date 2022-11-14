namespace ChatRoomWithBot.Application.ViewModel
{
    public  class SendMessageViewModel
    {
        public string? UserName { get; set; }
        public Guid? UserId { get; set; }
        public Guid RoomId { get; set; }
        public string? RoomName { get; set; }
        public string? HashBot { get; set; }
        public string Message { get; set; }

        public bool IsCommand { get; set; } 
    }
}
