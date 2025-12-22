using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Agendas.Application.Agendas.Events;

public record AgendaTrackUpdated(Guid Id) : IEvent;