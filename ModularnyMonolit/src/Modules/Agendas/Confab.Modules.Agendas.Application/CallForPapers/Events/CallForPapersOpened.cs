using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Agendas.Application.CallForPapers.Events;

internal record CallForPapersOpened(Guid ConferenceId, DateTime From, DateTime To) : IEvent;