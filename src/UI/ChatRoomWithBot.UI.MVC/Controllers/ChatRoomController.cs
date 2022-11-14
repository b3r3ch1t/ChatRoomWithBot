using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Domain;
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
        public ChatRoomController(IChatManagerApplication managerChatMessage, IUsersAppService usersAppService, IBerechitLogger berechitLogger )
        {
            _managerChatMessage = managerChatMessage;
            _usersAppService = usersAppService;
            _berechitLogger = berechitLogger;
        }


        [HttpPost]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageViewModel model)
        {
            
            var user = await _usersAppService.GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest("user or room invalid ! ");
            }

            var room = await ValidateRoomIdAsync(model.RoomId);

            if (room == null)
            {
                return BadRequest("user or room invalid !");
            }

            var result = await _managerChatMessage.SendMessageAsync(roomId: room.ChatRoomId, message: model.Message, user.Id);

            if (result.Failure)

                return BadRequest();

            return Accepted(result);

        }

        private async Task<ChatRoomViewModel?> ValidateRoomIdAsync(string roomIdString)
        {
            try
            {
                var roomId = new Guid(Criptografia.Decrypt(roomIdString));

                var result = await _managerChatMessage.GetChatRoomByIdAsync(roomId);

                return result;
            }
            catch (Exception e)
            {
                _berechitLogger.Error(e);
                return null;
            }
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

            await GetOutEveryChatRoomAsync(user.Id);

            var result = await _managerChatMessage.JoinChatRoomAsync(roomId: id, userId: user.Id);

            switch (result)
            {
                case false:
                    TempData["Message"] = "Fail to Join in the room ";
                    break;
                case true:

                    ViewData["roomId"] = Criptografia.Encrypt(room.ChatRoomId.ToString());

                    break;
            }

            ViewData["ChatName"] = room.Name;

            return View("Index");
        }


        public Task<bool> GetOutEveryChatRoomAsync(Guid userId)
        {
            Response.Cookies.Delete(key);

            return Task.FromResult(true);
        }
    }
}
