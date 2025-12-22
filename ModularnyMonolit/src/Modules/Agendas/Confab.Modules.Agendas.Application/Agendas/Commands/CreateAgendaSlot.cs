using Confab.Shared.Abstractions.Commands;
using System;

namespace Confab.Modules.Agendas.Application.Agendas.Commands;

public record CreateAgendaSlot(Guid AgendaTrackId, DateTime From, DateTime To,
    int? ParticipantsLimit, string Type) : ICommand
{
    public Guid Id = Guid.NewGuid();
}