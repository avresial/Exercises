using Confab.Shared.Abstractions.Commands;
using System;

namespace Confab.Modules.Agendas.Application.Agendas.Commands;

public record AssignPlaceholderAgendaSlot(Guid AgendaSlotId, Guid AgendaTrackId, string Placeholder) : ICommand;