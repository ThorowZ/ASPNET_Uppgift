using Data.Entities;
using Data.Contexts;

namespace Data.Repositories;

public interface IUserRepository : IMainRepository<UserEntity>
{

}

public class UserRepository(AppDbContext context) : MainRepository<UserEntity>(context), IUserRepository
{
}

