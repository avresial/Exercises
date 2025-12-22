using Confab.Shared.Abstractions.Exceptions;
using System;

namespace Confab.Modules.Agendas.Application.CallForPapers.Exceptions;

internal class CallForPapersAlreadyExistsException : ConfabException
{
    public Guid ConferenceId { get; }

    public CallForPapersAlreadyExistsException(Guid conferenceId)
        : base($"Conference with ID: '{conferenceId}' already defined CFP.")
        => ConferenceId = conferenceId;
}