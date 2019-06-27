using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurant.Reservations.Core.Models;
using Restaurant.Reservations.Core.Services;

namespace Restaurant.Reservations.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationProvider _reservationProvider;

        public ReservationService(IReservationProvider reservationProvider)
        {
            this._reservationProvider = reservationProvider ?? throw new ArgumentNullException(nameof(reservationProvider));
        }

        public async Task<ICollection<Reservation>> GetAllAsync(Guid accountId)
        {
            return await this._reservationProvider.GetAllAsync(accountId);
        }

        public async Task<Reservation> CreateAsync(InputModels.Reservation reservation)
        {
            // Confirm we have a reservation
            if (reservation == null)
            {
                throw new ArgumentNullException();
            }

            // Confirm this is not within 4 hours of another reservation
            var conflictingReservations = await this._reservationProvider.GetWithinRange(reservation.AccountId, reservation.ReservationTimeUtc);
            if (conflictingReservations.Count > 0)
            {
                throw new Exception("Existing reservation conflicts with this reservation.");
            }

            var newReservation = new Reservation(Guid.NewGuid())
            {
                AccountId = reservation.AccountId,
                FirstName = reservation.FirstName,
                LastName = reservation.LastName,
                Notes = reservation.Notes,
                ReservationTimeUtc = reservation.ReservationTimeUtc,
                RestaurantName = reservation.RestaurantName,
                TotalPatrons = reservation.TotalPatrons
            };

            await this._reservationProvider.CreateAsync(newReservation);

            return newReservation;
        }
    }
}
