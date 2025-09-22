using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practice.Model;
using static practice.Model.Models;

namespace practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomTypesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<room_types>>> GetAll() =>
            await _context.room_types.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<room_types>> GetById(int id)
        {
            var entity = await _context.room_types.FindAsync(id);
            return entity == null ? NotFound() : entity;
        }

        [HttpPost]
        public async Task<ActionResult<room_types>> Create(room_types entity)
        {
            _context.room_types.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = entity.id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, room_types entity)
        {
            if (id != entity.id) return BadRequest();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.room_types.FindAsync(id);
            if (entity == null) return NotFound();
            _context.room_types.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
