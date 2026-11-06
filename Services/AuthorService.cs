using System;
using LibraryApi.Data;
using LibraryApi.DTOs.Authors;
using LibraryApi.Models;
using LibraryApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Services;

public sealed class AuthorService : IAuthorService
{

    private readonly AppDbContext _dbContext;
    private readonly ILogger<AuthorService> _logger;

    public AuthorService(AppDbContext dbContext, ILogger<AuthorService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
   public async Task<AuthorResponseDto> CreateAsync(
    CreateAuthorDto request,
    CancellationToken cancellationToken)
{
    var author = new Author
    {
        Name = request.Name
    };

    _dbContext.Authors.Add(author);

    await _dbContext.SaveChangesAsync(cancellationToken);
    _logger.LogInformation(
        "Author created successfully. AuthorId: {AuthorId}",
        author.Id
    );

    return new AuthorResponseDto(
        author.Id,
        author.Name
    );
}
}