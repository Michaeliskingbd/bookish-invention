
using LibraryApi.DTOs.Auth;
using LibraryApi.Models;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LibraryApi.Services;

public sealed class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }


public async Task<IdentityResult> RegisterAsync(
    RegisterRequestDto request,
    CancellationToken cancellationToken)
{
    cancellationToken.ThrowIfCancellationRequested();

    var user = new ApplicationUser
    {
        UserName = request.Email,
        Email = request.Email
    };

    return await _userManager.CreateAsync(user, request.Password);
}

}

