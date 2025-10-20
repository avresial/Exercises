using Confab.Modules.Conferences.Core.Entities;
using Confab.Shared.Abstractions.Time;

namespace Confab.Modules.Conferences.Core.Policies;

internal class ConferenceDeletionPolicy(IClock clock) : IConferenceDeletionPolicy
{
    public Task<bool> CanDeleteAsync(Conference conference) =>
        Task.FromResult(clock.CurrentDate().Date.AddDays(7) < conference.From.Date);
}
