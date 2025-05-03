using Domain.Models;

namespace Business.Dtos;

public class ClientResult : ServiceResult
{
    public IEnumerable<Client>? Result { get; set; }

}
