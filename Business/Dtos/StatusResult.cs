using Domain.Models;

namespace Business.Dtos;

public class StatusResult : ServiceResult
{
    public IEnumerable <Status>? Result { get; set; }

}
