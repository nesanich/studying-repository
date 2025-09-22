using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practice.Model;
using static practice.Model.Models;

namespace practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GuestsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Guests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<guests>>> GetGuests()
        {
            return await _context.guests.ToListAsync();
        }

        // GET: api/Guests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<guests>> GetGuest(int id)
        {
            var guest = await _context.guests.FindAsync(id);

            if (guest == null)
            {
                return NotFound();
            }

            return guest;
        }

        //POST: api/Guests
        [HttpPost]
        public async Task<ActionResult<guests>> PostGuest(GuestDto guestDto)
        {
            var guest = new guests
            {
                дата_регистрации = guestDto.дата_регистрации,
                Имя = guestDto.Имя,
                Фамилия = guestDto.Фамилия,
                Телефон = guestDto.Телефон,
                email = guestDto.email
            };

            _context.guests.Add(guest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGuest), new { id = guest.id }, guest);
        }
        //[HttpPost]
        //public async Task<ActionResult<guests>> Create(guests guests)
        //{
        //    _context.guests.Add(guests);
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(GetGuest), new { id = guests.id }, guests);
        //}

        // PUT: api/Guests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuest(int id, guests guest)
        {
            if (id != guest.id)
            {
                return BadRequest();
            }

            _context.Entry(guest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Guests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            var guest = await _context.guests.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }

            _context.guests.Remove(guest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GuestExists(int id)
        {
            return _context.guests.Any(e => e.id == id);
        }
    }
}
