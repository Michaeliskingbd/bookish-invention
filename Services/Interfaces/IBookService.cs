
using LibraryApi.DTOs.Books;

namespace LibraryApi.Services.Interfaces;

public interface IBookService
{
    Task<IReadOnlyList<BookResponseDto>> GetAllAsync(
        CancellationToken cancellationToken
    );

    Task<BookResponseDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken
    );

    Task<BookResponseDto>  CreateAsync(
        CreateBookDTO request,
        CancellationToken cancellationToken
    );

    Task<BookResponseDto?>  UpdateAsync(
        int id,
        UpdateBookDTO request,
        CancellationToken cancellationToken
    );
}
