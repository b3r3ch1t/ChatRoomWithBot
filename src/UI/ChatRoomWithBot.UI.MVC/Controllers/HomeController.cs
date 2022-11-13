using System.Diagnostics;
using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.UI.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace ChatRoomWithBot.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChatManagerApplication _managerChat;

        public HomeController(IChatManagerApplication managerChat)
        {
            _managerChat = managerChat;
        }

        [AllowAnonymous]

        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ChatRooms()
        {

            var model = await _managerChat.GetChatRoomsAsync();


            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}