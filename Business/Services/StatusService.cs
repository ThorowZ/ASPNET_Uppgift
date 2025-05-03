using Business.Dtos;
using Data.Entities;
using Data.Repositories;
using Domain.Models;
using Org.BouncyCastle.Security;
using System.Runtime.CompilerServices;

namespace Business.Services;

public interface IStatusService
{
    Task<StatusResult> GetStatusAsync();
}

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
    private readonly IStatusRepository _statusRepository = statusRepository;

    public async Task<StatusResult> GetStatusAsync()
    {

        var result = await _statusRepository.GetAllAsync();
        if (!result.Success || result.Result == null)
        {
            return new StatusResult { Success = false, ErrorMessage = "No statuses found." };
        }

        return new StatusResult
        {
            Success = true,
            StatusCode = result.StatusCode,
            Result = result.Result.Select(e => new Status
            {
                id = e.id,
                StatusName = e.StatusName
            }).ToList()
        };
    }
} 
