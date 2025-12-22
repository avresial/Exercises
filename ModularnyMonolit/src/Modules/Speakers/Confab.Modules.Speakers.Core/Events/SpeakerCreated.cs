using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Speakers.Core.Events;

public record SpeakerCreated(Guid Id, string FullName) : IEvent;