using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Services;

public class StudentService(ApplicationDbContext dbContext) : IStudentService
{
    public async Task<List<Hobby>> GetAllHobbies()
    {
        return await dbContext.Hobbies.ToListAsync();
    }

    public async Task AddStudent(Student student)
    {
        var selectedHobbies = await dbContext.Hobbies
            .Where(h => student.SelectedHobbyIds.Contains(h.Id))
            .ToListAsync();

        student.Hobbies = selectedHobbies;

        dbContext.Students.Add(student);
        await dbContext.SaveChangesAsync();
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
        var students = await dbContext.Students.ToListAsync();
        return students;
    }

    public async Task<Student?> GetStudentById(int id)
    {
        var student = await dbContext.Students
            .Include(s => s.Hobbies)
            .SingleOrDefaultAsync(s => s.Id == id);
        return student;
    }

    public async Task UpdateStudent(Student student)
    {
        dbContext.Students.Update(student);
        await dbContext.SaveChangesAsync();
    }
}
