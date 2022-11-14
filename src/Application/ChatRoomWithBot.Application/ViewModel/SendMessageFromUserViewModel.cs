namespace ChatRoomWithBot.Application.ViewModel;

public class SendMessageFromUserViewModel : ISendMessageViewModel
{
    
    public string Message { get; set; }
    public string RoomId { get; set; }
    public bool IsBot => false;
}
