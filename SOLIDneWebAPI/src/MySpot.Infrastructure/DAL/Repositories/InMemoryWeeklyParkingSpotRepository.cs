using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.Services;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Repositories
{
    internal class InMemoryWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
    {
        private List<WeeklyParkingSpot> _weeklyParkingSpots = new();

        public InMemoryWeeklyParkingSpotRepository(IClock clock)
        {
            _weeklyParkingSpots = new List<WeeklyParkingSpot>()
            {
                WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000001"),new Week(clock.Current().Date),"P1"),
                WeeklyParkingSpot.Create (Guid.Parse("00000000-0000-0000-0000-000000000002"),new Week(clock.Current().Date),"P2"),
                WeeklyParkingSpot.Create (Guid.Parse("00000000-0000-0000-0000-000000000003"),new Week(clock.Current().Date),"P3"),
                WeeklyParkingSpot.Create (Guid.Parse("00000000-0000-0000-0000-000000000004"),new Week(clock.Current().Date),"P4"),
                WeeklyParkingSpot.Create (Guid.Parse("00000000-0000-0000-0000-000000000005"),new Week(clock.Current().Date),"P5"),
            };
        }

        public async Task<IEnumerable<WeeklyParkingSpot>> GetByWeekAsync(Week week)
        {
            var result = _weeklyParkingSpots.Where(x => x.Week == week);
            return await Task.FromResult(result);
        }
        public Task AddAsync(WeeklyParkingSpot parkingSpot)
        {
            _weeklyParkingSpots.Add(parkingSpot);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(WeeklyParkingSpot parkingSpot)
        {
            _weeklyParkingSpots.Remove(parkingSpot);
            return Task.CompletedTask;
        }

        public Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id) => Task.FromResult(_weeklyParkingSpots.FirstOrDefault(x => x.Id == id));

        public Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync() => Task.FromResult(_weeklyParkingSpots.AsEnumerable());

        public Task UpdateAsync(WeeklyParkingSpot parkingSpot) => Task.CompletedTask;

    }
}
