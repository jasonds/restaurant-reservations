using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Restaurant.Reservations.Application.Services;
using Restaurant.Reservations.Core.Models;
using Restaurant.Reservations.Core.Services;

namespace Restaurant.Reservations.Application.Tests.Services
{
    [TestClass]
    public class ReservationServiceTests
    {
        private Mock<IReservationProvider> _reservationProvider;

        [TestInitialize]
        public void Initialize()
        {
            this._reservationProvider = new Mock<IReservationProvider>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReservationService_Constructor_ReservationProvider_Null()
        {
            // Act
            var reservationService = new ReservationService(null);

            // Assert
            Assert.Fail("Expected exception was not thrown");
        }

        [TestMethod]
        public void ReservationService_Constructor_Valid()
        {
            // Act
            var reservationService = new ReservationService(this._reservationProvider.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ReservationService_CreateAsync_Model_Null()
        {
            // Arrange
            var reservationService = new ReservationService(this._reservationProvider.Object);

            // Act
            await reservationService.CreateAsync(null);

            // Assert
            Assert.Fail("Expected exception was not thrown");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task ReservationService_CreateAsync_Conflicting_Reservation()
        {
            // Arrange
            var reservation = new InputModels.Reservation
            {
                AccountId = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "User",
                Notes = "Test Note",
                ReservationTimeUtc = DateTime.UtcNow,
                RestaurantName = "Test Restaurant",
                TotalPatrons = 8
            };
            this._reservationProvider
                .Setup(x => x.GetWithinRange(reservation.AccountId, reservation.ReservationTimeUtc))
                .ReturnsAsync(new Collection<Reservation>() { new Reservation(Guid.NewGuid()) });

            var reservationService = new ReservationService(this._reservationProvider.Object);

            // Act
            var result = await reservationService.CreateAsync(reservation);

            // Assert
            Assert.Fail("Expected exception was not thrown");
        }

        [TestMethod]
        public async Task ReservationService_CreateAsync_Valid()
        {
            // Arrange
            var reservation = new InputModels.Reservation
            {
                AccountId = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "User",
                Notes = "Test Note",
                ReservationTimeUtc = DateTime.UtcNow,
                RestaurantName = "Test Restaurant",
                TotalPatrons = 8
            };
            this._reservationProvider
                .Setup(x => x.GetWithinRange(reservation.AccountId, reservation.ReservationTimeUtc))
                .ReturnsAsync(new Collection<Reservation>());

            var reservationService = new ReservationService(this._reservationProvider.Object);

            // Act
            var result = await reservationService.CreateAsync(reservation);
            Assert.IsNotNull(result);
            Assert.AreEqual(reservation.AccountId, result.AccountId);
            Assert.AreEqual(reservation.FirstName, result.FirstName);
            Assert.AreEqual(reservation.LastName, result.LastName);
            Assert.AreEqual(reservation.Notes, result.Notes);
            Assert.AreEqual(reservation.ReservationTimeUtc, result.ReservationTimeUtc);
            Assert.AreEqual(reservation.RestaurantName, result.RestaurantName);
            Assert.AreEqual(reservation.TotalPatrons, result.TotalPatrons);
        }
    }
}
