namespace ChatRoomWithBot.Application.ViewModel
{
    public  class AuditModel
    {
        public string? Id { get; set; }
        public string IpAddress { get; set; }
        public string? Status { get; set; }
        public string? UserId { get; set; }
        public DateTimeOffset? CreatedDateTime { get; set; }
    }
}
