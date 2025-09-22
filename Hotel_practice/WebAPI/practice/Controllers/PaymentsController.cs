using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practice.Model;
using static practice.Model.Models;

namespace practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaymentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<payments>>> GetAll() =>
            await _context.payments.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<payments>> GetById(int id)
        {
            var entity = await _context.payments.FindAsync(id);
            return entity == null ? NotFound() : entity;
        }

        [HttpPost]
        public async Task<ActionResult<payments>> Create(payments entity)
        {
            _context.payments.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = entity.id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, payments entity)
        {
            if (id != entity.id) return BadRequest();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.payments.FindAsync(id);
            if (entity == null) return NotFound();
            _context.payments.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
