using Microsoft.AspNetCore.Mvc;

namespace WebApp_Uppgift.Controllers;

public class RegisterAuthController : Controller
{

    [Route("signup")]
    public IActionResult SignUp()
    {
        return View();
    }

    [Route("login")]
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Logout()
    {
        return RedirectToAction("Index", "Home"); 
    }
}
