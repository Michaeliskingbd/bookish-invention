using System;

namespace LibraryApi.Models;

public class Book
{
public int Id {get; set;}
public string Title {get; set;} = string.Empty;
public string Genre {get; set;} = string.Empty;

public decimal Price {get; set;}

public DateOnly ReleaseDate {get; set;}

public int AuthorId { get; set; }

public Author Author { get; set; } = null!;

}

// AuthorId — Foreign key
// public int AuthorId { get; set; }


// This stores the ID of the author associated with the book.


// Author — Navigation property
// public Author Author { get; set; } = null!;


// This allows EF Core to represent the related author as an object.

// Instead of only accessing:
// book.AuthorId


// You can access:
// book.Author.Name


// null! tells the C# compiler that EF Core or your application will populate this non-nullable navigation property when appropriate. It does not guarantee that the relationship is loaded.

