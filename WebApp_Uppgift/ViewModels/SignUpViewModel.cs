using WebApp_Uppgift.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp_Uppgift.Models;
public class SignUpViewModel
{
    private readonly ClientService _clientService;

    public SignUpViewModel(ClientService clientService)
    {
        _clientService = clientService;
        Task.Run(PopulateClientOptionsAsync);
    }

    public SignUpFormModel FormData { get; set; } = new SignUpFormModel();
    public List<SelectListItem> ClientOptions { get; set; } = new List<SelectListItem>();

    public async Task PopulateClientOptionsAsync()
    {
        var clients = await _clientService.GetClients();
        ClientOptions = clients.Select(client => new SelectListItem
        {
            Value = client.Id.ToString(),
            Text = client.ClientName
        }).ToList();
    }
public void ClearFormData()
    {
        FormData = new SignUpFormModel();
    }

}


