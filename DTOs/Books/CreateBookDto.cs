using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs.Books;

public sealed record CreateBookDTO(

    [Required]
    [MaxLength(200)]
    string Title,

    
    [Required]
    [MaxLength(100)]
    string Genre,

    [Range(0.01, 999999999)]
    decimal Price,
    
    DateOnly ReleaseDate
);