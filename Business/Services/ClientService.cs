using Business.Dtos;
using Data.Repositories;
using Domain.Models;

namespace Business.Services;

public interface IClientService
{
    Task<ClientResult> GetClientAsync();
}

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<ClientResult> GetClientAsync()
    {
        var result = await _clientRepository.GetAllAsync();
        if (!result.Success || result.Result == null)
        {
            return new ClientResult { Success = false, ErrorMessage = "No clients found." };
        }
        return new ClientResult
        {
            Success = true,
            StatusCode = result.StatusCode,
            Result = result.Result.Select(e => new Client
            {
                id = e.id,
                ClientName = e.ClientName
            }).ToList()
        };
    }

}
