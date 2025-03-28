using WebApp_Uppgift.Models;

namespace WebApp_Uppgift.Services;

public class ProjectService
{
    private List<Project> _projects = [];

    
    public ProjectService()
    {
        _projects.Add(new Project { Id = 1, ProjectName = "Project 1", Description = "Description 1", StartDate = "2021-01-01", EndDate = "2021-01-31", ClientId = 1 });
        _projects.Add(new Project { Id = 2, ProjectName = "Project 2", Description = "Description 2", StartDate = "2021-02-01", EndDate = "2021-02-28", ClientId = 2 });
        _projects.Add(new Project { Id = 3, ProjectName = "Project 3", Description = "Description 3", StartDate = "2021-03-01", EndDate = "2021-03-31", ClientId = 3 });
    }

    public IEnumerable<Project> GetProjects()
    {
        return _projects;
    }
}