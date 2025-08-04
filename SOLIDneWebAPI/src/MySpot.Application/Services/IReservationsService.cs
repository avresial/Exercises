using MySpot.Application.Commands;

namespace MySpot.Application.Services
{
    public interface IReservationsService
    {
        Task<Guid?> CreateAsync(CreateReservationParkingSpot command);
        Task<bool> DeleteAsync(DeleteReservation command);
        Task<ReservationDto> GetAsync(Guid id);
        Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync();
        Task<bool> UpdateAsync(ChangeReservationLicensePlate command);
    }
}