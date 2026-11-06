using System;

namespace LibraryApi.Models;

public class Author
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;

    public ICollection<Book> Books {get; set;} = new List<Book>();
}


// What does ICollection<Book> mean?
// It represents a collection of books associated with an author.

// For example:
// author.Books


// Could contain:
// Things Fall Apart
// No Longer at Ease
// Arrow of God


// Why use ICollection<Book> instead of List<Book> directly?
// - ICollection<T> exposes the collection interface rather than tying the property to one specific implementation.
// - List<Book> is the actual collection implementation we're initializing.
// Notice the relationship from both sides
// Book:
// public int AuthorId { get; set; }
// public Author Author { get; set; } = null!;


// Author:
// public ICollection<Book> Books { get; set; } = new List<Book>();
// This is called a bidirectional navigation relationship.
