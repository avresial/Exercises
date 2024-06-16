using MySpot.Core.Entities;
using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;
using Shouldly;

namespace MySpot.Tests.Unit.Entities
{
	public class WeeklyParkingSpotTests
	{
		#region Arrange

		private readonly Date date;
		private readonly WeeklyParkingSpot weeklyParkingSpot;

		public WeeklyParkingSpotTests()
		{
			date = new Date(new DateTime(2024, 05, 17));
			weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new(date), "P1");
		}
		#endregion
		[Fact]
		public void given_reservation_for_not_taken_date_add_reservation_should_succeed()
		{
			// Arrange
			var reservation = new Reservation(Guid.NewGuid(), weeklyParkingSpot.Id, "John Doe", "XYZ123", new Date(date.AddDays(1)));

			// Act
			weeklyParkingSpot.AddReservation(reservation, new Date(date));

			// Assert
			weeklyParkingSpot.Reservations.ShouldHaveSingleItem();
		}

		[Theory]
		[InlineData("2024-04-30")]
		[InlineData("2024-06-01")]
		public void given_invalid_date_add_reservation_should_fail(string dateString)
		{
			// Arrange
			var invalidDate = DateTime.Parse(dateString);

			var reservation = new Reservation(Guid.NewGuid(), weeklyParkingSpot.Id, "John Doe", "XYZ123", new Date(invalidDate));

			// Act
			var exception = Record.Exception(() => weeklyParkingSpot.AddReservation(reservation, new Date(date)));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<InvalidReservationDateException>();
		}

		[Fact]
		public void given_reservation_for_already_existing_date_add_reservation_should_fail()
		{
			// Arrange
			var reservation = new Reservation(Guid.NewGuid(), weeklyParkingSpot.Id, "John Doe", "XYZ123", new Date(date.AddDays(1)));
			weeklyParkingSpot.AddReservation(reservation, new Date(date));

			var nextReservation = new Reservation(Guid.NewGuid(), weeklyParkingSpot.Id, "John Doe", "XYZ123", new Date(date.AddDays(1)));
			// Act
			var exception = Record.Exception(() => weeklyParkingSpot.AddReservation(nextReservation, new Date(date)));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ParkingSpotAlreadyReservedException>();
		}
	}
}
