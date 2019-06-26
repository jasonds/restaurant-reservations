using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurant.Reservations.Core.Models;

namespace Restaurant.Reservations.Core.Services
{
    public interface IReservationProvider
    {
        Task<ICollection<Reservation>> GetAllAsync(Guid accountId);

        Task<Reservation> GetByIdAsync(Guid accountId, Guid reservationId);

        Task CreateAsync(Reservation reservation);

        Task UpdateAsync(Reservation reservation);

        Task DeleteAsync(Guid accountId, Guid reservationId);
    }
}
