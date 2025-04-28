using System.ComponentModel.DataAnnotations;

namespace WebApp_Uppgift.Models;

public class LoginFormModel
{
    [Display(Name = "Email")]
    [Required(ErrorMessage = "You must enter your email")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "You must enter a valid email address")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password")]
    [Required(ErrorMessage = "You must enter your password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
