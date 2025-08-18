using MySpot.Core.Services;

namespace MySpot.Infrastructure.Time
{
    public class Clock : IClock
    {
        public DateTime Current() => DateTime.Now;
    }
}
