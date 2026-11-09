using LibraryApi.DTOs.Books;

namespace LibraryApi.Services.Results;

public enum UpdateBookStatus
{
    Updated,
    BookNotFound,
    AuthorNotFound
}

public sealed record UpdateBookResult(
    UpdateBookStatus Status,
    BookResponseDto? Book
);