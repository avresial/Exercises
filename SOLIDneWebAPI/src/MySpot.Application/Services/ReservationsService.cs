using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using MySpot.Core.Repositories;
using MySpot.Application.Commands;

namespace MySpot.Application.Services
{
    public class ReservationsService : IReservationsService
	{
		private readonly IClock clock;
		private readonly IWeeklyParkingSpotRepository weeklyParkingSpots;

		public ReservationsService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpots)
		{
			this.clock = clock;
			this.weeklyParkingSpots = weeklyParkingSpots;
		}


		public ReservationDto Get(Guid id) => GetAllWeekly().SingleOrDefault(x => x.Id == id);

		public IEnumerable<ReservationDto> GetAllWeekly() => weeklyParkingSpots.GetAll().SelectMany(x => x.Reservations).Select(x => new ReservationDto()
		{
			Id = x.Id,
			ParkingSpotId = x.ParkingSpotId,
			EmployeeName = x.EmployeeName,
			LicensePlate = x.LicensePlate,
			Date = x.Date.Value.Date,
		});
		public Guid? Create(CreateReservationParkingSpot command)
		{
			var weeklyParkingSpot = weeklyParkingSpots.Get(command.ParkingSpotId);

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
			return weeklyParkingSpots.GetAll().SingleOrDefault(x => x.Reservations.Any(y => y.Id.Value == reservationId.Value));
		}
	}
}
