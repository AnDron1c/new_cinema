using System.Linq;
using viacinema.Data;
using Microsoft.AspNetCore.Mvc;

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
