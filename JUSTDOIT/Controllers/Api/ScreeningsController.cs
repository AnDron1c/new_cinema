using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JUSTDOIT.Data;
using Microsoft.AspNetCore.Mvc;
using viacinema.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace viacinema.Controllers.Api
{
    [Route("api/screenings")]
    public class ScreeningsController : Controller
    {
        public ApplicationDbContext context;

        public ScreeningsController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet("{id}", Name = "GetScreening")]
        public IActionResult GetScreening(int id)
        {
            var screeningInDb = context.Screenings
                .SingleOrDefault(s => s.Id == id);

            if (screeningInDb == null)
                return NotFound();

            return Ok(screeningInDb);
        }
    }
}
