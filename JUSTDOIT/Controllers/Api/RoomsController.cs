using System.Linq;
using viacinema.Data;
using Microsoft.AspNetCore.Mvc;
using viacinema.Models;

namespace viacinema.Controllers.Api
{
    [Route("api/rooms")]
    public class RoomsController : Controller
    {
        public ApplicationDbContext context;

        public RoomsController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult GetAllRooms()
        {
            var rooms = context.Rooms
                .OrderByDescending(c => c.roomNo)
                .ToList();

            return Ok(rooms);
        }

        [HttpPost]
        public IActionResult AddRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Rooms.Add(room);
            context.SaveChanges();

            return Created(Request.Host + Request.Path + "/" + room.Id, room);
        }

        [HttpGet("{roomNo}", Name = "GetRoom")]
        public IActionResult GetRoom(int roomNo)
        {
            var roomInDb = context.Rooms
                .SingleOrDefault(c => c.roomNo == roomNo);

            if (roomInDb == null)
                return NotFound();

            return Ok(roomInDb);
        }

        [HttpPut("{roomNo}")]
        public IActionResult UpdateRoom(int roomNo, [FromBody] Room room)
        {
            var roomInDb = context.Rooms.SingleOrDefault(c => c.roomNo == roomNo);

            if (roomInDb == null)
                return NotFound();

            context.SaveChanges();

            return Ok(roomInDb);
        }

    }
}
