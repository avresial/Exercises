using MySpot.Application.Commands;
using MySpot.Application.Services;
using MySpot.Core.Repositories;
using Shouldly;
using MySpot.Infrastructure.Repositories;
using MySpot.Tests.Unit.Shared;

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
			reservationsService = new ReservationsService(clock, weeklyParkingSpots);
		}

		#endregion


		[Fact]
		public void given_reservation_for_not_taken_date_add_reservation_should_succeed()
		{
			// Arrange
			var parkingSpot = weeklyParkingSpots.GetAll().First();
			var command = new CreateReservationParkingSpot(parkingSpot.Id, Guid.NewGuid(), "John Doe", "XYZ123", DateTime.UtcNow.AddMinutes(4));

			// Act
			var reservationId = reservationsService.Create(command);

			//Assert
			reservationId.ShouldNotBeNull();
			reservationId.Value.ShouldBe(command.ReservationId);
		}
	}
}
