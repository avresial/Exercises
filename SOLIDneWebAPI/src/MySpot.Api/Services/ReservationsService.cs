using MySpot.Api.Models;

namespace MySpot.Api.Services
{
    public class ReservationsService
    {
        private static readonly List<Reservation> reservations = new();
        private readonly List<string> parkingSpotNames = new()
        {
            "P1", "P2", "P3", "P4", "P5"
        };

        private static int id = 1;

        public Reservation Get(int id) => reservations.SingleOrDefault(x => x.Id == id);

        public IEnumerable<Reservation> GetAll() => reservations;
        public int? Create(Reservation reservation)
        {
            var now = DateTime.UtcNow;
            var pastDays = now.DayOfWeek is DayOfWeek.Sunday ? 7 : (int)now.DayOfWeek;
            var remainingDays = 7 - pastDays;

            if (parkingSpotNames.All(x => x != reservation.ParkingSpotName))
                return default;

            if (!(reservation.Date.Date >= now && reservation.Date.Date <= now.AddDays(remainingDays)))
                return default;

            if (reservations.Any(x =>
                x.ParkingSpotName == reservation.ParkingSpotName &&
                x.Date == reservation.Date))
                return default;

            reservation.Id = id++;
            reservations.Add(reservation);

            return reservation.Id;
        }

        public bool Updtae(Reservation reservation)
        {
            var existingReservation = reservations.SingleOrDefault(x => x.Id == reservation.Id);

            if (existingReservation is null)
                return false;

            if (existingReservation.Date <= DateTime.UtcNow)
                return false;

            existingReservation.LicensePlate = reservation.LicensePlate;

            return true;
        }
        public bool Delete(int id)
        {
            var existingReservation = reservations.SingleOrDefault(x => x.Id == id);

            if (existingReservation is null)
                return false;

            reservations.Remove(existingReservation);

            return true;
        }

    }
}
