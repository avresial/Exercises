using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities
{
    public class WeeklyParkingSpot
    {
        private readonly HashSet<Reservation> reservations = new();

        public ParkingSpotId Id { get; private set; }
        public Week week { get; private set; }
        public ParkingSpotName Name { get; private set; }

        public IEnumerable<Reservation> Reservations => reservations;
        public WeeklyParkingSpot(ParkingSpotId id, Week week, ParkingSpotName name)
        {
            Id = id;
            this.week = week;
            Name = name;
        }

        internal void AddReservation(Reservation reservation, Date now)
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
