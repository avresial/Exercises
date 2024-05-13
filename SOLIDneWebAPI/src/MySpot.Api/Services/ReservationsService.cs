using MySpot.Api.Entities;

namespace MySpot.Api.Services
{
    public class ReservationsService
    {
        private readonly List<WeeklyParkingSpot> weeklyParkingSpots = new()
        {
            new WeeklyParkingSpot(Guid.NewGuid(),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P1"),
            new WeeklyParkingSpot(Guid.NewGuid(),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P2"),
            new WeeklyParkingSpot(Guid.NewGuid(),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P3"),
            new WeeklyParkingSpot(Guid.NewGuid(),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P4"),
            new WeeklyParkingSpot(Guid.NewGuid(),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P5"),
        };


        public Reservation Get(Guid id) => GetAllWeekly().SingleOrDefault(x => x.Id == id);

        public IEnumerable<Reservation> GetAllWeekly() => weeklyParkingSpots.SelectMany(x => x.Reservations);
        public Guid? Create(Reservation reservation)
        {

            var weeklyParkingSpot = weeklyParkingSpots.FirstOrDefault(x => x.Id == reservation.ParkingSpotId);

            if (weeklyParkingSpot == null) 
                return default;

            weeklyParkingSpot.AddReservation(reservation);

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
        public bool Delete(Guid id)
        {
            var existingReservation = reservations.SingleOrDefault(x => x.Id == id);

            if (existingReservation is null)
                return false;

            reservations.Remove(existingReservation);

            return true;
        }

    }
}
