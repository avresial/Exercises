using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;
internal class DeleteReservationHandler : ICommandHandler<DeleteReservation>
{
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpots;

    public DeleteReservationHandler(IWeeklyParkingSpotRepository weeklyParkingSpots)
    {
        _weeklyParkingSpots = weeklyParkingSpots;
    }

    public async Task HandleAsync(DeleteReservation command)
    {
        var weeklySpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);

        if (weeklySpot is null)
            throw new WeeklyParkingSpotNotFound(weeklySpot.Id);

        weeklySpot.RemoveReservation(command.ReservationId);
        await _weeklyParkingSpots.DeleteAsync(weeklySpot);
    }

    private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservationAsync(ReservationId reservationId) =>
       (await _weeklyParkingSpots.GetAllAsync())
       .SingleOrDefault(x => x.Reservations.Any(y => y.Id.Value == reservationId.Value));
}
