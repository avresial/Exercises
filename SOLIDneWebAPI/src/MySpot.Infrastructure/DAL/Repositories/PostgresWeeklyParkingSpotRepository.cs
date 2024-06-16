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

        public void Add(WeeklyParkingSpot parkingSpot)
        {
            mySpotDbContext.Add(parkingSpot);
            mySpotDbContext.SaveChanges();
        }

        public void Delete(WeeklyParkingSpot parkingSpot)
        {
            mySpotDbContext.Remove(parkingSpot);
            mySpotDbContext.SaveChanges();
        }

        public WeeklyParkingSpot Get(ParkingSpotId id)
        => mySpotDbContext.WeeklyParkingSpots
                            .Include(x => x.Reservations)
                            .Single(x => x.Id == id);

        public IEnumerable<WeeklyParkingSpot> GetAll()
        => mySpotDbContext.WeeklyParkingSpots
                            .Include(x => x.Reservations)
                            .ToList();

        public void Update(WeeklyParkingSpot parkingSpot)
        {
            mySpotDbContext.Update(parkingSpot);
            mySpotDbContext.SaveChanges();
        }
    }
}
