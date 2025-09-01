using Microsoft.EntityFrameworkCore;
using MySpot.Application.Abstractions;
using MySpot.Application.Dtos;
using MySpot.Application.Queries;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Handlers;
internal sealed class GetWeeklyParkingSpotsHandler : IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>>
{
    private readonly MySpotDbContext _mySpotDbContext;

    public GetWeeklyParkingSpotsHandler(MySpotDbContext mySpotDbContext)
    {
        _mySpotDbContext = mySpotDbContext;
    }

    public async Task<IEnumerable<WeeklyParkingSpotDto>> HandleAsync(GetWeeklyParkingSpots query)
    {
        var week = query.Date.HasValue ? new Week(query.Date.Value) : null;
        var weeklyParkingSpots = await _mySpotDbContext.WeeklyParkingSpots.Where(wps => week == null || wps.Week == week)
            .Include(x => x.Reservations)
            .AsNoTracking()
            .ToListAsync();

        return weeklyParkingSpots.Select(wps => wps.AsDto());
    }
}
