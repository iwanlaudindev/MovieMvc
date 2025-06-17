using System;
using MvcMovie.Models;

namespace MvcMovie.DTO;

public class MovieUser
{
    public List<Movie> Movies { get; set; } = [];
    public User? User { get; set; }
}


