using System.Diagnostics;
using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.UI.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoomWithBot.UI.MVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly IChatManagerApplication _managerChat;
		private readonly IUsersAppService _usersAppService;
		public HomeController(IChatManagerApplication managerChat, IUsersAppService usersAppService)
		{
			_managerChat = managerChat;
			_usersAppService = usersAppService;
		}

		[AllowAnonymous]

		public IActionResult Index()
		{

			var userName = _usersAppService.GetUserName();
			var tenantId = _usersAppService.GetTenantId();
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