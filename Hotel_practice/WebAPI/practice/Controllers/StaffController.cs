using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practice.Model;
using static practice.Model.Models;

namespace practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StaffController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<staff>>> GetAll() =>
            await _context.staff.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<staff>> GetById(int id)
        {
            var entity = await _context.staff.FindAsync(id);
            return entity == null ? NotFound() : entity;
        }

        [HttpPost]
        public async Task<ActionResult<staff>> Create(staff entity)
        {
            _context.staff.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = entity.id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, staff entity)
        {
            if (id != entity.id) return BadRequest();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.staff.FindAsync(id);
            if (entity == null) return NotFound();
            _context.staff.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
