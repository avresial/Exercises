using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;
using Shouldly;

namespace MySpot.Tests.Unit.Services
{
	public class ReservationServiceTests
	{
		#region Arrange
		private readonly IClock clock = new Clock();
		private readonly ReservationsService reservationsService;
		private readonly List<WeeklyParkingSpot> weeklyParkingSpots;

		public ReservationServiceTests()
		{
			weeklyParkingSpots = new()
			{
				new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"),new Week(clock.Current().Value.Date),"P1"),
				new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"),new Week(clock.Current().Value.Date),"P2"),
				new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"),new Week(clock.Current().Value.Date),"P3"),
				new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"),new Week(clock.Current().Value.Date),"P4"),
				new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"),new Week(clock.Current().Value.Date),"P5"),
			};
			reservationsService = new ReservationsService(weeklyParkingSpots);
		}

		#endregion


		[Fact]
		public void given_reservation_for_not_taken_date_add_reservation_should_succeed()
		{
			// Arrange
			var parkingSpot = weeklyParkingSpots.First();
			var command = new CreateReservationParkingSpot(parkingSpot.Id, Guid.NewGuid(), "John Doe", "XYZ123", DateTime.UtcNow.AddMinutes(4));

			// Act
			var reservationId = reservationsService.Create(command);

			//Assert
			reservationId.ShouldNotBeNull();
			reservationId.Value.ShouldBe(command.ReservationId);
		}
	}
}
