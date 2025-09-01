using MySpot.Application;
using MySpot.Application.Dtos;
using MySpot.Core.Entities;

namespace MySpot.Infrastructure.DAL.Handlers;
internal static class Extensions
{
    public static WeeklyParkingSpotDto AsDto(this WeeklyParkingSpot weeklyParkingSpot) => new()
    {
        Id = weeklyParkingSpot.Id.ToString(),
        Name = weeklyParkingSpot.Name,
        Capacity = weeklyParkingSpot.Capacity,
        From = weeklyParkingSpot.Week.From.Value.DateTime,
        To = weeklyParkingSpot.Week.To.Value.DateTime,
        Reservations = weeklyParkingSpot.Reservations.Select(r => new ReservationDto()
        {
            Id = r.Id,
            ParkingSpotId = r.ParkingSpotId,
            EmployeeName = r is VehicleReservation vr ? vr.EmployeeName : null,
            Type = r is VehicleReservation ? "vehicle" : "cleaning",
            Date = r.Date.Value.Date,
        })
    };
}
