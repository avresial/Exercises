using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Agendas.Application.CallForPapers.Events;

internal record CallForPapersCreated(Guid ConferenceId) : IEvent;