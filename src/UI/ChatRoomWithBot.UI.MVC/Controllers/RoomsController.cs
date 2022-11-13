using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoomWithBot.UI.MVC.Controllers;

[Authorize]
public class RoomsController : Controller
{

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}