using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Agendas.Application.Submissions.Events;

public record SubmissionRejected(Guid Id) : IEvent;