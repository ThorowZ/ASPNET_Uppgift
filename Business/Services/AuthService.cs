using Azure.Core;
using Business.Dtos;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using MySqlX.XDevAPI.Common;
using System.Runtime.CompilerServices;
using WebApp_Uppgift.Models;

namespace Business.Services;

public interface IAuthService
{
    Task<AuthResult> SignInAsync(LoginFormData formData);
    Task<AuthResult> SignOutAsync();
    Task<AuthResult> SignUpAsync(SignUpFormData formData);
}

public class AuthService(IUserService userService, SignInManager<ApplicationUser> loginManager) : IAuthService
{
    private readonly IUserService _userService = userService;
    private readonly SignInManager<ApplicationUser> _signInManager = loginManager;


    public async Task<AuthResult> SignInAsync(LoginFormData formData)
    {
        if (formData == null)
            return new AuthResult { Success = false, StatusCode = 400, ErrorMessage = "Form data is null" };

        var result = await _signInManager.PasswordSignInAsync(formData.Email, formData.Password, formData.IsPersistent, lockoutOnFailure: false);
        return result.Succeeded
        ? new AuthResult { Success = true, StatusCode = 200 }
        : new AuthResult { Success = false, StatusCode = 401, ErrorMessage = "Invalid login attempt" };
    }
    public async Task<AuthResult> SignUpAsync(SignUpFormData formData)
    {
        if (formData == null)
            return new AuthResult { Success = false, StatusCode = 400, ErrorMessage = "Form data is null" };

        var result = await _userService.CreateUserAsync(formData);
        return result.Success
                ? new AuthResult { Success = true, StatusCode = 201 }
                : new AuthResult { Success = false, StatusCode = 400, ErrorMessage = "User creation failed" };
    }

    public async Task<AuthResult> SignOutAsync()
    {
        await _signInManager.SignOutAsync();
        return new AuthResult { Success = true, StatusCode = 200 };
    }

}
