using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovie.Models;
using MvcMovie.Services;

namespace MvcMovie.Controllers
{
    public class StudentController(IStudentService studentService) : Controller
    {
        // GET: StudentController
        public async Task<ActionResult> Index()
        {
            var students = await studentService.GetAllStudents();

            return View(students);
        }

        // GET: StudentController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var student = await studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: StudentController/Create
        public async Task<ActionResult> Create()
        {
            var hobbies = await studentService.GetAllHobbies();
            ViewBag.Hobbies = new SelectList(hobbies, "Id", "Name");
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                var hobbies = await studentService.GetAllHobbies();
                ViewBag.Hobbies = new SelectList(hobbies, "Id", "Name");
                return View(student);
            }

            await studentService.AddStudent(student);
            return RedirectToAction(nameof(Index));
        }

        // GET: StudentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var student = await studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            var studentToUpdate = await studentService.GetStudentById(id);
            if (studentToUpdate == null)
            {
                return NotFound();
            }

            // Update properties except Id
            studentToUpdate.Name = student.Name;
            studentToUpdate.Age = student.Age;
            // Tambahkan properti lain sesuai model Student jika ada

            await studentService.UpdateStudent(studentToUpdate);

            return RedirectToAction(nameof(Index));
        }

    }
}
