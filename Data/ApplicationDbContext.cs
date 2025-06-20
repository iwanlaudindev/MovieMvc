using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Hobby> Hobbies { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentAddres> StudentAddreses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>()
            .ToTable("Students")
            .HasMany(e => e.Hobbies)
            .WithMany(e => e.Students)
            .UsingEntity(x => x.ToTable("StudentHobbies"));

        modelBuilder.Entity<Movie>().ToTable("Movies");

        var studentAddressBuilder = modelBuilder.Entity<StudentAddres>();
        studentAddressBuilder
            .ToTable("StudentAddreses");
        studentAddressBuilder
            .Property(e => e.City).HasMaxLength(100);
        studentAddressBuilder
            .Property(e => e.State).HasMaxLength(100);
        studentAddressBuilder
            .Property(e => e.ZipCode).HasMaxLength(100);
        studentAddressBuilder
            .Property(e => e.Country).HasMaxLength(100);
        studentAddressBuilder
            .Property(e => e.Street).HasMaxLength(100);
        studentAddressBuilder
            .HasIndex(e => e.StudentId);

        // new index
        studentAddressBuilder
            .HasIndex(e => e.City);
    }
}
