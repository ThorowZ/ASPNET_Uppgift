using Business.Dtos;
using Data.Entities;
using Data.Repositories;
using Domain.Models;

namespace Business.Services;

public interface IProjectService
{
    Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData);
    Task<ProjectResult<IEnumerable<Project>>> GetAllProjectsAsync();
    Task<ProjectResult<Project>> GetProjectByIdAsync(string id);
}

public class ProjectService(IProjectRepository projectRepository, IStatusService statusService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IStatusService _statusService = statusService;



    private Project MapProject(ProjectEntity entity)
    {
        return new Project
        {
            Image = entity.Image,
            ProjectName = entity.ProjectName,
            ClientName = entity.ClientName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Budget = entity.Budget,

            Client = new Client
            {
                id = entity.Client.id,
                ClientName = entity.Client.ClientName,
            },
            User = new User
            {
                Id = entity.User.Id,
                FirstName = entity.User.FirstName,
                LastName = entity.User.LastName,
                Email = entity.User.Email,
            },
            Status = new Status
            {
                id = entity.Status.id,
                StatusName = entity.Status.StatusName,
            }

        };
    }
    public async Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData)
    {
        var projectEntity = new ProjectEntity
        {
            ProjectName = formData.ProjectName,
            Description = formData.Description,
            ClientName = formData.ClientName,
            StartDate = formData.StartDate,
            EndDate = formData.EndDate,
            Budget = formData.Budget,
            Image = formData.Image,

            ClientId = formData.ClientId,
            StatusId = formData.StatusId,
            UserId = formData.UserId
        };

        var status = await _statusService.GetStatusByIdAsync(1);

        projectEntity.StatusId = status.Result!.id;

        var result = await _projectRepository.AddAsync(projectEntity);

        if (result == null || !result.Success)

        {
            return new ProjectResult
            {
                Success = false,
                StatusCode = 400,
                ErrorMessage = "Error creating project"
            };
        }
        return new ProjectResult
        {
            Success = true,
            StatusCode = 201
        };

    }


    public async Task<ProjectResult<IEnumerable<Project>>> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync
            (
                orderByDecending: true,
                sortBy: s => s.Created,
                filter: null,
                include => include.User,
                include => include.Status,
                include => include.Client
            );
        if (projects == null)
        {
            return new ProjectResult<IEnumerable<Project>>
            {
                Success = false,
                StatusCode = 404,
                ErrorMessage = "No projects found"
            };
        }
        return new ProjectResult<IEnumerable<Project>>
        {
            Success = true,
            StatusCode = 200,
            Result = projects.Result!.Select(MapProject).ToList()
        };
    }

    public async Task<ProjectResult<Project>> GetProjectByIdAsync(string id)
    {
        var response = await _projectRepository.GetAsync
            (filter: x => x.id == id,
                include => include.User,
                include => include.Status,
                include => include.Client
            );

        if (!response.Success || response.Result == null)

        {
            return new ProjectResult<Project>
            {
                Success = false,
                StatusCode = 404,
                ErrorMessage = $"Project '{id}' was not found."
            };
        }
        return new ProjectResult<Project>
        {
            Success = true,
            StatusCode = 200,
            // Github Copilot
            Result = new List<Project> { MapProject(response.Result!) }

        };
    }
    //Delete project
    public async Task<ProjectResult> DeleteProjectAsync(string id)
    {
        var projectEntity = await _projectRepository.GetAsync(x => x.id == id);
        if (projectEntity.Result == null || !projectEntity.Success)
        {
            return new ProjectResult
            {
                Success = false,
                StatusCode = 404,
                ErrorMessage = "Project not found"
            };
        }

        var result = await _projectRepository.DeleteAsync(projectEntity.Result);
        if (!result.Success)
        {
            return new ProjectResult
            {
                Success = false,
                StatusCode = 400,
                ErrorMessage = "Error deleting project"
            };
        }

        return new ProjectResult
        {
            Success = true,
            StatusCode = 200
        };
    }


    //Update project
    public async Task<ProjectResult> UpdateProjectAsync(string id, UpdateProjectFormData formData)
    {
        var projectEntity = new ProjectEntity
        {
            id = id,
            ProjectName = formData.ProjectName,
            Description = formData.Description,
            ClientName = formData.ClientName,
            StartDate = formData.StartDate,
            EndDate = formData.EndDate,
            Budget = formData.Budget,
            Image = formData.Image,


            ClientId = formData.ClientId,
            StatusId = formData.StatusId,
            UserId = formData.UserId
        };
        var result = await _projectRepository.UpdateAsync(projectEntity);
        if (result == null || !result.Success)
        {
            return new ProjectResult
            {
                Success = false,
                StatusCode = 400,
                ErrorMessage = "Error updating project"
            };
        }
        return new ProjectResult
        {
            Success = true,
            StatusCode = 200
        };
    }

}
