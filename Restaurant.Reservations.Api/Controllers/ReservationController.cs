using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Reservations.Application.Services;
using Restaurant.Reservations.Core.Models;
using InputModels = Restaurant.Reservations.Application.InputModels;

namespace Restaurant.Reservations.Api.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this._reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
        }

        /// <summary>
        /// Gets reservations
        /// </summary>
        /// <returns>Reservations</returns>
        [HttpGet("")]
        public async Task<ICollection<Reservation>> GetReservationsAsync(Guid accountId)
        {
            return await this._reservationService.GetAllAsync(accountId);
        }
        
        /// <summary>
        /// Creates new reservation
        /// </summary>
        /// <returns>Reservation</returns>
        [HttpPost("")]
        public async Task CreateReservationAsync([FromBody] InputModels.Reservation reservation)
        {
            await this._reservationService.CreateAsync(reservation);
        }
    }
}
