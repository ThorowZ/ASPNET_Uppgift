using Microsoft.AspNetCore.Mvc;

namespace WebApp_Uppgift.Controllers;

public class RegisterAuthController : Controller
{

    [Route("signup")]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public IActionResult HandleSignUp()
    {
        return View();
    }

    [Route("login")]
    public IActionResult Login()
    {

        //return LocalRedirect("/projects");
        return View();
    }

    public IActionResult Logout()
    {
        return RedirectToAction("Index", "Home"); 
    }
}
