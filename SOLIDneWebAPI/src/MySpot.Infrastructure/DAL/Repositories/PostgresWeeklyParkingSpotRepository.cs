using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Repositories
{
    internal class PostgresWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
    {
        private readonly MySpotDbContext mySpotDbContext;
        public PostgresWeeklyParkingSpotRepository(MySpotDbContext mySpotDbContext)
        {
            this.mySpotDbContext = mySpotDbContext;
        }

        public async Task AddAsync(WeeklyParkingSpot parkingSpot)
        {
            await mySpotDbContext.AddAsync(parkingSpot);
            await mySpotDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(WeeklyParkingSpot parkingSpot)
        {
            mySpotDbContext.Remove(parkingSpot);
            await mySpotDbContext.SaveChangesAsync();
        }

        public Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id)
        => mySpotDbContext.WeeklyParkingSpots
                            .Include(x => x.Reservations)
                            .SingleAsync(x => x.Id == id);

        public async Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync()
        {
            var result = await mySpotDbContext.WeeklyParkingSpots
                                .Include(x => x.Reservations)
                                .ToListAsync();

            return result.AsEnumerable();
        }

        public async Task UpdateAsync(WeeklyParkingSpot parkingSpot)
        {
            mySpotDbContext.Update(parkingSpot);
            await mySpotDbContext.SaveChangesAsync();
        }
    }
}
