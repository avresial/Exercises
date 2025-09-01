using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions;
public sealed class WeeklyParkingSpotNotFound : CustomException
{
    public Guid Id { get; }

    public WeeklyParkingSpotNotFound(Guid id) : base($"Weekly parking spot with id {id} was not found")
    {
        Id = id;
    }
}
