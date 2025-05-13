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
    IAuthService  authService,
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
    public async Task<IActionResult> SignUp(SignUpFormModel model)
    {
        Console.WriteLine("SignUp Post");
        if (!ModelState.IsValid) 
        {
            Console.WriteLine("ModelState is not valid");
        return View(model);
        }

        var member = new ApplicationUser
        {
            UserName = model.Username,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(member, model.Password);

        Console.WriteLine("Successful");
        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync("Member"))
                await _roleManager.CreateAsync(new IdentityRole("Member"));

            await _userManager.AddToRoleAsync(member, "Member");
            await _signInManager.SignInAsync(member, isPersistent: false);

            Console.WriteLine("Signup succeeded—redirecting to Projects");
            return RedirectToAction("Login", "RegisterAuth");
        }

        foreach (var err in result.Errors)
            ModelState.AddModelError("", err.Description);

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

    [HttpGet]
    //[Route("login")]
    public IActionResult Login()
    {
   
        return View();
    }

    [HttpPost]
    //[Route("login")]
    public IActionResult Login(LoginFormModel formData)
    {
        ViewBag.ErrorMessage = null;

        Console.WriteLine("Post");
        if (!ModelState.IsValid)
        {
            Console.WriteLine("ModelState is not valid");
            return View(formData);
        }
        Console.WriteLine("ModelState is valid");
        var loginFormData = new LoginFormData
        {
            Email = formData.Email,
            Password = formData.Password,
            IsPersistent = false
        };
        Console.WriteLine("LoginFormData created");
        var result = _authService.SignInAsync(loginFormData);
        if (result.Result.Success)
        {
            return RedirectToAction("projects", "ProjectManagement");

        }
        ViewBag.ErrorMessage = result.Result.ErrorMessage;
        return View(formData);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
