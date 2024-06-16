namespace MySpot.Application.Commands
{
	public record CreateReservationParkingSpot(Guid ParkingSpotId, Guid ReservationId, string EmployeeName, string LicensePlate, DateTime date);
}
