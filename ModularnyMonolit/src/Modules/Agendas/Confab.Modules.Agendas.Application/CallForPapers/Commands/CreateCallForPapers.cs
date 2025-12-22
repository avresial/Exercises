using Confab.Shared.Abstractions.Commands;
using System;

namespace Confab.Modules.Agendas.Application.CallForPapers.Commands;

public record CreateCallForPapers(Guid ConferenceId, DateTime From, DateTime To) : ICommand
{
    public Guid Id { get; } = Guid.NewGuid();
}