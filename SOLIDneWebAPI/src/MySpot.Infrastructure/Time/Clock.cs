using MySpot.Application.Services;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.Time
{
    public class Clock : IClock
    {
        public Date Current() => Date.Now;
    }
}
