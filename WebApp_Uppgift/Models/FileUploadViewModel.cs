using System.ComponentModel.DataAnnotations;

namespace WebApp_Uppgift.Models;

public class FileUploadViewModel
{

    [Required(ErrorMessage = "File is required to upload.")]
    public IFormFile File { get; set; } = null!;

}


public class  UserRegistrationFormModel
{

    
}
