using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Policies;

internal sealed class ManagerEmployeeReservationPolicy : IReservationPolicy
{
    public bool CanBeApplied(JobTitle jobTitle) => jobTitle == JobTitle.Manager;

    public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
    {
        var totalEmployeeReservations = weeklyParkingSpots
            .SelectMany(spot => spot.Reservations)
            .Count(reservation => reservation.EmployeeName == employeeName);

        return totalEmployeeReservations < 4;
    }
}
