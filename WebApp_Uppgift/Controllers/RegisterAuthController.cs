using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp_Uppgift.Models;

namespace WebApp_Uppgift.Controllers;

public class RegisterAuthController(SignUpViewModel signUpViewModel) : Controller
{
    private readonly SignUpViewModel _signUpViewModel = signUpViewModel;

    [Route("signup")]
    public async Task<IActionResult> SignUp()
    {
        await _signUpViewModel.PopulateClientOptionsAsync();
        return View(_signUpViewModel);
    }

    [HttpPost]
    [Route("signup")]
    public IActionResult HandleSignUp(SignUpFormModel formData)
    {
        if (!ModelState.IsValid)
        {
            _signUpViewModel.FormData = formData;
            return View("SignUp", _signUpViewModel);
        }

        _signUpViewModel.FormData = new SignUpFormModel();
        return View("SignUp", _signUpViewModel);
    }

    [Route("login")]
    public IActionResult Login()
    {
        var formData = new LoginFormModel();
        return View(formData);
    }

    [HttpPost]
    [Route("login")]
    public IActionResult HandleLogin(LoginFormModel formData)
    {
        if (!ModelState.IsValid)
            return View("Login", formData);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        return RedirectToAction("Index", "Home");
    }
}
