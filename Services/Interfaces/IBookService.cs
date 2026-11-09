
using LibraryApi.DTOs.Books;
using LibraryApi.Services.Results;

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

    Task<UpdateBookResult>  UpdateAsync(
        int id,
        UpdateBookDTO request,
        CancellationToken cancellationToken
    );

    Task<bool>  DeleteAsync(
        int id,
        CancellationToken cancellationToken
    );
}
