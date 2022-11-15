using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoomWithBot.UI.MVC.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class ChatRoomController : Controller
    {

        private readonly IChatManagerApplication _managerChatMessage;
        private readonly IUsersAppService _usersAppService;
        private readonly IBerechitLogger _berechitLogger;

        private const string key = "roomId";
        public ChatRoomController(IChatManagerApplication managerChatMessage, IUsersAppService usersAppService, IBerechitLogger berechitLogger)
        {
            _managerChatMessage = managerChatMessage;
            _usersAppService = usersAppService;
            _berechitLogger = berechitLogger;
        }


        [HttpPost]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageViewModel model)
        {

            var room = await _managerChatMessage.GetChatRoomByIdAsync(model.RoomId);

            if (room == null)
            {
                return BadRequest("user or room invalid !");
            }


            var user = await _usersAppService.GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest("user or room invalid ! ");
            }

            model.UserId = user.Id;
            model.UserName = user.Name; 

            var result = await _managerChatMessage.SendMessageAsync(model);

            if (result.Failure)

                return BadRequest();

            return Accepted(result);

        }

         

        [HttpGet("JoinChatRoom/{id}")]
        public async Task<IActionResult> JoinChatRoom(Guid id)
        {

            var room = await _managerChatMessage.GetChatRoomByIdAsync(roomId: id);
            if (room == null)
            {
                TempData["Message"] = "This room is invalid !";

                return RedirectToAction("ChatRooms", "Home");
            }

            var user = await _usersAppService.GetCurrentUserAsync();
            if (user == null || user.Id == Guid.Empty)
            {
                return RedirectToAction("Login", "Account");
            }
            
 
            ViewData["ChatName"] = room.Name;
            ViewData["roomId"] = room.ChatRoomId;

            return View("Index");
        }
         
    }
}
