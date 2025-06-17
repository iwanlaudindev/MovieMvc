using Microsoft.AspNetCore.Mvc;
using MvcMovie.DTO;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        // Penyimpanan data sementara (in-memory)
        private static List<Movie> _movieList = new();

        // GET: MoviesController
        [HttpGet]
        public ActionResult Index()
        {
            var resutl = new MovieUser
            {
                Movies = _movieList,
                User = new User
                {
                    Id = 2,
                    Username = "iwanlaudin"
                }
            };

            return View(resutl);
            // return View("List", _movieList); // Views/Movies/List.cshtml
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {
            var isAlreadyTitle = _movieList.Any(e => e.Title == movie.Title);
            if (isAlreadyTitle)
            {
                ModelState.AddModelError(nameof(movie.Title), "Titel already exist");
            }

            if (ModelState.IsValid)
            {
                movie.Id = _movieList.Count + 1;
                _movieList.Add(movie);
                return RedirectToAction(nameof(Details), "Movies", new { id = movie.Id });
            }
            return View(movie);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var movie = _movieList.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _movieList.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Movie updatedMovie)
        {
            if (id != updatedMovie.Id)
            {
                return BadRequest();
            }

            var existingMovie = _movieList.FirstOrDefault(m => m.Id == id);
            if (existingMovie == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                existingMovie.Title = updatedMovie.Title;
                existingMovie.ReleaseDate = updatedMovie.ReleaseDate;
                existingMovie.Genre = updatedMovie.Genre;
                existingMovie.Price = updatedMovie.Price;

                return RedirectToAction(nameof(Index));
            }

            return View(updatedMovie);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var movie = _movieList.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            _movieList.Remove(movie);
            return RedirectToAction(nameof(Index));
        }

        // [HttpGet]
        // public IActionResult Delete(int id)
        // {
        //     var movie = _movieList.FirstOrDefault(m => m.Id == id);
        //     if (movie == null)
        //     {
        //         return NotFound();
        //     }
        //     _movieList.Remove(movie);
        //     return RedirectToAction(nameof(Index));
        // }
    }
}
