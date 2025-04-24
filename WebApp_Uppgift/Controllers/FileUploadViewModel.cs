using Microsoft.AspNetCore.Mvc;
using WebApp_Uppgift.Models; // Ensure this matches the actual namespace of FileUploadViewModel

namespace WebApp_Uppgift.Controllers;

public class FileUploadController(IWebHostEnvironment env) : Controller
{
    private readonly IWebHostEnvironment _env = env;

    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(FileUploadViewModel model)
    {

        if (!ModelState.IsValid || model.File == null || model.File.Length == 0)
            return View(model);

        var uploadFolder = Path.Combine(_env.WebRootPath, "uploads");
        Directory.CreateDirectory(uploadFolder);

        var filePath = Path.Combine(uploadFolder, model.File.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await model.File.CopyToAsync(stream);
        }

        ViewBag.Message = "File uploaded successfully!";

        return View(model);
    }
}
