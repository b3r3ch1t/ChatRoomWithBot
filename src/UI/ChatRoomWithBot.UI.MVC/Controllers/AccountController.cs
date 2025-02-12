using ChatRoomWithBot.Application.Interfaces; 
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoomWithBot.UI.MVC.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUsersAppService _usersAppService;

        public AccountController(IUsersAppService usersAppService)
        {
            _usersAppService = usersAppService;
        }

       
        [HttpGet]

        public async Task<IActionResult> Logoff()
        {

            var callbackUrl = Url.Action("index", "Home", values: null, protocol: Request.Scheme);

            return SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                OpenIdConnectDefaults.AuthenticationScheme,
                CookieAuthenticationDefaults.AuthenticationScheme
            );
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

         
        public async Task<IActionResult> Users()
        {
            //var users = await  _usersAppService.GetAllUsersAsync();

            //return View(users); 

            return View();
        }


        [HttpGet("logout")]

        [AllowAnonymous]
        public IActionResult Logout()
        {
            var callbackUrl = Url.Action(nameof(Index), "Home", null, Request.Scheme);

            return SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                OpenIdConnectDefaults.AuthenticationScheme,
                CookieAuthenticationDefaults.AuthenticationScheme
            );



        }

        public async Task<IActionResult> Audit()
        {
            var audit =await  _usersAppService.GetAudits();

            return View(audit);
        }

        public async Task<IActionResult> Usuarios()
        {
            var users = await _usersAppService.GetAllUsersAsync();

            return View(users);
        }

        public async Task<IActionResult> Grupos()
        {
            var groups  = await _usersAppService.GetAllGroupsAsync ();

            return View(groups);
        }
    }
}
