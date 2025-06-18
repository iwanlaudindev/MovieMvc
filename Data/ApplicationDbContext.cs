using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Hobby> Hobbies { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Student> Students { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Student>()
            .ToTable("Students")
            .HasMany(e => e.Hobbies)
            .WithMany(e => e.Students)
            .UsingEntity(x => x.ToTable("StudentHobbies"));
        modelBuilder.Entity<Movie>().ToTable("Movies");
    }
}
