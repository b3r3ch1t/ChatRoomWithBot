namespace ChatRoomWithBot.Domain.Events.FromUser
{
    public  class ChatMessageFromUserEvent : Event
    {
        public Guid UserId { get; set; }
       
    }
}
