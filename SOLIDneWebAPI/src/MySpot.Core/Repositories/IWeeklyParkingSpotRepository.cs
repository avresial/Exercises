﻿using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Repositories
{
	public interface IWeeklyParkingSpotRepository
	{
		WeeklyParkingSpot Get(ParkingSpotId id);
		IEnumerable<WeeklyParkingSpot> GetAll();

		void Add(WeeklyParkingSpot parkingSpot);
		void Update(WeeklyParkingSpot parkingSpot);
		void Delete(WeeklyParkingSpot parkingSpot);
	}
}
