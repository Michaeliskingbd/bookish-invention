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

[HttpPost("login")]
public async Task<IActionResult> Login(
    LoginRequestDto request,
    CancellationToken cancellationToken)
{
    var succeeded = await _authService.LoginAsync(
        request,
        cancellationToken);

    if (!succeeded)
    {
        return Unauthorized(new
        {
            message = "Invalid email or password."
        });
    }

    return Ok(new
    {
        message = "Login successful."
    });
}

}