using Microsoft.AspNetCore.Mvc;
using WebApp_Uppgift.Services;

namespace WebApp_Uppgift.Controllers;


public class ProjectManagementController(ProjectService projectService) : Controller


{
    private readonly ProjectService _projectService = projectService;


    [Route("projects")]
    public IActionResult Projects()
    {
        ViewData["Title"] = "Projects";

        return View(_projectService.GetProjects());
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
