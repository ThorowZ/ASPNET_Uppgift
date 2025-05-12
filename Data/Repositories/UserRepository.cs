using Data.Entities;
using Data.Contexts;

namespace Data.Repositories;

public interface IUserRepository : IMainRepository<ApplicationUser>
{

}

public class UserRepository(AppDbContext context) : MainRepository<ApplicationUser>(context), IUserRepository
{
}

