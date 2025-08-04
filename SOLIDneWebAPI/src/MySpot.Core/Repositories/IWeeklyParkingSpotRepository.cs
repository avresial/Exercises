using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Repositories
{
    public interface IWeeklyParkingSpotRepository
    {
        Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id);
        Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync();

        Task AddAsync(WeeklyParkingSpot parkingSpot);
        Task UpdateAsync(WeeklyParkingSpot parkingSpot);
        Task DeleteAsync(WeeklyParkingSpot parkingSpot);
    }
}
