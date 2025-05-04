using Business.Dtos;
using Data.Entities;
using Data.Repositories;
using Domain.Models;
using Org.BouncyCastle.Security;
using System.Runtime.CompilerServices;

namespace Business.Services;

public interface IStatusService
{
    Task<StatusResult<IEnumerable<Status>>> GetStatusAsync(); 
    Task<StatusResult<Status>> GetStatusByIdAsync(int statusId);
    Task<StatusResult<Status>> GetStatusByNameAsync(string statusName);
}

public class StatusService : IStatusService
{
    private readonly IStatusRepository _statusRepository;

    public StatusService(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }

    public async Task<StatusResult<IEnumerable<Status>>> GetStatusAsync()
    {
        var result = await _statusRepository.GetAllAsync();
        if (!result.Success || result.Result == null)
        {
            return new StatusResult<IEnumerable<Status>> { Success = false, ErrorMessage = "No statuses found." };
        }

        return new StatusResult<IEnumerable<Status>>
        {
            Success = true,
            StatusCode = result.StatusCode,
            Result = result.Result.Select(e => new Status
            {
                id = e.id,
                StatusName = e.StatusName
            })
        };
    }

    public async Task<StatusResult<Status>> GetStatusByNameAsync(string statusName)
    {
        var result = await _statusRepository.GetAsync(x => x.StatusName == statusName);
        if (!result.Success || result.Result == null)
            return new StatusResult<Status> { Success = false, ErrorMessage = "Status not found." };

        return new StatusResult<Status>
        {
            Success = true,
            StatusCode = result.StatusCode,
            Result = new Status
            {
                id = result.Result.id,
                StatusName = result.Result.StatusName
            }
        };
    }

    public async Task<StatusResult<Status>> GetStatusByIdAsync(int id)
    {
        var result = await _statusRepository.GetAsync(x => x.id == id);

        if (!result.Success || result.Result == null)
            return new StatusResult<Status> { Success = false, ErrorMessage = "Status not found." };

        return new StatusResult<Status>
        {
            Success = true,
            StatusCode = result.StatusCode,
            Result = new Status
            {
                id = result.Result.id,
                StatusName = result.Result.StatusName
            }
        };
    }
}
