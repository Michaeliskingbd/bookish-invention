
using LibraryApi.DTOs.Auth;
using LibraryApi.Models;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LibraryApi.Services;

public sealed class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(UserManager<ApplicationUser> userManager ,     SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
         _signInManager = signInManager;
    }


public async Task<IdentityResult> RegisterAsync(
    RegisterRequestDto request,
    CancellationToken cancellationToken)
{
    cancellationToken.ThrowIfCancellationRequested();

    var user = new ApplicationUser
    {
        UserName = request.Email,
        Email = request.Email,
        PhoneNumber = request.PhoneNumber
    };

    return await _userManager.CreateAsync(user, request.Password);
}



public async Task<bool> LoginAsync(
    LoginRequestDto request,
    CancellationToken cancellationToken)
{
    cancellationToken.ThrowIfCancellationRequested();

    var user = await _userManager.FindByEmailAsync(request.Email);

    if (user is null)
    {
        return false;
    }

    var result = await _signInManager.PasswordSignInAsync(
        user,
        request.Password,
        isPersistent: false,
        lockoutOnFailure: true);

    return result.Succeeded;
}
}

