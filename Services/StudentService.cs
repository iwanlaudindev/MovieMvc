using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Services;

public class StudentService(ApplicationDbContext dbContext, ILogger<StudentService> logger) : IStudentService
{
    public async Task<List<Hobby>> GetAllHobbies()
    {
        return await dbContext.Hobbies.ToListAsync();
    }

    public async Task AddStudent(Student student)
    {
        try
        {
            var selectedHobbies = await dbContext.Hobbies
                .Where(h => student.SelectedHobbyIds.Contains(h.Id))
                .ToListAsync();

            student.Hobbies = selectedHobbies;

            dbContext.Students.Add(student);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError($"Error adding student {ex.Message}");
            throw;
        }
    }

    public async Task DeleteStudent(int id)
    {
        var student = await dbContext.Students
            .SingleOrDefaultAsync(s => s.Id == id);
        if (student is null)
        {
            throw new ArgumentException("Student not found");
        }

        dbContext.Students.Remove(student);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Student>> GetAllStudents()
    {
        Task.Delay(10000).Wait();

        var students = await dbContext.Students
            .Include(e => e.Hobbies)
            .Include(e => e.StudentAddreses)
                // .ThenInclude()
            // .Select(e => new Student
            // {
            //     Id = e.Id,
            //     Name = e.Name,
            //     StudentAddreses = e.StudentAddreses.Select(s => new StudentAddres
            //     {
            //         Id = s.Id,
            //         City = s.City,
            //         State = s.State
            //     }).ToList(),
            //     Hobbies = e.Hobbies.Select(h => new Hobby
            //     {
            //         Id = h.Id,
            //         Name = h.Name
            //     }).ToList()
            // })
            .ToListAsync();
        return students;
    }

    public async Task<Student?> GetStudentById(int id)
    {
        var student = await dbContext.Students
            // .FromSqlRaw("SELECT * FROM Students WHERE Id = {0}", id)
            .Include(s => s.Hobbies)
            .Where(s => s.Id == id)
            .SingleOrDefaultAsync();
        return student;
    }

    public async Task UpdateStudent(Student student)
    {
        dbContext.Students.Update(student);
        await dbContext.SaveChangesAsync();
    }
}
