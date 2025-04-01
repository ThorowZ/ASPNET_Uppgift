using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp_Uppgift.Models;

namespace WebApp_Uppgift.Services;

public class ClientService
{
    public async Task<IEnumerable<Client>> GetClients()
    {
        return new List<Client>
        {
                new Client { Id = 1, ClientName = "Client 1" },
                new Client { Id = 2, ClientName = "Client 2" },
                new Client { Id = 3, ClientName = "Client 3" }
        };
    }

}




