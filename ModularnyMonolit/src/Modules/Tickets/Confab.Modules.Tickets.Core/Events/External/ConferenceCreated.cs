using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Tickets.Core.Events.External;

public record ConferenceCreated(Guid Id, string Name, int? ParticipantsLimit, DateTime From, DateTime To) : IEvent;
