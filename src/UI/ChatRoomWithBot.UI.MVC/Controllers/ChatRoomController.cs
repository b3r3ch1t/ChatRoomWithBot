using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.Application.ViewModel;
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

        private const string key = "roomId";
        public ChatRoomController(IChatManagerApplication managerChatMessage, IUsersAppService usersAppService)
        {
            _managerChatMessage = managerChatMessage;
            _usersAppService = usersAppService;
        }

        protected IActionResult ResponsePost<T>(T result)
        {
            if (result == null)
                return NoContent();

            return Ok(result);

        }


        /// <summary>
        /// Action for sending and store messages
        /// </summary>
        /// <param name="message">Model with message composition</param>
        /// <returns>Returns the posted message</returns>
        [HttpPost]
        [Route("postmessage")]
        public async Task<IActionResult> PostMessage([FromBody] ChatMessageViewModel message)
        {

            var result = await _managerChatMessage.SendMessageAsync(message);

            return ResponsePost(result);
        }


        [HttpGet]
        public IActionResult GetRoom()
        {
            return View();
        }


        [HttpGet("JoinChatRoom/{id}")]
        public async Task<IActionResult> JoinChatRoom(Guid id)
        {

            var room = await _managerChatMessage.GetChatRoomByIdAsync(roomId: id);
            if (room == null )
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

            if (!result)
            {
                TempData["Message"] = "Fail to Join in the room ";
            }

            if (result) CreateCookie(userId: user.Id);


            return View();
        }

        private void CreateCookie(Guid userId)
        {
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(30)
            };
            Response.Cookies.Append(key, userId.ToString(), option);
        }


        public Task<bool> GetOutEveryChatRoomAsync(Guid userId)
        {
            Response.Cookies.Delete(key);

            return Task.FromResult(true);
        }
    }
}
