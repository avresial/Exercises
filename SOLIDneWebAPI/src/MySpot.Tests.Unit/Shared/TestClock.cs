using MySpot.Core.Services;

namespace MySpot.Tests.Unit.Shared
{
    internal class TestClock : IClock
    {
        //public Date Current() => new Date(DateTime.UtcNow.Date);
        public DateTime Current() => new(2020, 01, 01, 12, 0, 0);
    }
}
