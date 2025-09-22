using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practice.Model;
using static practice.Model.Models;

namespace practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookedServicesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookedServicesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<booked_services>>> GetAll() =>
            await _context.booked_services.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<booked_services>> GetById(int id)
        {
            var entity = await _context.booked_services.FindAsync(id);
            return entity == null ? NotFound() : entity;
        }

        [HttpPost]
        public async Task<ActionResult<booked_services>> Create(booked_services entity)
        {
            _context.booked_services.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = entity.id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, booked_services entity)
        {
            if (id != entity.id) return BadRequest();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.booked_services.FindAsync(id);
            if (entity == null) return NotFound();
            _context.booked_services.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
