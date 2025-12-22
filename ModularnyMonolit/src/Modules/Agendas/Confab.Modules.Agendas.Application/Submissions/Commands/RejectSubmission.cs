using Confab.Shared.Abstractions.Commands;
using System;

namespace Confab.Modules.Agendas.Application.Submissions.Commands;

public record RejectSubmission(Guid Id) : ICommand;