using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoomWithBot.UI.MVC.Services
{

    [Authorize]
    public class ChatRoomHub:Hub
    {
        public void JoinGroup(string groupName)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
