using Confab.Shared.Abstractions.Commands;
using System;

namespace Confab.Modules.Agendas.Application.Agendas.Commands;

public record CreateAgendaTrack(Guid ConferenceId, string Name) : ICommand
{
    public Guid Id { get; } = Guid.NewGuid();
}