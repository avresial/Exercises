using Confab.Modules.Attendances.Infrastructure.EF;
using Confab.Shared.Tests;
using System;

namespace Confab.Modules.Attendances.Tests.Integration.Common;

public class TestAttendancesDbContext : IDisposable
{
    public AttendancesDbContext DbContext { get; } = new(DbHelper.GetOptions<AttendancesDbContext>());

    public void Dispose()
    {
        DbContext?.Database.EnsureDeleted();
        DbContext?.Dispose();
    }
}