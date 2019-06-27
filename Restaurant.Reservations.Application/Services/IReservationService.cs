using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurant.Reservations.Core.Models;

namespace Restaurant.Reservations.Application.Services
{
    public interface IReservationService
    {
        Task<ICollection<Reservation>> GetAllAsync(Guid accountId);

        Task<Reservation> CreateAsync(InputModels.Reservation reservation);
    }
}
