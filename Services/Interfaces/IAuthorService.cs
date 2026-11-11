using System;
using LibraryApi.DTOs.Authors;

namespace LibraryApi.Services.Interfaces;

public interface IAuthorService
{
Task<AuthorResponseDto>  CreateAsync(
        CreateAuthorDto request,
        CancellationToken cancellationTokenb 
    );
}; 
