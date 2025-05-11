using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Models;

namespace WebApp_Uppgift.ViewModels
{
    public class AddProjectModalViewModel
    {
        public AddProjectFormData Form { get; set; } = new();
        public List<SelectListItem> StatusOptions { get; set; } = new();
    }
}
