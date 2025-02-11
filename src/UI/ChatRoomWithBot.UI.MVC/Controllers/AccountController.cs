using ChatRoomWithBot.Services.EntraId.ViewModels;
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
        public AccountController()
        {

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
            //if (!ModelState.IsValid) return View();
            //var result = await _userIdentityManager.Login(model);
            //if (!result.Error && result.Result.Succeeded)
            //{
            //    var claims = new List<Claim>() {
            //        new Claim(ClaimTypes.NameIdentifier, Convert.ToString(model.Email)),
            //        new Claim(ClaimTypes.Name, model.Email),
            //        new Claim("userEmail", model.Email),
            //    };

            //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
            //    var principal = new ClaimsPrincipal(identity);

            //    await HttpContext.SignInAsync(
            //        CookieAuthenticationDefaults.AuthenticationScheme,
            //        new ClaimsPrincipal(identity));
            //}

            // if (result.Result.Succeeded) return RedirectToAction("ChatRooms", "Home");


            TempData["Message"] = "User or password is invalid.";
            return RedirectToAction("Login", "Account");

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Error in data";

                return View(model);
            }


            //var result = await _userIdentityManager.Register(model);

            //if (!result.Error && result.Result.Succeeded)
            //{
            //    TempData["Message"] = "User created with success !";

            //}
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

    }
}
