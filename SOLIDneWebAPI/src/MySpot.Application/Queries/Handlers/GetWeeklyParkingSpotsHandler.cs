using MySpot.Application.Abstractions;
using MySpot.Application.Dtos;

namespace MySpot.Application.Queries.Handlers;
public class GetWeeklyParkingSpotsHandler : IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>>
{
    public Task<IEnumerable<WeeklyParkingSpotDto>> HandleAsync(GetWeeklyParkingSpots query)
    {
        throw new NotImplementedException();
    }
}
