using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models;

public class Hobby
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    // Relasi ke Student
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
