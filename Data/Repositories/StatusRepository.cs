using Data.Entities;
using Data.Contexts;

namespace Data.Repositories;

public interface IStatusRepository : IMainRepository<StatusEntity>
{

}

public class StatusRepository(AppDbContext context) : MainRepository<StatusEntity>(context), IStatusRepository
{
}

