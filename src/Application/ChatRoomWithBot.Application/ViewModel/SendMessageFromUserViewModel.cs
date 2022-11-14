namespace ChatRoomWithBot.Application.ViewModel;

public class SendMessageFromUserViewModel : SendMessageViewModel
{
    
    public string Message { get; set; }
    public Guid RoomId { get; set; }
    public override  bool IsBot => false;
    public Guid  UserId { get; set; }
}
