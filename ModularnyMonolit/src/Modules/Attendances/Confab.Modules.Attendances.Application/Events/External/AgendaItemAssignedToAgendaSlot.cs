using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Attendances.Application.Events.External
{
    internal record AgendaItemAssignedToAgendaSlot(Guid Id, Guid AgendaItemId) : IEvent;
}