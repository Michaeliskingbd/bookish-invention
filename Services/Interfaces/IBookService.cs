
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
}
