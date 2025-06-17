using System.ComponentModel.DataAnnotations;
using MvcMovie.Attributes;

namespace MvcMovie.Models;

public class Movie
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title harus diisi.")]
    [ValidateName]
    public string? Title { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Release Date")]
    public DateTime ReleaseDate { get; set; }

    [Required]
    public string? Genre { get; set; }

    public Genre? GenreObject { get; set; }

    [Range(1, 100)]
    public decimal Price { get; set; }
    // [Required]
    public DateTime CreatedAt { get; set; }
}


public class Genre
{
    public int Id { get; set; }
    public string? Titel { get; set; }
}
