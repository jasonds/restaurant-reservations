using System;

namespace Restaurant.Reservations.Application.InputModels
{
    public class Reservation
    {
        public Guid AccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string RestaurantName { get; set; }

        public DateTime ReservationTimeUtc { get; set; }

        public int TotalPatrons { get; set; }

        public string Notes { get; set; }
    }
}
