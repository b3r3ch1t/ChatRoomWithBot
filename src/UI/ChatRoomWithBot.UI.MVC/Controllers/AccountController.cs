using System.Security.Claims;
using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.Data.IdentityModel;
using ChatRoomWithBot.Service.Identity.Interfaces;
using ChatRoomWithBot.Service.Identity.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoomWithBot.UI.MVC.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {

        private readonly IUserIdentityManager _userIdentityManager;
        private readonly IUsersAppService _usersAppService;

        public AccountController(IUserIdentityManager userIdentityManager, IUsersAppService usersAppService)
        {
            _userIdentityManager = userIdentityManager;
            _usersAppService = usersAppService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View();
            var result = await _userIdentityManager.Login(model);
            if (!result.Error && result.Result.Succeeded)
            {
                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(model.Email)),
                    new Claim(ClaimTypes.Name, model.Email),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
            }

            if (result.Result.Succeeded) return RedirectToAction("Index", "Rooms");


            TempData["Login"] = "User or password is invalid.";
            return RedirectToAction("Login", "Account");

        }


        [HttpGet]

        public async Task<IActionResult> Logoff()
        {

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Register"] = "Error in data";

                return View(model);
            }


            var result = await _userIdentityManager.Register(model);

            if (!result.Error && result.Result.Succeeded)
            {
                TempData["Register"] = "User created with success !";

            }
            return View();
        }

        public async Task<IActionResult> Users()
        {
            var users = await  _usersAppService.GetAllUsersAsync();

            return View(users); 
        }
    }
}
