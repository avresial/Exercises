using Confab.Shared.Abstractions.Commands;
using System;

namespace Confab.Modules.Agendas.Application.Agendas.Commands;

public record ChangeAgendaTrackName(Guid Id, string Name) : ICommand;