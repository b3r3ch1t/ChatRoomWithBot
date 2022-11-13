using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoomWithBot.UI.MVC.Services
{

    [Authorize]
    public class ChatRoomHub:Hub
    {
         
    }
}
