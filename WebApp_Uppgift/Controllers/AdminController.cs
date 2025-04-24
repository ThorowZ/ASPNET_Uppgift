using Microsoft.AspNetCore.Mvc;

namespace WebApp_Uppgift.Controllers;

[Route("admin")]
public class AdminController : Controller
{

    [Route("members")]
    public IActionResult Members()
    {
        ViewData["Title"] = "Team Members";
        return View();
    }

    [Route("clients")]
    public IActionResult Clients()
    {
        ViewData["Title"] = "Clients";
        return View();
    }

    [Route("admin")]
    public IActionResult Admin()
    {
        ViewData["Title"] = "Admin";
        return View();
    }
}
