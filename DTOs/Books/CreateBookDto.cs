namespace LibraryApi.DTOs.Books;

public sealed record CreateBookDTO(
    string Title,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);