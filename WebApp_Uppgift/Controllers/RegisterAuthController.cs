using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Security;
using System.Threading.Tasks;
using WebApp_Uppgift.Models;

namespace WebApp_Uppgift.Controllers;

public class RegisterAuthController : Controller

{
    private readonly IAuthService _authService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RegisterAuthController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    RoleManager<IdentityRole> roleManager)

    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }


        [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        ViewBag.ErrorMessage = null;

        Console.WriteLine("Form submitted");
        if (!ModelState.IsValid)
            return View(model);

        var signUpFormData = new SignUpFormData
        {
            Username = model.FormData.Username,
            Email = model.FormData.Email,
            Password = model.FormData.Password,
        };

        var result = await _authService.SignUpAsync(signUpFormData);

        if (!result.Success)
        {
            Console.WriteLine("Signup failed: " + result.ErrorMessage);
            ViewBag.ErrorMessage = result.ErrorMessage;
            return View(model);
        }

        Console.WriteLine("Signup succeeded, redirecting to Projects.");
        return RedirectToAction("Projects", "ProjectManagement");
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
