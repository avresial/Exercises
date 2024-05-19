using MySpot.Api.Entities;
using MySpot.Api.Repositories;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IClock, Clock>()
				.AddSingleton<IEnumerable<WeeklyParkingSpot>>(serviceprovider =>
				{
					var clock = serviceprovider.GetService<IClock>();
					return new List<WeeklyParkingSpot>()
					{
						new (Guid.Parse("00000000-0000-0000-0000-000000000001"),new Week(clock.Current().Value.Date),"P1"),
						new (Guid.Parse("00000000-0000-0000-0000-000000000002"),new Week(clock.Current().Value.Date),"P2"),
						new (Guid.Parse("00000000-0000-0000-0000-000000000003"),new Week(clock.Current().Value.Date),"P3"),
						new (Guid.Parse("00000000-0000-0000-0000-000000000004"),new Week(clock.Current().Value.Date),"P4"),
						new (Guid.Parse("00000000-0000-0000-0000-000000000005"),new Week(clock.Current().Value.Date),"P5"),
					};
				})
				.AddSingleton<IReservationsService, ReservationsService>()
				.AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>()
				.AddControllers();


var app = builder.Build();
app.MapControllers();
app.Run();



