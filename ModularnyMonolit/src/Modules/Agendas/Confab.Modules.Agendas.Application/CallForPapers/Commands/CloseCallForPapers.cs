using Confab.Shared.Abstractions.Commands;
using System;

namespace Confab.Modules.Agendas.Application.CallForPapers.Commands;

public record CloseCallForPapers(Guid ConferenceId) : ICommand;