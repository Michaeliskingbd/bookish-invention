using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs.Authors;

public sealed record CreateAuthorDto(
    [Required]
    [MaxLength(150)]
    string Name
);