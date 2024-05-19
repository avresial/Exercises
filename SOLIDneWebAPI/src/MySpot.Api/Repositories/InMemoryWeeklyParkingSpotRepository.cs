using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Repositories
{
	public class InMemoryWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
	{
		private List<WeeklyParkingSpot> _weeklyParkingSpots = new();

		public InMemoryWeeklyParkingSpotRepository(IClock clock)
		{
			Clock = clock;
			_weeklyParkingSpots = new List<WeeklyParkingSpot>()
					{
						new (Guid.Parse("00000000-0000-0000-0000-000000000001"),new Week(clock.Current().Value.Date),"P1"),
						new (Guid.Parse("00000000-0000-0000-0000-000000000002"),new Week(clock.Current().Value.Date),"P2"),
						new (Guid.Parse("00000000-0000-0000-0000-000000000003"),new Week(clock.Current().Value.Date),"P3"),
						new (Guid.Parse("00000000-0000-0000-0000-000000000004"),new Week(clock.Current().Value.Date),"P4"),
						new (Guid.Parse("00000000-0000-0000-0000-000000000005"),new Week(clock.Current().Value.Date),"P5"),
					};
		}

		public IClock Clock { get; }

		public void Add(WeeklyParkingSpot parkingSpot)
		{
			_weeklyParkingSpots.Add(parkingSpot);
		}

		public void Delete(WeeklyParkingSpot parkingSpot)
		{
			_weeklyParkingSpots.Remove(parkingSpot);
		}

		public WeeklyParkingSpot Get(ParkingSpotId id) => _weeklyParkingSpots.FirstOrDefault(x => x.Id == id);

		public IEnumerable<WeeklyParkingSpot> GetAll() => _weeklyParkingSpots;

		public void Update(WeeklyParkingSpot parkingSpot)
		{

		}
	}
}
