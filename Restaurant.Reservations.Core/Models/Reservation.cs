using System;

namespace Restaurant.Reservations.Core.Models
{
    public class Reservation
    {
        private Reservation()
        {
        }

        public Reservation(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; private set; }

        public Guid AccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string RestaurantName { get; set; }

        public DateTime ReservationTimeUtc { get; set; }

        public int TotalPatrons { get; set; }

        public string Notes { get; set; }
    }
}
