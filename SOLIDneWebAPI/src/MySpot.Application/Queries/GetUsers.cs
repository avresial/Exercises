using MySpot.Application.Abstractions;
using MySpot.Application.Dtos;

namespace MySpot.Application.Queries;

public class GetUsers : IQuery<IEnumerable<UserDto>>
{
    public Guid UserId { get; set; }
}