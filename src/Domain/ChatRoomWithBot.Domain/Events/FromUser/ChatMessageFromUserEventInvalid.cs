namespace ChatRoomWithBot.Domain.Events.FromUser
{
    public  class ChatMessageFromUserEventInvalid: ChatMessageFromUserEvent
    {
        public Guid UserId { get; set; }
    }
}
