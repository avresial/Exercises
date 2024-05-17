using MySpot.Api.Commands;
using MySpot.Api.Dtos;
using MySpot.Api.Entities;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Services
{
	public class ReservationsService
	{
		public static Clock clock = new();
		private static readonly List<WeeklyParkingSpot> weeklyParkingSpots = new()
		{
			new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"),new Week(clock.Current().Value.Date),"P1"),
			new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"),new Week(clock.Current().Value.Date),"P2"),
			new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"),new Week(clock.Current().Value.Date),"P3"),
			new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"),new Week(clock.Current().Value.Date),"P4"),
			new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"),new Week(clock.Current().Value.Date),"P5"),
		};


		public ReservationDto Get(Guid id) => GetAllWeekly().SingleOrDefault(x => x.Id == id);

		public IEnumerable<ReservationDto> GetAllWeekly() => weeklyParkingSpots.SelectMany(x => x.Reservations).Select(x => new ReservationDto()
		{
			Id = x.Id,
			ParkingSpotId = x.ParkingSpotId,
			EmployeeName = x.EmployeeName,
			LicensePlate = x.LicensePlate,
			Date = x.Date.Value.Date,
		});
		public Guid? Create(CreateReservationParkingSpot command)
		{
			var weeklyParkingSpot = weeklyParkingSpots.FirstOrDefault(x => x.Id.Value == command.ParkingSpotId);

			if (weeklyParkingSpot == null)
				return default;

			var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName, command.LicensePlate, (DateTimeOffset)command.date);
			weeklyParkingSpot.AddReservation(reservation, clock.Current());

			return reservation.Id;
		}

		public bool Updtae(ChangeReservationLicensePlate command)
		{
			var weeklySpot = GetWeeklyParkingSpotByReservation(command.ReservationId);

			if (weeklySpot is null)
				return false;

			var existingReservation = weeklySpot.Reservations.FirstOrDefault(x => x.Id.Value == command.ReservationId);

			if (existingReservation is null)
				return false;

			existingReservation.ChangeLicensePlate(command.LicensePlate);


			if (existingReservation.Date <= clock.Current())
				return false;


			return true;
		}
		public bool Delete(DeleteReservation command)
		{
			WeeklyParkingSpot weeklySpot = GetWeeklyParkingSpotByReservation(command.ReservationId);

			if (weeklySpot is null)
				return false;

			var existingReservation = weeklySpot.Reservations.FirstOrDefault(x => x.Id.Value == command.ReservationId);

			if (existingReservation is null)
				return false;

			weeklySpot.RemoveReservation(existingReservation);

			return true;
		}
		private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(ReservationId reservationId)
		{
			return weeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(y => y.Id == reservationId));
		}
	}
}
