
namespace LibraryApi.DTOs.Books;

public sealed record BookResponseDto(
    int Id,
    string Title,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate,
    int? AuthorId
);
