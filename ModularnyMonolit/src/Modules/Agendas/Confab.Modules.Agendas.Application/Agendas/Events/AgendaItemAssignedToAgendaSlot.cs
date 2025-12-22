using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Agendas.Application.Agendas.Events;

public record AgendaItemAssignedToAgendaSlot(Guid Id, Guid AgendaItemId) : IEvent;