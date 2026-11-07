using LibraryApi.DTOs.Authors;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public sealed class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AuthorResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthorResponseDto>> Create(
        CreateAuthorDto request,
        CancellationToken cancellationToken)
    {
        var author = await _authorService.CreateAsync(
            request,
            cancellationToken
        );

        return StatusCode(StatusCodes.Status201Created, author);
    }
}