using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Client
{
    public string id { get; set; } = null!;
    public string ClientName { get; set; } = null!;

}
