using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp_Uppgift.Models;

namespace WebApp_Uppgift.Services;

public class ClientService
{
    public async Task<IEnumerable<ClientCreateFormModel>> GetClients()
    {
        return new List<ClientCreateFormModel>
        {
                new ClientCreateFormModel { Id = 1, ClientName = "Client 1" },
                new ClientCreateFormModel { Id = 2, ClientName = "Client 2" },
                new ClientCreateFormModel { Id = 3, ClientName = "Client 3" }
        };
    }

}




