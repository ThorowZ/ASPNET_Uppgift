using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Security;
using System.Threading.Tasks;
using WebApp_Uppgift.Models;

namespace WebApp_Uppgift.Controllers;

public class RegisterAuthController: Controller
{
    private readonly IAuthService _authService;

    
   public IActionResult SignUp()
    {
        return View();
    }
    
    
    [HttpGet]
    [Route("signup")]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        ViewBag.ErrorMessage = null;

        if (!ModelState.IsValid)
            return View(model);

        var signUpFormData = new SignUpFormData
        {
            Username = model.FormData.Username,
            Email = model.FormData.Email,
            Password = model.FormData.Password,
        };

        var result = await _authService.SignUpAsync(signUpFormData);
        if (result.Success)
        {
            return RedirectToAction("SignIn", "Auth");
        }
        else
        {
            ViewBag.ErrorMessage = result.ErrorMessage;
            return View(model);
        }
    }



    //public async Task<IActionResult> SignUp()
    //{
    //    var clients = await _clientService.GetClients();
    //    var model = new SignUpViewModel
    //    {
    //        ClientOptions = clients.Select(c => new SelectListItem
    //        {
    //            Value = c.Id.ToString(),
    //            Text = c.ClientName
    //        }).ToList()
    //    };

    ////    return View(model);
    //}

    [Route("login")]
    public IActionResult Login(string returnUrl = "~/")
    {
        ViewBag.ReturnUrl = returnUrl;

        return View();
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login(LoginFormModel formData, string returnUrl ="~/")
    {
        ViewBag.ReturnUrl = returnUrl;
        ViewBag.ErrorMessage = null;

        if (!ModelState.IsValid)
        {
            return View(formData);
        }
        var loginFormData = new LoginFormData
        {
            Email = formData.Email,
            Password = formData.Password,
            IsPersistent = false
        };
        var result = _authService.SignInAsync(loginFormData);
        if (result.Result.Success)
        {
            return LocalRedirect(returnUrl);

        }
        ViewBag.ErrorMessage = result.Result.ErrorMessage;
        return View(formData);
    }

    public IActionResult Logout()
    {
        return RedirectToAction("Index", "Home");
    }
}
