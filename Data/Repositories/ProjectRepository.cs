using Data.Entities;
using Data.Contexts;

namespace Data.Repositories;

public interface IProjectRepository : IMainRepository<ProjectEntity>
{

}

public class ProjectRepository(AppDbContext context) : MainRepository<ProjectEntity>(context), IProjectRepository
{
}

