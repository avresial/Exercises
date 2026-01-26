using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Saga.Messages;

public record SpeakerCreated(Guid Id, string FullName) : IEvent;