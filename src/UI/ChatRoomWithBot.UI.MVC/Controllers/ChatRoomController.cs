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

            var room = await _usersAppService.GetChat(model.RoomId);

            if (room == null)
            {
                return BadRequest("user or room invalid !");
            }


            var user = await _usersAppService.GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest("user or room invalid ! ");
            }

            model.UserId =Guid.Parse(  user.TenantId ) ;
            model.UserName = user.Name;

            var result = await _managerChatMessage.SendMessageAsync(model);

            if (result.Failure)

                return BadRequest();

            return Accepted(result);

        }



        [HttpGet("JoinChatRoom/{id}")]
        public async Task<IActionResult> JoinChatRoom(Guid id)
        {

            var room = await _usersAppService.GetChat(id);
            if (room == null)
            {
                TempData["Message"] = "This room is invalid !";

                return RedirectToAction("index", "ChatRoom");
            }

            var user = await _usersAppService.GetCurrentUserAsync();
            if (user == null || Guid.Parse( user.TenantId ) == Guid.Empty)
            {
                return RedirectToAction("Login", "Account");
            }


            ViewData["ChatName"] = room.Name;
            ViewData["roomId"] = room.Id ;

            return View("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var rooms = await _usersAppService
                    .GetChats();

                return View(rooms);
            }
            catch (Exception e)
            {
                _berechitLogger.Error(e);

                return RedirectToAction("index", "Home");
            }
        }
    }
}
