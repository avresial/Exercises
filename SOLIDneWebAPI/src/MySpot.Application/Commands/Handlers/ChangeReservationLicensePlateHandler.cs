using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.Services;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;
internal class ChangeReservationLicensePlateHandler : ICommandHandler<ChangeReservationLicensePlate>
{
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpots;
    private readonly IClock _clock;

    public ChangeReservationLicensePlateHandler(IWeeklyParkingSpotRepository weeklyParkingSpots, IClock clock)
    {
        _weeklyParkingSpots = weeklyParkingSpots;
        _clock = clock;
    }

    public async Task HandleAsync(ChangeReservationLicensePlate command)
    {
        var weeklySpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);

        if (weeklySpot is null)
            throw new WeeklyParkingSpotNotFound(weeklySpot.Id);

        var existingReservation = weeklySpot.Reservations.OfType<VehicleReservation>().FirstOrDefault(x => x.Id.Value == command.ReservationId);

        if (existingReservation is null)
            throw new Exception("Reservation does not exist");

        existingReservation.ChangeLicensePlate(command.LicensePlate);

        if (existingReservation.Date <= new Date(_clock.Current()))
            throw new Exception("Date is invalid");

        await _weeklyParkingSpots.UpdateAsync(weeklySpot);
    }
    private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservationAsync(ReservationId reservationId) =>
       (await _weeklyParkingSpots.GetAllAsync())
       .SingleOrDefault(x => x.Reservations.Any(y => y.Id.Value == reservationId.Value));
}
