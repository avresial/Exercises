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
                new (Guid.Parse("00000000-0000-0000-0000-000000000001"),new Week(clock.Current().Value.Date),"P1"),
                new (Guid.Parse("00000000-0000-0000-0000-000000000002"),new Week(clock.Current().Value.Date),"P2"),
                new (Guid.Parse("00000000-0000-0000-0000-000000000003"),new Week(clock.Current().Value.Date),"P3"),
                new (Guid.Parse("00000000-0000-0000-0000-000000000004"),new Week(clock.Current().Value.Date),"P4"),
                new (Guid.Parse("00000000-0000-0000-0000-000000000005"),new Week(clock.Current().Value.Date),"P5"),
            };
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
