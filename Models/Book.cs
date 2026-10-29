using System;

namespace LibraryApi.Models;

public class Book
{
public int Id {get; set;}
public string Title {get; set;} = string.Empty;
public string Genre {get; set;} = string.Empty;

public decimal Price {get; set;}

public DateOnly ReleaseDate {get; set;}

}
