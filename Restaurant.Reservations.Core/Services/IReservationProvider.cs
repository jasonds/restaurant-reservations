using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurant.Reservations.Core.Models;

namespace Restaurant.Reservations.Core.Services
{
    public interface IReservationProvider
    {
        Task<ICollection<Reservation>> GetAllAsync(Guid accountId);

        Task<ICollection<Reservation>> GetWithinRange(Guid accountId, DateTime reservationDate);

        Task<Reservation> GetByIdAsync(Guid accountId, Guid reservationId);

        Task<Reservation> CreateAsync(Reservation reservation);

        Task<Reservation> UpdateAsync(Reservation reservation);

        Task DeleteAsync(Guid accountId, Guid reservationId);
    }
}
