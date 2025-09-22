using System.Collections.Generic;
using System.Reflection.Emit;
using static practice.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace practice.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }


        public DbSet<guests> guests { get; set; }
        public DbSet<rooms> rooms { get; set; }
        public DbSet<staff> staff { get; set; }
        public DbSet<room_types> room_types { get; set; }
        public DbSet<services> services { get; set; }
        public DbSet<bookings> bookings { get; set; }
        public DbSet<booked_services> booked_services { get; set; }
        public DbSet<room_assignments> room_assignments { get; set; }
        public DbSet<payments> payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Композитный ключ для room_assignments
            modelBuilder.Entity<room_assignments>()
                .HasKey(ra => new { ra.Номер_id, ra.Сотрудник_id, ra.Дата });

            base.OnModelCreating(modelBuilder);
        }
    }
}
