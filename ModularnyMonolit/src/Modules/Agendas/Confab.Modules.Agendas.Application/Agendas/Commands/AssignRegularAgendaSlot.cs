using Confab.Shared.Abstractions.Commands;
using System;

namespace Confab.Modules.Agendas.Application.Agendas.Commands;

public sealed record AssignRegularAgendaSlot(Guid AgendaTrackId, Guid AgendaSlotId, Guid AgendaItemId) : ICommand;