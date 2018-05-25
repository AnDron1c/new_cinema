using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JUSTDOIT.Data;
using Microsoft.AspNetCore.Mvc;
using viacinema.Data;
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
        {
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

            //movieInDb.Title = movie.Title != null ? movie.Title : movieInDb.Title;
            //movieInDb.Description = movie.Description != null ? movie.Description : movieInDb.Description;
            //movieInDb.Genre = movie.Genre != null ? movie.Genre : movieInDb.Genre;
            //movieInDb.Rating = movie.Rating != null ? movie.Rating : movieInDb.Rating;

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
