using Domain.Models;

namespace Business.Dtos;

public class ProjectResult<T> : ServiceResult
{
    public IEnumerable<Project>? Result { get; set; }
}


public class ProjectResult: ServiceResult
{
}