using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurant.Reservations.Core.Models;
using Restaurant.Reservations.Core.Services;
using Restaurant.Reservations.Infrastructure.Data;

namespace Restaurant.Reservations.Infrastructure.Services
{
    public class OpenTableReservationProvider : IReservationProvider
    {
        private readonly ReservationDbContext _context;

        public OpenTableReservationProvider(ReservationDbContext dbContext)
        {
            this._context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ICollection<Reservation>> GetAllAsync(Guid accountId)
        {
            return await this._context.Reservations.Where(x => x.AccountId == accountId).ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(Guid accountId, Guid reservationId)
        {
            return await this._context.Reservations
                .Where(x => x.AccountId == accountId)
                .Where(x => x.Id == reservationId)
                .SingleOrDefaultAsync();
        }

        public async Task CreateAsync(Reservation reservation)
        {
            this._context.Reservations.Add(reservation);

            await this._context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            var existingReservation = await this._context.Reservations
                .Where(x => x.AccountId == reservation.AccountId)
                .Where(x => x.Id == reservation.Id)
                .SingleOrDefaultAsync();

            if (existingReservation == null)
            {
                throw new NullReferenceException();
            }

            this._context.Reservations.Add(reservation);

            await this._context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid accountId, Guid reservationId)
        {
            var existingReservation = await this._context.Reservations
                .Where(x => x.AccountId == accountId)
                .Where(x => x.Id == reservationId)
                .SingleOrDefaultAsync();

            this._context.Reservations.Remove(existingReservation);

            await this._context.SaveChangesAsync();
        }
    }
}
