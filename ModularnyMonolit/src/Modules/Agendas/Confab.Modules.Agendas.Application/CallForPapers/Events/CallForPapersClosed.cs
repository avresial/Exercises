using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Agendas.Application.CallForPapers.Events;

internal record CallForPapersClosed(Guid ConferenceId) : IEvent;