using System.Linq;
using viacinema.Data;
using Microsoft.AspNetCore.Mvc;
using viacinema.Models;

namespace viacinema.Controllers.Api
{
    [Route("api/seats")]
    public class SeatsController : Controller
    {
        public ApplicationDbContext context;

        public SeatsController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult GetAllSeats()
        {   //seats with the highest price are on top
            var seats = context.Seats
                .OrderByDescending(c => c.Price)
                .ToList();

            return Ok(seats);
        }

        [HttpGet("seatsroom/{roomNo}")]
        public IActionResult GetSeatsForRoom(int roomNo)
        {   //receive seats for a specific room
            var seats = context.Seats
                .Where(s => s.RoomNo == roomNo)
                .OrderBy(s => s.SeatNo)
                .ToList();

            return Ok(seats);
        }

        [HttpGet("seatsscreening/{screeningID}/{roomNo}")]
        public IActionResult GetSeatsForScreening(int screeningID, int roomNo)
        {   //getting the seats for a specific screening and room
            var seats = context.SeatScreeningMediator
                .Where(s => s.ScreeningId == screeningID && s.RoomNo == roomNo)
                .OrderBy(s => s.SeatNo)
                .ToList();

            return Ok(seats);
        }

        [HttpGet("{id}", Name = "GetSeat")]
        public IActionResult GetSeat(int id)
        {
            var seatInDb = context.Seats
                .SingleOrDefault(c => c.Id == id);

            if (seatInDb == null)
                return NotFound();

            return Ok(seatInDb);
        }

        [HttpPost]
        public IActionResult AddSeat([FromBody] Seat seat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Seats.Add(seat);
            context.SaveChanges();

            return Created(Request.Host + Request.Path + "/" + seat.Id, seat);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSeat(int id, [FromBody] Seat seat)
        {
            var seatInDb = context.Seats.SingleOrDefault(c => c.Id == id);

            if (seatInDb == null)
                return NotFound();

            context.SaveChanges();

            return Ok(seatInDb);
        }

    }
}
