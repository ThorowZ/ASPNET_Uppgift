using Data.Entities;
using Data.Contexts;
using Domain.Models;

namespace Data.Repositories;

public interface IClientRepository : IMainRepository<ClientEntity>
{

}

public class ClientRepository(AppDbContext context) : MainRepository<ClientEntity>(context), IClientRepository
{
}

