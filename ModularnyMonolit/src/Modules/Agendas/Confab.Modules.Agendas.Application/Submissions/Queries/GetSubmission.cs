using Confab.Modules.Agendas.Application.Submissions.DTO;
using Confab.Shared.Abstractions.Queries;
using System;

namespace Confab.Modules.Agendas.Application.Submissions.Queries;

public class GetSubmission : IQuery<SubmissionDto>
{
    public Guid Id { get; set; }
}