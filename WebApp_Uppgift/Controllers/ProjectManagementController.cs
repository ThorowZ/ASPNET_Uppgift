using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using WebApp_Uppgift.Models;
using WebApp_Uppgift.ViewModels;

namespace WebApp_Uppgift.Controllers;


public class ProjectManagementController : Controller
{
    private readonly IProjectService _projectService;
    private readonly IStatusService _statusService;

    public ProjectManagementController(IProjectService projectService, IStatusService statusService)
    { 
        _projectService = projectService;
        _statusService = statusService;
    }

    [Route("projects")]
    public async Task<IActionResult> Projects()
    {
        ViewData["Title"] = "Projects";

        var result = await _projectService.GetAllProjectsAsync();
        var statusResult = await _statusService.GetStatusAsync();


        if (result == null || !result.Success)
        {
            return View(new List<ProjectDisplayViewModel>());
        }


        var projectDisplayList = result.Result.Select(p => new ProjectDisplayViewModel
        {

            ProjectName = p.ProjectName,
            ClientName = p.ClientName,
            Description = p.Description,
            Status = p.Status.StatusName
        }).ToList();

        //ChatGpt hjälp till med Status 
        var statusOptions = statusResult?.Result?.Select(s => new SelectListItem
        {
            Value = s.id.ToString(),
            Text = s.StatusName
        }).ToList() ?? new();

        ViewBag.StatusOptions = statusOptions;

        var viewModel = new ProjectPageViewModel
        {
            Projects = projectDisplayList,
            Modal = new AddProjectModalViewModel
            {
                Form = new AddProjectFormData(),
                StatusOptions = statusOptions
            }
        };



        return View(viewModel);



    }


    [HttpPost]
    [Route("projects/addproject")]
    public async Task<IActionResult> Add(AddProjectFormData model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Invalid model state" });
        }

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Json(new { success = false, message = "User is not authenticated" });
        }

        model.UserId = userId;

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
