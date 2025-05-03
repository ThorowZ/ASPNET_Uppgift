using Business.Dtos;
using Data.Repositories;
using Domain.Models;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository)
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<ProjectResult> CreateProjectAsync()
    {
        return new ProjectResult { Success = true, StatusCode = 201, ErrorMessage = "Error creating project" };
    }

    public async Task<ProjectResult<IEnumerable<Project>>> GetProjectAsync()
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
            return new ProjectResult<IEnumerable<Project>> { Success = false, StatusCode = 404, ErrorMessage = "No projects found" };
        }
        return new ProjectResult<IEnumerable<Project>> { Success = true, StatusCode = 200, Result = [] };
    }

    public async Task<ProjectResult<Project>> GetProjectAsync(string id)
    {
        var response = await _projectRepository.GetAsync
            (   filter: x => x.id == id,
                include => include.User,
                include => include.Status,
                include => include.Client
            );

        if (response == null)
        {
            return new ProjectResult<Project> { Success = false, StatusCode = 404, ErrorMessage = $"Project '{id}' was not found." };
        }
        return new ProjectResult<Project> { Success = true, StatusCode = 200, Result = [] };
    }

}
