using System.Linq;
using viacinema.Data;
using Microsoft.AspNetCore.Mvc;
using viacinema.Models;

namespace viacinema.Controllers.Api
{
    [Route("api/movies")]
    public class MoviesController : Controller
    {
        public ApplicationDbContext context;

        public MoviesController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult GetAllMovies()
        {   //getting all movies, most recent movies are first
            var movies = context.Movies
                .OrderByDescending(c => c.ReleaseDate)
                .ToList();

            return Ok(movies);
        }

        [HttpGet("{id}", Name = "GetMovie")]
        public IActionResult GetMovie(int id)
        {
            var movieInDb = context.Movies
                .SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();

            return Ok(movieInDb);
        }

        [HttpPost]
        public IActionResult AddMovie([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Movies.Add(movie);
            context.SaveChanges();

            return Created(Request.Host + Request.Path + "/" + movie.Id, movie);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] Movie movie)
        {
            var movieInDb = context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();

            context.SaveChanges();

            return Ok(movieInDb);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var movieInDb = context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();

            context.Movies.Remove(movieInDb);

            context.SaveChanges();

            return Ok();

        }

    }
}
