using MySpot.Core.Entities;
using MySpot.Core.Services;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Policies;

internal sealed class RegularEmployeeReservationPolicy : IReservationPolicy
{
    private readonly IClock _clock;

    public RegularEmployeeReservationPolicy(IClock clock)
    {
        _clock = clock;
    }

    public bool CanBeApplied(JobTitle jobTitle) => jobTitle == JobTitle.Employee;

    public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
    {
        var totalEmployeeReservations = weeklyParkingSpots
            .SelectMany(spot => spot.Reservations)
            .Count(reservation => reservation.EmployeeName == employeeName);

        return totalEmployeeReservations < 2 && _clock.Current().Value.Hour > 4;
    }
}
