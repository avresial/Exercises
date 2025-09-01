using MySpot.Application.Abstractions;
using MySpot.Core.Repositories;
using MySpot.Core.Services;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;
internal class ReserveParkingSpotForCleaningHandler : ICommandHandler<ReserveParkingSpotForCleaning>
{
    private readonly IParkingReservationService _parkingReservationService;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpots;

    public ReserveParkingSpotForCleaningHandler(IParkingReservationService parkingReservationService, IWeeklyParkingSpotRepository weeklyParkingSpots)
    {
        _parkingReservationService = parkingReservationService;
        _weeklyParkingSpots = weeklyParkingSpots;
    }

    public async Task HandleAsync(ReserveParkingSpotForCleaning command)
    {
        Week week = new(command.Date);
        var weeklyParkingSpots = (await _weeklyParkingSpots.GetByWeekAsync(week)).ToList();

        _parkingReservationService.ReserveParkingForCleaning(weeklyParkingSpots, new(command.Date));

        foreach (var parkingSpots in weeklyParkingSpots)
            await _weeklyParkingSpots.UpdateAsync(parkingSpots);
    }
}
