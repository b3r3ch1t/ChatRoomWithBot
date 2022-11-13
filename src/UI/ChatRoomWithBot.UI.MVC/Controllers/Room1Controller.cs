using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.UI.MVC.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoomWithBot.UI.MVC.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class Room1Controller : ApiRoomBaseController, IRoomController
{
    
    public Room1Controller(IManagerChatMessage managerChatMessage) :base(managerChatMessage)
    {
       
    }

   

}