using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practice.Model;
using static practice.Model.Models;

namespace practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<bookings>>> GetAll() =>
        //    await _context.bookings.ToListAsync();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingViewModel>>> GetBookings()
        {
            var bookings = await _context.bookings
                .Include(b => b.Guest)
                .Include(b => b.Room)
                .Select(b => new BookingViewModel
                {
                    Id = b.id,
                    Дата_заезда = b.дата_заезда,
                    Дата_выезда = b.дата_выезда,
                    Статус = b.Статус,
                    ИмяГостя = b.Guest.Имя,
                    ФамилияГостя = b.Guest.Фамилия,
                    НомерКомнаты = b.Room.номер
                })
                .ToListAsync();

            return bookings;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<bookings>> GetById(int id)
        {
            var entity = await _context.bookings.FindAsync(id);
            return entity == null ? NotFound() : entity;
        }

        //[HttpPost]
        //public async Task<ActionResult<bookings>> Create(bookings entity)
        //{
        //    _context.bookings.Add(entity);
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(GetById), new { id = entity.id }, entity);
        //}
        [HttpPost]
        public async Task<ActionResult<bookings>> Create(BookingDto dto)
        {
            var booking = new bookings
            {
                Номер_id = dto.Номер_id,
                Гость_id = dto.Гость_id,
                дата_заезда = dto.дата_заезда,
                дата_выезда = dto.дата_выезда,
                Статус = dto.Статус
            };

            _context.bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = booking.id }, booking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, bookings entity)
        {
            if (id != entity.id)
            {
                return BadRequest();
            }

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.bookings.FindAsync(id);
            if (entity == null) return NotFound();
            _context.bookings.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool BookingExists(int id)
        {
            return _context.bookings.Any(e => e.id == id);
        }
        public class BookingViewModel
        {
            public int Id { get; set; }
            public DateTime Дата_заезда { get; set; }
            public DateTime Дата_выезда { get; set; }
            public bool Статус { get; set; }

            public string ИмяГостя { get; set; }
            public string ФамилияГостя { get; set; }
            public int НомерКомнаты { get; set; }
        }
        //[HttpGet("{id}/bill")]
        //public async Task<ActionResult<object>> GetBookingBill(int id)
        //{
        //    var booking = await _context.bookings
        //        .Include(b => b.Номер_id)
        //        .Include(b => b.BookedServices)
        //            .ThenInclude(bs => bs.Service)
        //        .FirstOrDefaultAsync(b => b.id == id);

        //    if (booking == null) return NotFound();

        //    var nights = (booking.дата_заезда - booking.дата_выезда).Days;

        //    // Поле в Room называется Стоимость_за_ночь — у тебя так?
        //    var roomCost = booking.Room.стоимость_за_ночь * nights;

        //    var servicesCost = booking.BookedServices.Sum(bs => bs.Service.Стоимость * bs.Количество);

        //    var total = roomCost + servicesCost;

        //    return Ok(new
        //    {
        //        Nights = nights,
        //        RoomCost = roomCost,
        //        ServicesCost = servicesCost,
        //        Total = total
        //    });
        //}

    }
}
