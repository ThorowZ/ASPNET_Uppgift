using Microsoft.AspNetCore.Mvc;

namespace WebApp_Uppgift.Controllers;

public class ProjectManagementController : Controller
{
    public IActionResult Home()
    {
        ViewData["Title"] = "Home";

        return View("Home");
    }

    public IActionResult AddProject()
    {
        return View();
    }

    public IActionResult OptionsProject()
    {
        return View();
    }

    public IActionResult EditProject()
    {
        return View();
    }
}
