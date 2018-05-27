using System;
using System.Collections.Generic;
using System.Linq;
using viacinema.Data;
using Microsoft.AspNetCore.Mvc;
using viacinema.Models;
using viacinema.ViewModels;

namespace viacinema.Controllers
{
    [Route("moreinfo")]
    public class MoreInfoController : Controller
    {
        public ApplicationDbContext context;

        public MoreInfoController(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        //Movie ID received from homepage and used 
        //to get the movie and find its corresponding, future screenings for MoreInfo (Specifics) page
        [Route("{id}")]
        public IActionResult Index(int id)
        {
            var movie = context.Movies
                .Single(m => m.Id == id);
            var screenings = context.Screenings
                .Where(s => s.MovieId == id && DateTime.Now < s.StartTime) // gets screenings that are only in future
                .OrderByDescending(s => s.StartTime)
                .ToList();

            return View(new MoreInfoViewModel(movie, screenings));
        }
    }
}
