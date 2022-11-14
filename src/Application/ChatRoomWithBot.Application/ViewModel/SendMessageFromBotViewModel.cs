namespace ChatRoomWithBot.Application.ViewModel;

public class SendMessageFromBotViewModel : ISendMessageViewModel
{
    
    public string Message { get; set; }
    public string RoomId { get; set; }

    public bool IsBot => true ;

    public string HashBot { get; set; }
}