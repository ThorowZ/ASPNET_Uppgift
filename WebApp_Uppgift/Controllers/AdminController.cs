using Microsoft.AspNetCore.Mvc;

namespace WebApp_Uppgift.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {

        [Route("members")]
        public IActionResult Members()
        {
            ViewData["Title"] = "Team Members";
            return View();
        }
    }
}
