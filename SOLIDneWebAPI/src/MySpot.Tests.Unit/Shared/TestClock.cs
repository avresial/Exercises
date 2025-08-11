using MySpot.Core.Services;
using MySpot.Core.ValueObjects;

namespace MySpot.Tests.Unit.Shared
{
    internal class TestClock : IClock
    {
        public Date Current() => new Date(DateTime.UtcNow.Date);
    }
}
