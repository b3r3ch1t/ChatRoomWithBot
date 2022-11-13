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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IError _error;


        private const string key = "roomId";
        public ChatRoomController(IChatManagerApplication managerChatMessage, IUsersAppService usersAppService, IHttpContextAccessor httpContextAccessor, IError error)
        {
            _managerChatMessage = managerChatMessage;
            _usersAppService = usersAppService;
            _httpContextAccessor = httpContextAccessor;
            _error = error;
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

            var validateRoomId = await ValidateRoomIdAsync(model.RoomId);

            if (!validateRoomId )
            {
                return BadRequest("user or room invalid !");
            }

            var result = await _managerChatMessage.SendMessageAsync(model);

            if (result.Failure)

                return BadRequest();

            return Accepted(result);

        }

        private async Task<bool> ValidateRoomIdAsync (string roomIdString)
        {
            try
            {
                var roomId = new Guid(Criptografia.Decrypt(roomIdString));

                var result = await _managerChatMessage.GetChatRoomByIdAsync(roomId);

                return result != null;
            }
            catch (Exception e)
            {
                _error.Error(e);
                return false;
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
