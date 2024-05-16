using MySpot.Api.Exceptions;

namespace MySpot.Api.Entities
{
    public class WeeklyParkingSpot
    {
        private readonly HashSet<Reservation> reservations = new();

        public Guid Id { get; }
        public DateTime From { get; }
        public DateTime To { get; }
        public string Name { get; }

        public IEnumerable<Reservation> Reservations => reservations;
        public WeeklyParkingSpot(Guid id, DateTime from, DateTime to, string name)
        {
            this.reservations = reservations;
            Id = id;
            From = from;
            To = to;
            Name = name;
        }

        public void AddReservation(Reservation reservation, DateTime now)
        {
            var isInvalidDate = (reservation.Date.Date < From.Date || reservation.Date.Date > To || reservation.Date.Date.Date < now);

            if (isInvalidDate)
                throw new InvalidReservationDateException(reservation.Date.Date);

            var reservationAlreadyExists = Reservations.Any(x => x.Date.Date == reservation.Date.Date);

            if (reservationAlreadyExists)
                throw new ParkingSpotAlreadyReservedException(reservation.EmployeeName, reservation.Date.Date);
            
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
