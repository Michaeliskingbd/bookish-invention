using LibraryApi.DTOs.Auth;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }



[HttpPost("register")]
public async Task<IActionResult> Register(
    RegisterRequestDto request,
    CancellationToken cancellationToken)
{
    var result = await _authService.RegisterAsync(
        request,
        cancellationToken);

    if (!result.Succeeded)
    {
        return BadRequest(new
        {
            errors = result.Errors.Select(error => error.Description)
        });
    }

    return StatusCode(StatusCodes.Status201Created, new
    {
        message = "Registration successful."
    });
}

}