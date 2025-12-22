using Confab.Shared.Abstractions.Commands;
using System;

namespace Confab.Modules.Agendas.Application.Agendas.Commands;

public record DeleteAgendaTrack(Guid Id) : ICommand;