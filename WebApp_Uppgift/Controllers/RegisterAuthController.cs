using Business.Services;
using Data.Entities;
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
    IAuthService authService,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    RoleManager<IdentityRole> roleManager)

    {
        _authService = authService;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }


        [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    //ChatGPT hjälp med syntax och struktur
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var member = new ApplicationUser
        {
            UserName = model.FormData.Username,
            Email = model.FormData.Email
        };

        var result = await _userManager.CreateAsync(member, model.FormData.Password);

        if (result.Succeeded)
        {

            await _userManager.AddToRoleAsync(member, "Member");

            await _signInManager.SignInAsync(member, isPersistent: false);
            return RedirectToAction("Projects", "ProjectManagement");
        }


        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
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
