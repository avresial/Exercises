using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Agendas.Application.Agendas.Events;

public record PlaceholderAssignedToAgendaSlot(Guid Id, string Placeholder) : IEvent;