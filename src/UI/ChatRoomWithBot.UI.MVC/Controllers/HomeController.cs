using System.Diagnostics;
using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.UI.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace ChatRoomWithBot.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChatManagerApplication _managerChat;
        private readonly IUsersAppService _usersAppService;
        private readonly GraphServiceClient _graphServiceClient;
        public HomeController(IChatManagerApplication managerChat, IUsersAppService usersAppService, GraphServiceClient graphServiceClient)
        {
            _managerChat = managerChat;
            _usersAppService = usersAppService;
            _graphServiceClient = graphServiceClient;
        }

        [AllowAnonymous]

        public IActionResult Index()
        {


            return View();
        }


        [Authorize]
        public async Task<ActionResult> Privacy()
        {

            //var user = await _graphServiceClient.Me.GetAsync();
            var u = await _usersAppService.GetCurrentUserAsync();


            var user = _graphServiceClient.Users[u.Email].Photo;



           //var organization = await _graphServiceClient.Organization.GetAsync();


           // var signIns = await _graphServiceClient.AuditLogs.SignIns
           //     .GetAsync();

           // var users = await _graphServiceClient.Users.GetAsync();


           var groups = await _graphServiceClient.Groups.GetAsync();
           
           return View();
        }



       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}