using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Entities
{
	public class WeeklyParkingSpot
	{
		private readonly HashSet<Reservation> reservations = new();

		public ParkingSpotId Id { get; }
		public Week week { get; }
		public ParkingSpotName Name { get; }

		public IEnumerable<Reservation> Reservations => reservations;
		public WeeklyParkingSpot(ParkingSpotId id, Week week, ParkingSpotName name)
		{
			Id = id;
			this.week = week;
			Name = name;
		}

		public void AddReservation(Reservation reservation, Date now)
		{
			var isInvalidDate = (reservation.Date < week.From || reservation.Date > week.To || reservation.Date < now);

			if (isInvalidDate)
				throw new InvalidReservationDateException(reservation.Date.Value.Date);

			var reservationAlreadyExists = Reservations.Any(x => x.Date == reservation.Date);

			if (reservationAlreadyExists)
				throw new ParkingSpotAlreadyReservedException(reservation.EmployeeName, reservation.Date.Value.Date);

			reservations.Add(reservation);
		}

		public void RemoveReservation(Reservation reservation)
		{
			if (!reservations.Contains(reservation))
				throw new Exception("Reservation does not exist.");

			reservations.Remove(reservation);
		}
	}
}
