using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.Application.ViewModel;
using Microsoft.AspNetCore.Mvc; 
namespace ChatRoomWithBot.UI.MVC.Controllers
{

    [ApiController]
    public class ApiRoomBaseController : Controller
    {
        
        private readonly IManagerChatMessage _managerChatMessage;


        public ApiRoomBaseController( IManagerChatMessage managerChatMessage)
        {
          
            _managerChatMessage = managerChatMessage;
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

            var roomId = this.GetType().GUID; 

            var result = await _managerChatMessage.SendMessageAsync(message, roomId);

            return ResponsePost(result);
        }


        [HttpGet]
        public IActionResult GetRoom()
        {
            return View(); 
        }
    }
}
