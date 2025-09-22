using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practice.Model;
using static practice.Model.Models;

namespace practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomAssignmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomAssignmentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<room_assignments>>> GetAll() =>
            await _context.room_assignments.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<room_assignments>> GetById(int id)
        {
            var entity = await _context.room_assignments.FindAsync(id);
            return entity == null ? NotFound() : entity;
        }

        [HttpPost]
        public async Task<ActionResult<room_assignments>> Create(room_assignments entity)
        {
            _context.room_assignments.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = entity.Номер_id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, room_assignments entity)
        {
            if (id != entity.Номер_id) return BadRequest();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.room_assignments.FindAsync(id);
            if (entity == null) return NotFound();
            _context.room_assignments.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
