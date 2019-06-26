using Microsoft.EntityFrameworkCore;
using Restaurant.Reservations.Core.Models;

namespace Restaurant.Reservations.Infrastructure.Data
{
    public class ReservationDbContext : DbContext
    {
        public ReservationDbContext(DbContextOptions<ReservationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Reservation> Reservations { get; set; }
    }
}
