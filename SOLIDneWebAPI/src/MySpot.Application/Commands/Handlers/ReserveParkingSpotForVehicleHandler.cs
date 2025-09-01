using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.Services;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;
internal class ReserveParkingSpotForVehicleHandler : ICommandHandler<ReserveParkingSpotForVehicle>
{
    private readonly IClock _clock;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpots;
    private readonly IParkingReservationService _parkingReservationService;

    public ReserveParkingSpotForVehicleHandler(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpots,
        IParkingReservationService parkingReservationService)
    {
        _clock = clock;
        _weeklyParkingSpots = weeklyParkingSpots;
        _parkingReservationService = parkingReservationService;
    }

    public async Task HandleAsync(ReserveParkingSpotForVehicle command)
    {
        ParkingSpotId parkingSpotId = command.ParkingSpotId;
        Week week = new(_clock.Current());
        JobTitle jobTitle = JobTitle.Employee;

        var weeklyParkingSpots = (await _weeklyParkingSpots.GetByWeekAsync(week)).ToList();
        var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);
        if (parkingSpotToReserve is null) throw new WeeklyParkingSpotNotFound(parkingSpotId);

        var reservation = new VehicleReservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName, command.LicensePlate, command.Capacity, (DateTimeOffset)command.date);
        _parkingReservationService.ReserveSpotForVehicle(weeklyParkingSpots, jobTitle, parkingSpotToReserve, reservation);

        await _weeklyParkingSpots.UpdateAsync(parkingSpotToReserve);
    }
}
