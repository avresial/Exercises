using MySpot.Application.Services;
using MySpot.Core.ValueObjects;

namespace MySpot.Tests.Unit.Shared
{
    internal class TestClock : IClock
    {
        public Date Current() => new Date(new DateTime(2024, 05, 20));
    }
}
