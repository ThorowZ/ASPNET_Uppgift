using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp_Uppgift.Models;
using WebApp_Uppgift.Services;
using WebApp_Uppgift.ViewModels;

namespace WebApp_Uppgift.Controllers;


public class ProjectManagementController : Controller
{
    private readonly IProjectService _projectService;

    public ProjectManagementController(IProjectService projectService) => _projectService = projectService;

    [Route("projects")]
    public async Task<IActionResult> Projects()
    {
        ViewData["Title"] = "Projects";

        var model = new ProjectsViewModel
        {
            Projects = await _projectService.GetAllProjectsAsync(),
        };
        return View(model);
    }

    [HttpPost]
    [Route("projects/addproject")]
    public async Task<IActionResult> Add(AddProjectFormData model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Invalid model state" });
        }

        var formData = new AddProjectFormData
        {
            ProjectName = model.ProjectName,
            ClientName = model.ClientName,
            Description = model.Description,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Budget = model.Budget,
            Image = model.Image,
            ClientId = model.ClientId,
            UserId = model.UserId,
            StatusId = model.StatusId
        };

        var result = await _projectService.CreateProjectAsync(model);

        return Json(new { success = result.Success, message = result.ErrorMessage });
    }

    [HttpPut]
    [Route("projects/updateproject")]
    public async Task<IActionResult> Update(UpdateProjectFormData model)
    {
        var result = await _projectService.UpdateProjectAsync(model.Id, model);

        return Json(new { success = result.Success, ErrorMessage = result.ErrorMessage });
    }

    [HttpDelete]
    [Route("projects/deleteproject")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _projectService.DeleteProjectAsync(id);

        return Json(new { success = result.Success });
    }
}
