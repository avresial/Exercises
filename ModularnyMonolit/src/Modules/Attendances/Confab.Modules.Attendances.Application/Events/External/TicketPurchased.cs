using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Attendances.Application.Events.External
{
    internal record TicketPurchased(Guid TicketId, Guid ConferenceId, Guid UserId) : IEvent;
}