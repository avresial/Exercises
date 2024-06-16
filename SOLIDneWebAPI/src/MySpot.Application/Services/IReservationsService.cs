using MySpot.Application.Commands;

namespace MySpot.Application.Services
{
	public interface IReservationsService
	{
		Guid? Create(CreateReservationParkingSpot command);
		bool Delete(DeleteReservation command);
		ReservationDto Get(Guid id);
		IEnumerable<ReservationDto> GetAllWeekly();
		bool Updtae(ChangeReservationLicensePlate command);
	}
}