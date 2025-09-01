using MySpot.Application.Abstractions;

namespace MySpot.Application.Commands;

public record ReserveParkingSpotForVehicle(Guid ParkingSpotId, Guid ReservationId, int Capacity,
    string EmployeeName, string LicensePlate, DateTime date) : ICommand;
