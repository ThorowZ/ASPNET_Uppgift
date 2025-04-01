using System.ComponentModel.DataAnnotations;

namespace WebApp_Uppgift.Models;

public class SignUpFormModel
{
    public int Id { get; set; }

    [Display(Name = "Username")]
    [Required(ErrorMessage = "You must enter your username")]
    public string Username { get; set; } = null!;

    [Display(Name = "Email")]
    [Required(ErrorMessage = "You must enter your email")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "You must enter a valid email address")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password")]
    [Required(ErrorMessage = "You must enter your password")]
    [DataType(DataType.Password)]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[A-Za-z\\d]{8,}$", ErrorMessage = "You must enter a strong password, at least 8 characters long, with at least one lowercase letter, one uppercase letter, and one digit")]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm Password")]
    [Required(ErrorMessage = "You must confirm your password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = null!;

    [Display(Name = "Select Client")]
    [Required(ErrorMessage = "You must select a client")]
    public int ClientId { get; set; }
}
