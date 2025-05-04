using Business.Dtos;
using Data.Entities;
using Data.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

public interface IUserService
{
    Task<UserResult> AddUserToRole(string userId, string roleName);
    Task<UserResult> CreateUserAsync(SignUpFormData formData, string roleName = "User");
    Task<UserResult> GetUserAsync();
}

public class UserService(IUserRepository userRepository, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager) : IUserService
{

    private readonly IUserRepository _userRepository = userRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task<UserResult> GetUserAsync()
    {
        var result = await _userRepository.GetAllAsync();
        if (!result.Success || result.Result == null)
        {
            return new UserResult { Success = false, ErrorMessage = "No users found." };
        }
        return new UserResult
        {
            Success = true,
            StatusCode = result.StatusCode,
            Result = result.Result.Select(e => new User
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                JobTitle = e.JobTitle,
                Email = e.Email,
            }).ToList()
        };
    }

    public async Task<UserResult> AddUserToRole(string userId, string roleName)
    {
        // Tagit hjälp av GitHub Copilot
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return new UserResult { Success = false, ErrorMessage = "User not found." };
        }
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            return new UserResult { Success = false, ErrorMessage = "Role not found." };
        }
        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (!result.Succeeded)
        {
            return new UserResult { Success = false, ErrorMessage = string.Join(", ", result.Errors.Select(e => e.Description)) };
        }
        return new UserResult { Success = true, StatusCode = 200 };
    }

    public async Task<UserResult> CreateUserAsync(SignUpFormData formData, string roleName = "User")
    {
        if (formData == null)
        {
            return new UserResult { Success = false, ErrorMessage = "Form data is null." };
        }

        var emailExistsResult = await _userRepository.ExistsAsync(x => x.Email == formData.Email);
        if (!emailExistsResult.Success)
        {
            return new UserResult { Success = false, ErrorMessage = "Error checking email existence." };
        }

        if (emailExistsResult.Result)
        {
            return new UserResult { Success = false, ErrorMessage = "Email already exists." };
        }
        try
        {
            var user = new UserEntity
            {
                UserName = formData.Username,
                Email = formData.Email,
            };

            var result = await _userManager.CreateAsync(user, formData.Password);
            if (result.Succeeded)
            {
                var addToRoleResult = await AddUserToRole(user.Id, roleName);
            return result.Succeeded
                ? new UserResult { Success = true, StatusCode = 201 }
                : new UserResult { Success = false, StatusCode = 201, ErrorMessage = "User created but not added to role" };
            }
            else
            {
                return new UserResult { Success = false, ErrorMessage = "Unable to add create user."};
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new UserResult { Success = false, ErrorMessage = "An error occurred while creating the user." };
        }
    }


}
