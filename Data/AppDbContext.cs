using System;
using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(book => book.Id);

            entity.Property(book => book.Title)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(book => book.Genre)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(book => book.Price)
                .HasPrecision(12, 2);

            entity.Property(book => book.ReleaseDate)
                .IsRequired();

            entity.HasOne(book => book.Author)
                .WithMany(author => author.Books)
                .HasForeignKey(book => book.AuthorId);
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(author => author.Id);

            entity.Property(author => author.Name)
            .HasMaxLength(150)
            .IsRequired();
        });
    }
}