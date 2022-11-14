namespace ChatRoomWithBot.Application.ViewModel
{
    public interface ISendMessageViewModel
    {
        public string Message { get; set; }
        public string RoomId { get; set; }
        public bool IsBot { get; }
    }
}
