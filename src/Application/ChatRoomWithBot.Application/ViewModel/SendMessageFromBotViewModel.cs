namespace ChatRoomWithBot.Application.ViewModel;

public class SendMessageFromBotViewModel : SendMessageViewModel
{
    
    public string Message { get; set; }
    public Guid  RoomId { get; set; }


    public override  bool IsBot => true;
    public string HashBot { get; set; }
}