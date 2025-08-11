using MySpot.Application.Commands;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.Services;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly IParkingReservationService _parkingReservationService;
        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpots;

        public ReservationsService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpots, IParkingReservationService parkingReservationService)
        {
            _clock = clock;
            _weeklyParkingSpots = weeklyParkingSpots;
            _parkingReservationService = parkingReservationService;
        }


        public async Task<ReservationDto> GetAsync(Guid id) => (await GetAllWeeklyAsync()).SingleOrDefault(x => x.Id == id);

        public async Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync() => (await _weeklyParkingSpots.GetAllAsync()).SelectMany(x => x.Reservations).Select(x => new ReservationDto()
        {
            Id = x.Id,
            ParkingSpotId = x.ParkingSpotId,
            EmployeeName = x.EmployeeName,
            LicensePlate = x.LicensePlate,
            Date = x.Date.Value.Date,
        });
        public async Task<Guid?> CreateAsync(CreateReservationParkingSpot command)
        {
            ParkingSpotId parkingSpotId = command.ParkingSpotId;
            Week week = new(_clock.Current());
            JobTitle jobTitle = JobTitle.Employee;

            var weeklyParkingSpots = (await _weeklyParkingSpots.GetByWeekAsync(week)).ToList();
            var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);
            if (parkingSpotToReserve is null) return null;

            var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName, command.LicensePlate, (DateTimeOffset)command.date);
            _parkingReservationService.ReserveSpotForVehicle(weeklyParkingSpots, jobTitle, parkingSpotToReserve, reservation);

            await _weeklyParkingSpots.UpdateAsync(parkingSpotToReserve);

            return reservation.Id;
        }
        public async Task<bool> UpdateAsync(ChangeReservationLicensePlate command)
        {
            var weeklySpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);

            if (weeklySpot is null)
                return false;

            var existingReservation = weeklySpot.Reservations.FirstOrDefault(x => x.Id.Value == command.ReservationId);

            if (existingReservation is null)
                return false;

            existingReservation.ChangeLicensePlate(command.LicensePlate);

            if (existingReservation.Date <= _clock.Current())
                return false;

            await _weeklyParkingSpots.UpdateAsync(weeklySpot);

            return true;
        }
        public async Task<bool> DeleteAsync(DeleteReservation command)
        {
            var weeklySpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);

            if (weeklySpot is null)
                return false;

            var existingReservation = weeklySpot.Reservations.FirstOrDefault(x => x.Id.Value == command.ReservationId);

            if (existingReservation is null)
                return false;

            weeklySpot.RemoveReservation(existingReservation);
            await _weeklyParkingSpots.DeleteAsync(weeklySpot);

            return true;
        }
        private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservationAsync(ReservationId reservationId) =>
        (await _weeklyParkingSpots.GetAllAsync())
        .SingleOrDefault(x => x.Reservations.Any(y => y.Id.Value == reservationId.Value));

    }
}
