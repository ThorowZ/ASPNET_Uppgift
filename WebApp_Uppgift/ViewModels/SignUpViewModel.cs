
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp_Uppgift.Models;
public class SignUpViewModel
{
    public SignUpFormModel FormData { get; set; } = new SignUpFormModel();
    public List<SelectListItem> ClientOptions { get; set; } = new List<SelectListItem>();
}
