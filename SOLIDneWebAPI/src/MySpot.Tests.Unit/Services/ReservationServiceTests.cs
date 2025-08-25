using MySpot.Application.Commands;
using MySpot.Application.Services;
using MySpot.Core.Policies;
using MySpot.Core.Repositories;
using MySpot.Core.Services;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.Tests.Unit.Shared;
using Shouldly;

namespace MySpot.Tests.Unit.Services
{
    public class ReservationServiceTests
    {
        #region Arrange
        private readonly IClock clock;
        private readonly IWeeklyParkingSpotRepository weeklyParkingSpots;
        private readonly IReservationsService reservationsService;

        public ReservationServiceTests()
        {
            clock = new TestClock();
            weeklyParkingSpots = new InMemoryWeeklyParkingSpotRepository(clock);
            var parkingReservationService = new ParkingReservationService(new IReservationPolicy[] {
            new BossEmployeeReservationPolicy(),
            new ManagerEmployeeReservationPolicy(),
            new RegularEmployeeReservationPolicy(clock)
            }, clock);
            reservationsService = new ReservationsService(clock, weeklyParkingSpots, parkingReservationService);
        }

        #endregion


        [Fact]
        public async Task given_reservation_for_not_taken_date_add_reservation_should_succeed()
        {
            // Arrange
            var parkingSpot = (await weeklyParkingSpots.GetAllAsync()).First();
            var command = new ReserveParkingSpotForVehicle(parkingSpot.Id, Guid.NewGuid(), 2, "John Doe", "XYZ123", clock.Current().AddMinutes(4));

            // Act
            var reservationId = await reservationsService.ReserveForVehicleAsync(command);

            //Assert
            reservationId.ShouldNotBeNull();
            reservationId.Value.ShouldBe(command.ReservationId);
        }
    }
}
