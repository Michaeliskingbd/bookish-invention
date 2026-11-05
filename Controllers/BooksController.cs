
using LibraryApi.DTOs.Books;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<BookResponseDto>),StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<BookResponseDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var books = await _bookService.GetAllAsync(
            cancellationToken
        );

        return Ok(books);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BookResponseDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookResponseDto>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var book = await _bookService.GetByIdAsync(
            id,
            cancellationToken
        );

        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }

[HttpPost]
[ProducesResponseType(typeof(BookResponseDto), StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<BookResponseDto>> Create(
    CreateBookDTO request,
    CancellationToken cancellationToken)
{
    var book = await _bookService.CreateAsync(
        request,
        cancellationToken
    );

    return CreatedAtAction(
        nameof(GetById),
        new { id = book.Id },
        book
    );
}

[HttpPut("{id:int}")]
[ProducesResponseType(typeof(BookResponseDto), StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]

public async Task<ActionResult<BookResponseDto>> Update(
    int id,
    UpdateBookDTO request,
    CancellationToken cancellationToken)
    {
         var book = await _bookService.UpdateAsync(
            id,
            request,
            cancellationToken
        );

        if (book is null)
    {
        return NotFound();
    }

    return Ok(book);
    }
    

[HttpDelete("{id:int}")]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> Delete(
    int id,
    CancellationToken cancellationToken)
{
    var deleted = await _bookService.DeleteAsync(
        id,
        cancellationToken
    );

    if (!deleted)
    {
        return NotFound();
    }

    return NoContent();
}


[HttpGet("test-error")]
public IActionResult TestError()
{
    throw new InvalidOperationException("This is a test exception.");
}

}
