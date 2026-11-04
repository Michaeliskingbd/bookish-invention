using System;
using LibraryApi.Data;
using LibraryApi.DTOs.Books;
using LibraryApi.Models;
using LibraryApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace LibraryApi.Services;

public sealed class BookService: IBookService

{
    private readonly AppDbContext _dbContext;

    public BookService(AppDbContext dbContext)
    {
            _dbContext = dbContext;
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
                book.ReleaseDate)).ToListAsync(cancellationToken);
                
            
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
                book.ReleaseDate)).SingleOrDefaultAsync(cancellationToken);
  
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

       return new BookResponseDto(
        book.Id,
        book.Title,
        book.Genre,
        book.Price,
        book.ReleaseDate

       );
        
    }

}
