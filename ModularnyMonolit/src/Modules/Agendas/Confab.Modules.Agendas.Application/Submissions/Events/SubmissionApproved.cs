using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Agendas.Application.Submissions.Events;

public record SubmissionApproved(Guid Id) : IEvent;