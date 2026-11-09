using System;
using LibraryApi.Data;
using LibraryApi.DTOs.Books;
using LibraryApi.Models;
using LibraryApi.Services.Interfaces;
using LibraryApi.Services.Results;
using Microsoft.EntityFrameworkCore;



namespace LibraryApi.Services;

public sealed class BookService: IBookService

{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<BookService> _logger;

    public BookService(AppDbContext dbContext, ILogger<BookService> logger)
    {
            _dbContext = dbContext;
            _logger = logger;
    }

    public async Task<IReadOnlyList<BookResponseDto>> GetAllAsync(
        CancellationToken cancellationToken
    )
    {
        return await _dbContext.Books 
        .AsNoTracking()
        .Select(book => new BookResponseDto(
                book.Id,
                book.Title,
                book.Genre,
                book.Price,
                book.ReleaseDate,
                book.AuthorId)).ToListAsync(cancellationToken);
                
            
    }
public async Task<BookResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken ) {
    return await _dbContext.Books
    .AsNoTracking()
    .Where(book => book.Id == id)
    .Select(book => new BookResponseDto(
        book.Id,
                book.Title,
                book.Genre,
                book.Price,
                book.ReleaseDate,
                book.AuthorId)).SingleOrDefaultAsync(cancellationToken);
                
  
}

public async Task<BookResponseDto> CreateAsync(CreateBookDTO request, CancellationToken cancellationToken)
    {
       var book = new Book
       {
           Title = request.Title,
           Genre = request.Genre,
           Price = request.Price,
           ReleaseDate = request.ReleaseDate
       };

       _dbContext.Books.Add(book);
       await _dbContext.SaveChangesAsync(cancellationToken);

       _logger.LogInformation(
         new EventId(1001, "BookCreated"),
        "Book created successfully. BookId: {BookId}",
        book.Id
);

       return new BookResponseDto(
        book.Id,
        book.Title,
        book.Genre,
        book.Price,
        book.ReleaseDate,
        book.AuthorId

       );
        
    }

 public async Task<UpdateBookResult> UpdateAsync(
    int id,
    UpdateBookDTO request,
    CancellationToken cancellationToken)
{
    var book = await _dbContext.Books
        .SingleOrDefaultAsync(
            book => book.Id == id,
            cancellationToken
        );

    if (book is null)
    {
        return new UpdateBookResult(
            UpdateBookStatus.BookNotFound,
            null
        );
    }

    if (request.AuthorId.HasValue)
    {
        var authorExists = await _dbContext.Authors
            .AnyAsync(
                author => author.Id == request.AuthorId.Value,
                cancellationToken
            );

        if (!authorExists)
        {
            return new UpdateBookResult(
                UpdateBookStatus.AuthorNotFound,
                null
            );
        }
    }

    book.Title = request.Title;
    book.Genre = request.Genre;
    book.Price = request.Price;
    book.ReleaseDate = request.ReleaseDate;
    book.AuthorId = request.AuthorId;

    await _dbContext.SaveChangesAsync(cancellationToken);

    var response = new BookResponseDto(
        book.Id,
        book.Title,
        book.Genre,
        book.Price,
        book.ReleaseDate,
        book.AuthorId
    );

    return new UpdateBookResult(
        UpdateBookStatus.Updated,
        response
    );
}
public async Task<bool>  DeleteAsync(
        int id,
        CancellationToken cancellationToken
    )
    {
        var book = await _dbContext.Books
        .SingleOrDefaultAsync(
            book => book.Id == id,
            cancellationToken
        );

        if (book is null)
        {
            return false;
        }
        _dbContext.Books.Remove(book);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    

    }
    
}
