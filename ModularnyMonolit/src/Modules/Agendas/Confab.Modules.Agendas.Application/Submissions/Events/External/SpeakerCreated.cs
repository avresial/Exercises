using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Agendas.Application.Submissions.Events.External;

public record SpeakerCreated(Guid Id, string FullName) : IEvent;