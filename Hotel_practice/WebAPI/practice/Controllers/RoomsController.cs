using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practice.Model;
using static practice.Model.Models;

namespace practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<rooms>>> GetAll() =>
            await _context.rooms.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<rooms>> GetById(int id)
        {
            var entity = await _context.rooms.FindAsync(id);
            return entity == null ? NotFound() : entity;
        }

        [HttpPost]
        public async Task<ActionResult<rooms>> Create(rooms entity)
        {
            _context.rooms.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = entity.id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, rooms entity)
        {
            if (id != entity.id) return BadRequest();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("Free")]
        public async Task<ActionResult<IEnumerable<roomsDto>>> GetFreeRooms(DateTime checkIn, DateTime checkOut)
        {
            var busyRoomIds = await _context.bookings
                .Where(b => b.Статус == true &&
                    (checkIn < b.дата_выезда && checkOut > b.дата_заезда))
                .Select(b => b.Номер_id)
                .Distinct()
                .ToListAsync();

            var freeRooms = await _context.rooms
                .Where(r => !busyRoomIds.Contains(r.id))
                .Select(r => new roomsDto
                {
                    id = r.id,
                    номер = r.номер,
                    вместимость = r.вместимость,
                    стоимость_за_ночь = r.стоимость_за_ночь,
                    тип_номера = r.тип_номера
                })
                .ToListAsync();

            return Ok(freeRooms);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.rooms.FindAsync(id);
            if (entity == null) return NotFound();
            _context.rooms.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<rooms>>> GetAvailableRooms(DateTime дата_заезда, DateTime дата_выезда)
        {
            var availableRooms = await _context.rooms
                .Where(r => !_context.bookings
                    .Any(b => b.Номер_id == r.id &&
                              дата_заезда < b.дата_выезда && дата_выезда > b.дата_заезда))
                .ToListAsync();

            return Ok(availableRooms);
        }
    }
}
