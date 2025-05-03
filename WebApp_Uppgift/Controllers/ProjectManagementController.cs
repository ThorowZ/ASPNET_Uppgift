using Microsoft.AspNetCore.Mvc;
using WebApp_Uppgift.Services;

namespace WebApp_Uppgift.Controllers;


public class ProjectManagementController(test_ProjectService projectService) : Controller


{
    private readonly test_ProjectService _projectService = projectService;


    [Route("projects")]
    public IActionResult Projects()
    {
        ViewData["Title"] = "Projects";

        return View(_projectService.GetProjects());
    }

    [Route("projects/addproject")]
    public IActionResult AddProject(string id)
    {
        ViewData["Title"] = "Add Project";

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
