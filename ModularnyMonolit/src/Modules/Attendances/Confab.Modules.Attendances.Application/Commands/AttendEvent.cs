using Confab.Shared.Abstractions.Commands;
using System;

namespace Confab.Modules.Attendances.Application.Commands;

public record AttendEvent(Guid Id, Guid ParticipantId) : ICommand;